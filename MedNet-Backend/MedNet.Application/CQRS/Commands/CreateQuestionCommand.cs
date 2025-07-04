using AutoMapper;
using MediatR;
using MedNet.Application.DTOs;
using MedNet.Application.Interfaces;
using MedNet.Application.Models;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Exceptions;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Commands;

using CreateQuestionCommandResult = Result<int>;

public class CreateQuestionCommand : IRequest<CreateQuestionCommandResult>
{
    public required int ParentQuestionSetId { get; init; }
    public required string Body { get; init; }
    public int BlankQuestionNumber { get; init; } = 0;
    public ICollection<AnswerDto> Answers { get; init; } = [];

    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, CreateQuestionCommandResult>
    {
        private readonly IReadOnlyRepositoryAsync<QuestionsSet> _questionsSetRoRepository;
        private readonly IWriteRepositoryAsync<Question> _questionRwRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CreateQuestionCommandHandler(IReadOnlyRepositoryAsync<QuestionsSet> questionsSetRoRepository,
            IWriteRepositoryAsync<Question> questionRwRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _questionsSetRoRepository = questionsSetRoRepository;
            _questionRwRepository = questionRwRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateQuestionCommandResult> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var questionSet =
                await _questionsSetRoRepository.SingleOrDefaultAsync(
                    new GetEntityByIdSpecification<QuestionsSet>(request.ParentQuestionSetId), cancellationToken);
            if (questionSet == null)
            {
                return CreateQuestionCommandResult.Failure($"A {nameof(QuestionsSet)} with id '{request.ParentQuestionSetId}' was not found",
                    "set_not_found");
            }

            {
                var ans = request.Answers.FirstOrDefault(ans => ans.Id != 0);
                if (ans != null)
                {
                    return CreateQuestionCommandResult.Failure($"A {nameof(Answer)} must not have an id installed, '{ans.Id}' provided", "forbidden_answer_id");
                }
            }

            var question = new Question()
            {
                Body = request.Body,
                ParentQuestionsSetId = request.ParentQuestionSetId,
                BlankQuestionNumber = request.BlankQuestionNumber,
                Answers = _mapper.Map<ICollection<Answer>>(request.Answers),
            };

            await _questionRwRepository.AddAsync(question, cancellationToken);
            
            try
            {
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (DbUniqueConstraintViolationException exc) when (exc.Entity is Question qt)
            {
                return CreateQuestionCommandResult.Failure($"A {nameof(Question)} with body '{qt.Body}' already exists", "question_already_exists");
            }
            catch (DbUniqueConstraintViolationException exc) when (exc.Entity is Answer ans)
            {
                return CreateQuestionCommandResult.Failure($"Conflicting {nameof(Answer)} body occurred: '{ans.Body}'", "conflicting_answer_body");
            }

            return CreateQuestionCommandResult.Success(question.Id);
        }
    }
}

using AutoMapper;
using MediatR;
using MedNet.Application.DTOs;
using MedNet.Application.Helpers;
using MedNet.Application.Interfaces;
using MedNet.Application.Models;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Exceptions;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Commands;

using UpdateQuestionCommandResult = Result;

public class UpdateQuestionCommand : IRequest<UpdateQuestionCommandResult>
{
    public required int Id { get; init; }

    public string? Body { get; init; }
    public ICollection<AnswerDto>? Answers { get; init; }

    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, UpdateQuestionCommandResult>
    {
        private readonly IWriteRepositoryAsync<Question> _questionRwRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateQuestionCommandHandler(IWriteRepositoryAsync<Question> questionRwRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _questionRwRepository = questionRwRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateQuestionCommandResult> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question =
                await _questionRwRepository.SingleOrDefaultAsync(
                    new GetEntityByIdSpecification<Question>(request.Id), cancellationToken);
            if (question == null)
            {
                return UpdateQuestionCommandResult.Failure($"A {nameof(Question)} with id '{request.Id}' was not found",
                    "question_not_found");
            }

            {
                if (request.Answers != null)
                {
                    foreach (var answer in request.Answers)
                    {
                        if (answer.Id != 0)
                        {
                            if (question.Answers.FirstOrDefault(a => a.Id == answer.Id) == null)
                            {
                                return UpdateQuestionCommandResult.Failure($"A new entry of {nameof(Answer)} must not have an id installed, '{answer.Id}' provided", "forbidden_answer_id");
                            }
                        }
                    }

                    question.Answers = _mapper.Map<ICollection<Answer>>(request.Answers);
                }

                if (request.Body != null)
                {
                    question.Body = request.Body;
                }
            }
            
            try
            {
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (DbUniqueConstraintViolationException exc) when (exc.Entity is Question qt)
            {
                return UpdateQuestionCommandResult.Failure($"A {nameof(Question)} with body '{qt.Body}' already exists", "question_already_exists");
            }
            catch (DbUniqueConstraintViolationException exc) when (exc.Entity is Answer ans)
            {
                return UpdateQuestionCommandResult.Failure($"Conflicting {nameof(Answer)} body occurred: '{ans.Body}'", "conflicting_answer_body");
            }

            return UpdateQuestionCommandResult.Success();
        }
    }
}

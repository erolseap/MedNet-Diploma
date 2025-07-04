using AutoMapper;
using MediatR;
using MedNet.Application.DTOs;
using MedNet.Application.Interfaces;
using MedNet.Application.Models;
using MedNet.Application.Specifications.QuestionSpecifications;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Commands;

using GenerateUserTestSessionCommandResponse = Result<int>;

public class GenerateUserTestSessionCommand : IRequest<GenerateUserTestSessionCommandResponse>
{
    public required int QuestionsSetId { get; init; }
    public required int UserId { get; init; }
    public int NumOfQuestions { get; init; } = 50;

    public class GenerateUserTestSessionCommandHandler : IRequestHandler<GenerateUserTestSessionCommand, GenerateUserTestSessionCommandResponse>
    {
        private readonly IWriteRepositoryAsync<UserTestSession> _userTestSessionRepository;
        private readonly IWriteRepositoryAsync<QuestionsSet> _questionsSetRepository;
        private readonly IWriteRepositoryAsync<Question> _questionRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public GenerateUserTestSessionCommandHandler(IWriteRepositoryAsync<UserTestSession> userTestSessionRepository, IWriteRepositoryAsync<QuestionsSet> questionsSetRepository, IUnitOfWork unitOfWork, IWriteRepositoryAsync<Question> questionRepository)
        {
            _userTestSessionRepository = userTestSessionRepository;
            _questionsSetRepository = questionsSetRepository;
            _unitOfWork = unitOfWork;
            _questionRepository = questionRepository;
        }

        public async Task<GenerateUserTestSessionCommandResponse> Handle(GenerateUserTestSessionCommand request, CancellationToken cancellationToken)
        {
            var specification = new GetEntityByIdSpecification<QuestionsSet>(request.QuestionsSetId);
            var qs = await _questionsSetRepository.SingleOrDefaultAsync(specification, cancellationToken);
            if (qs == null)
            {
                return GenerateUserTestSessionCommandResponse.Failure($"A {nameof(QuestionsSet)} with id '{request.QuestionsSetId}' was not found",
                    "set_not_found");
            }

            var questionsSpecification = new FetchQuestionsInQuestionsSetIdSpecification(qs.Id);
            questionsSpecification.ApplyOrderBy(q => Guid.NewGuid());
            questionsSpecification.Take(int.Clamp(request.NumOfQuestions, 3, 300));

            var questions = await _questionRepository.ListAsync(questionsSpecification, cancellationToken);
            if (questions.Count == 0)
            {
                return GenerateUserTestSessionCommandResponse.Failure($"A {nameof(QuestionsSet)} with id '{request.QuestionsSetId}' does not contain any questions",
                    "set_empty");
            }

            var test = new UserTestSession
            {
                UserId = request.UserId,
                QuestionsSetId = request.QuestionsSetId,
                Questions = []
            };
            foreach (var question in questions)
            {
                test.Questions.Add(new UserTestSessionQuestion()
                {
                    Question = question,
                });
            }

            await _userTestSessionRepository.AddAsync(test, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return GenerateUserTestSessionCommandResponse.Success(test.Id);
        }
    }
}

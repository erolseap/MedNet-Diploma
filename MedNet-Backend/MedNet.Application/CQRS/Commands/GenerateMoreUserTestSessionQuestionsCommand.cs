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

using GenerateMoreUserTestSessionQuestionsCommandResponse = Result;

public class GenerateMoreUserTestSessionQuestionsCommand : IRequest<GenerateMoreUserTestSessionQuestionsCommandResponse>
{
    public required int Id { get; init; }
    public int NumOfQuestions { get; init; } = 50;

    public class GenerateMoreUserTestSessionQuestionsCommandHandler : IRequestHandler<GenerateMoreUserTestSessionQuestionsCommand, GenerateMoreUserTestSessionQuestionsCommandResponse>
    {
        private readonly IWriteRepositoryAsync<UserTestSession> _userTestSessionRepository;
        private readonly IWriteRepositoryAsync<Question> _questionRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public GenerateMoreUserTestSessionQuestionsCommandHandler(IWriteRepositoryAsync<UserTestSession> userTestSessionRepository, IUnitOfWork unitOfWork, IWriteRepositoryAsync<Question> questionRepository)
        {
            _userTestSessionRepository = userTestSessionRepository;
            _unitOfWork = unitOfWork;
            _questionRepository = questionRepository;
        }

        public async Task<GenerateMoreUserTestSessionQuestionsCommandResponse> Handle(GenerateMoreUserTestSessionQuestionsCommand request, CancellationToken cancellationToken)
        {
            var sessionSpecification = new GetEntityByIdSpecification<UserTestSession>(request.Id);
            sessionSpecification.AddInclude(s => s.QuestionsSet);
            sessionSpecification.AddInclude(s => s.Questions);
            sessionSpecification.AddInclude(nameof(UserTestSession.Questions), nameof(UserTestSessionQuestion.Question));
            var session = await _userTestSessionRepository.SingleOrDefaultAsync(sessionSpecification, cancellationToken);
            
            if (session == null)
            {
                return GenerateMoreUserTestSessionQuestionsCommandResponse.Failure($"A {nameof(UserTestSession)} with id '{request.Id}' was not found",
                    "user_test_not_found");
            }

            var questionsSpecification = new FetchQuestionsInQuestionsSetIdExceptIdsSpecification(session.QuestionsSetId, session.Questions.Select(q => q.QuestionId).ToList());
            questionsSpecification.ApplyOrderBy(q => Guid.NewGuid());
            questionsSpecification.Take(int.Clamp(request.NumOfQuestions, 3, 300));

            var questions = await _questionRepository.ListAsync(questionsSpecification, cancellationToken);
            if (questions.Count == 0)
            {
                return GenerateMoreUserTestSessionQuestionsCommandResponse.Failure($"A {nameof(QuestionsSet)} with id '{session.QuestionsSetId}' does not contain any questions",
                    "set_empty");
            }
            
            foreach (var question in questions)
            {
                session.Questions.Add(new UserTestSessionQuestion()
                {
                    Question = question,
                });
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return GenerateMoreUserTestSessionQuestionsCommandResponse.Success();
        }
    }
}

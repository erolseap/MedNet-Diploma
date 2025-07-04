using MediatR;
using MedNet.Application.Interfaces;
using MedNet.Application.Models;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Commands;

using AnswerUserTestSessionQuestionCommandResponse = Result<int>;

public class AnswerUserTestSessionQuestionCommand : IRequest<AnswerUserTestSessionQuestionCommandResponse>
{
    public required int Id { get; init; }
    public required int AnswerId { get; init; }

    public class AnswerUserSessionQuestionCommandHandler : IRequestHandler<AnswerUserTestSessionQuestionCommand, AnswerUserTestSessionQuestionCommandResponse>
    {
        private readonly IWriteRepositoryAsync<UserTestSessionQuestion> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AnswerUserSessionQuestionCommandHandler(IWriteRepositoryAsync<UserTestSessionQuestion> userTestSessionRepository, IUnitOfWork unitOfWork, IWriteRepositoryAsync<UserTestSessionQuestion> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AnswerUserTestSessionQuestionCommandResponse> Handle(AnswerUserTestSessionQuestionCommand request, CancellationToken cancellationToken)
        {
            var specification = new GetEntityByIdSpecification<UserTestSessionQuestion>(request.Id);
            specification.AddInclude(nameof(UserTestSessionQuestion.Question));
            specification.AddInclude(
                nameof(UserTestSessionQuestion.Question),
                nameof(UserTestSessionQuestion.Question.Answers)
            );
            var question = await _repository.SingleOrDefaultAsync(specification, cancellationToken);
            if (question == null)
            {
                return AnswerUserTestSessionQuestionCommandResponse.Failure($"A {nameof(UserTestSessionQuestion)} with id '{request.Id}' was not found",
                    "question_not_found");
            }
            if (question.AnswerId != null)
            {
                return AnswerUserTestSessionQuestionCommandResponse.Failure($"This {nameof(UserTestSessionQuestion)} has already been answered (answer id: {question.AnswerId})",
                    "question_already_answered");
            }

            if (question.Question!.Answers.SingleOrDefault(a => a.Id == request.AnswerId) == null)
            {
                return AnswerUserTestSessionQuestionCommandResponse.Failure($"A {nameof(Answer)} with id '{request.AnswerId}' was not found",
                    "answer_not_found");
            }
            question.AnswerId = request.AnswerId;
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return AnswerUserTestSessionQuestionCommandResponse.Success(question.Question.CorrectAnswerId);
        }
    }
}

using MediatR;
using MedNet.Application.Interfaces;
using MedNet.Application.Models;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Commands;

using DeleteQuestionCommandResponse = Result;

public class DeleteQuestionCommand : IRequest<DeleteQuestionCommandResponse>
{
    public required int Id { get; init; }

    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, DeleteQuestionCommandResponse>
    {
        private readonly IWriteRepositoryAsync<Question> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionCommandHandler(IWriteRepositoryAsync<Question> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteQuestionCommandResponse> Handle(DeleteQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var question = await _repository.SingleOrDefaultAsync(new GetEntityByIdSpecification<Question>(request.Id),
                cancellationToken);
            if (question == null)
            {
                return DeleteQuestionCommandResponse.Failure($"A {nameof(Question)} with id '{request.Id}' was not found",
                    "question_not_found");
            }

            _repository.Remove(question);
            await _unitOfWork.CommitAsync(cancellationToken);

            return DeleteQuestionCommandResponse.Success();
        }
    }
}

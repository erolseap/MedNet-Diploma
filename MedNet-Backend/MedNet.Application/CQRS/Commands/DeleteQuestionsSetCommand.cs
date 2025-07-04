using MediatR;
using MedNet.Application.Interfaces;
using MedNet.Application.Models;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Commands;

using DeleteQuestionsSetCommandResponse = Result;

public class DeleteQuestionsSetCommand : IRequest<DeleteQuestionsSetCommandResponse>
{
    public required int Id { get; init; }

    public class DeleteQuestionsSetCommandHandler : IRequestHandler<DeleteQuestionsSetCommand, DeleteQuestionsSetCommandResponse>
    {
        private readonly IWriteRepositoryAsync<QuestionsSet> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionsSetCommandHandler(IWriteRepositoryAsync<QuestionsSet> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteQuestionsSetCommandResponse> Handle(DeleteQuestionsSetCommand request,
            CancellationToken cancellationToken)
        {
            var qs = await _repository.SingleOrDefaultAsync(new GetEntityByIdSpecification<QuestionsSet>(request.Id),
                cancellationToken);
            if (qs == null)
            {
                return DeleteQuestionsSetCommandResponse.Failure($"A {nameof(QuestionsSet)} with id '{request.Id}' was not found",
                    "set_not_found");
            }

            _repository.Remove(qs);
            await _unitOfWork.CommitAsync(cancellationToken);

            return DeleteQuestionsSetCommandResponse.Success();
        }
    }
}

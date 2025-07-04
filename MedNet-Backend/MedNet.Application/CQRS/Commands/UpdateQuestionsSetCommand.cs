using MediatR;
using MedNet.Application.Interfaces;
using MedNet.Application.Models;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Exceptions;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Commands;

using UpdateQuestionsSetCommandResponse = Result;

public class UpdateQuestionsSetCommand : IRequest<UpdateQuestionsSetCommandResponse>
{
    public required int Id { get; init; }

    public string? Name { get; init; }
    
    public class UpdateQuestionsSetCommandHandler : IRequestHandler<UpdateQuestionsSetCommand, UpdateQuestionsSetCommandResponse>
    {
        private readonly IWriteRepositoryAsync<QuestionsSet> _repository;
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateQuestionsSetCommandHandler(IWriteRepositoryAsync<QuestionsSet> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateQuestionsSetCommandResponse> Handle(UpdateQuestionsSetCommand request, CancellationToken cancellationToken)
        { 
            var qs = await _repository.SingleOrDefaultAsync(new GetEntityByIdSpecification<QuestionsSet>(request.Id), cancellationToken);
            if (qs == null)
            {
                return UpdateQuestionsSetCommandResponse.Failure($"A {nameof(QuestionsSet)} with id '{request.Id}' was not found",
                    "set_not_found");
            }

            if (request.Name != null)
            {
                qs.Name = request.Name;
            }

            try
            {
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (DbUniqueConstraintViolationException)
            {
                return UpdateQuestionsSetCommandResponse.Failure($"A {nameof(QuestionsSet)} with name '{request.Name}' already exists", "set_already_exists");
            }
            return UpdateQuestionsSetCommandResponse.Success();
        }
    }
}

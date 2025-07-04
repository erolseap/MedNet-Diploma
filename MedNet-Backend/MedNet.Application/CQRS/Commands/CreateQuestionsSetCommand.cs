using MediatR;
using MedNet.Application.Interfaces;
using MedNet.Application.Models;
using MedNet.Domain.Entities;
using MedNet.Domain.Exceptions;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Commands;

using CreateQuestionsSetCommandResponse = Result<int>;

public class CreateQuestionsSetCommand : IRequest<CreateQuestionsSetCommandResponse>
{
    public required string Name { get; init; }

    public class CreateQuestionsSetCommandHandler : IRequestHandler<CreateQuestionsSetCommand, CreateQuestionsSetCommandResponse>
    {
        private readonly IWriteRepositoryAsync<QuestionsSet> _questionsSetRwRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateQuestionsSetCommandHandler(IWriteRepositoryAsync<QuestionsSet> questionsSetRwRepository, IUnitOfWork unitOfWork)
        {
            _questionsSetRwRepository = questionsSetRwRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateQuestionsSetCommandResponse> Handle(CreateQuestionsSetCommand request, CancellationToken cancellationToken)
        {
            var questionsSet =
                await _questionsSetRwRepository.AddAsync(
                    new QuestionsSet()
                    {
                        Name = request.Name,
                    }, cancellationToken
                );

            try
            {
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (DbUniqueConstraintViolationException)
            {
                return CreateQuestionsSetCommandResponse.Failure($"A {nameof(QuestionsSet)} with name '{request.Name}' already exists", "set_already_exists");
            }

            return CreateQuestionsSetCommandResponse.Success(questionsSet.Id);
        }
    }
}

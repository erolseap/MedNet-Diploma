using AutoMapper;
using MediatR;
using MedNet.Application.DTOs;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Queries;

public class GetUserTestByIdQuery : IRequest<UserTestSessionDto?>
{
    public required int Id { get; init; }

    public class GetUserTestByIdQueryHandler : IRequestHandler<GetUserTestByIdQuery, UserTestSessionDto?>
    {
        private readonly IReadOnlyRepositoryAsync<UserTestSession> _repository;
        private readonly IMapper _mapper;

        public GetUserTestByIdQueryHandler(IReadOnlyRepositoryAsync<UserTestSession> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserTestSessionDto?> Handle(GetUserTestByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetEntityByIdSpecification<UserTestSession>(request.Id);
            specification.AddInclude(s => s.Questions); // required for number of questions
            specification.AddInclude(s => s.QuestionsSet!); // required for dto
            specification.AddInclude(nameof(UserTestSession.Questions), nameof(UserTestSessionQuestion.Answer)); // required for dto

            var session = await _repository.SingleOrDefaultAsync(specification, cancellationToken);
            return _mapper.Map<UserTestSessionDto?>(session);
        }
    }
}

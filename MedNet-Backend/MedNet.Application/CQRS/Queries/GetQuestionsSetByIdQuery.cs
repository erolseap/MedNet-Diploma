using AutoMapper;
using MediatR;
using MedNet.Application.DTOs;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Queries;

public class GetQuestionsSetByIdQuery : IRequest<QuestionsSetWithNumOfQuestionsDto?>
{
    public required int Id { get; init; }

    public class GetQuestionsSetByIdQueryHandler : IRequestHandler<GetQuestionsSetByIdQuery, QuestionsSetWithNumOfQuestionsDto?>
    {
        private readonly IReadOnlyRepositoryAsync<QuestionsSet> _repository;
        private readonly IMapper _mapper;

        public GetQuestionsSetByIdQueryHandler(IReadOnlyRepositoryAsync<QuestionsSet> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<QuestionsSetWithNumOfQuestionsDto?> Handle(GetQuestionsSetByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetEntityByIdSpecification<QuestionsSet>(request.Id);
            specification.AddInclude(qs => qs.Questions); // required for number of questions

            var qs = await _repository.SingleOrDefaultAsync(specification, cancellationToken);
            return _mapper.Map<QuestionsSetWithNumOfQuestionsDto?>(qs);
        }
    }
}

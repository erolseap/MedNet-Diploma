using AutoMapper;
using MediatR;
using MedNet.Application.DTOs;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Queries;

public class GetQuestionByIdQuery : IRequest<QuestionDto?>
{
    public required int Id { get; init; }

    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, QuestionDto?>
    {
        private readonly IReadOnlyRepositoryAsync<Question> _repository;
        private readonly IMapper _mapper;

        public GetQuestionByIdQueryHandler(IReadOnlyRepositoryAsync<Question> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<QuestionDto?> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetEntityByIdSpecification<Question>(request.Id);
            specification.AddInclude(qs => qs.Answers);

            var question = await _repository.SingleOrDefaultAsync(specification, cancellationToken);
            return _mapper.Map<QuestionDto?>(question);
        }
    }
}

using AutoMapper;
using MediatR;
using MedNet.Application.DTOs;
using MedNet.Application.Specifications.QuestionSpecifications;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Repositories;
using MedNet.Domain.Specifications;

namespace MedNet.Application.CQRS.Queries;

using ListQuestionsQueryResponse = IEnumerable<QuestionDto>;

public class ListQuestionsQuery : IRequest<ListQuestionsQueryResponse>
{
    /// <summary>
    /// A parent questions set id
    /// </summary>
    public int? ParentQuestionsSetId { get; init; }

    /// <summary>
    /// Number of questions to skip
    /// </summary>
    public int? Offset { get; init; } = null;
   
    /// <summary>
    /// Number of questions to get
    /// </summary>
    public int? Limit { get; init; } = null;
    

    public class ListQuestionsQueryHandler : IRequestHandler<ListQuestionsQuery, ListQuestionsQueryResponse>
    {
        private readonly IReadOnlyRepositoryAsync<Question> _questionRoRepository;
        private readonly IMapper _mapper;
        
        public ListQuestionsQueryHandler(IReadOnlyRepositoryAsync<Question> questionRoRepository, IMapper mapper)
        {
            _questionRoRepository = questionRoRepository;
            _mapper = mapper;
        }

        public async Task<ListQuestionsQueryResponse> Handle(ListQuestionsQuery request, CancellationToken cancellationToken)
        {
            BaseSpecification<Question> specification = new FetchAllEntitiesSpecification<Question>();
            if (request.ParentQuestionsSetId != null)
            {
                specification = new FetchQuestionsInQuestionsSetIdSpecification(request.ParentQuestionsSetId.Value);
            }

            if (request.Offset != null)
            {
                specification.Skip(request.Offset.Value);
            }

            if (request.Limit != null)
            {
                specification.Take(request.Limit.Value);
            }
            specification.AddInclude(qs => qs.Answers);

            var questions = await _questionRoRepository
                .ListAsync(specification, cancellationToken);
         
            return _mapper.Map<ListQuestionsQueryResponse>(questions);
        }
    }
}

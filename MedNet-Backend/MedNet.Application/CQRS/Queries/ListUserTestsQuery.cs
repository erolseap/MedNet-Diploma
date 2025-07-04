using AutoMapper;
using MediatR;
using MedNet.Application.DTOs;
using MedNet.Application.Specifications.UserTestSessionSpecifications;
using MedNet.Domain.Entities;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Queries;

using ListUserTestsQueryResponse = IReadOnlyList<UserTestSessionDto>;

public class ListUserTestsQuery : IRequest<ListUserTestsQueryResponse>
{
    public required int UserId { get; init; }
    
    /// <summary>
    /// Number of items to skip
    /// </summary>
    public int? Offset { get; init; } = null;
   
    /// <summary>
    /// Number of items to get
    /// </summary>
    public int? Limit { get; init; } = null;

    public class ListUserTestsQueryHandler : IRequestHandler<ListUserTestsQuery, ListUserTestsQueryResponse>
    {
        private readonly IReadOnlyRepositoryAsync<UserTestSession> _repository;
        private readonly IMapper _mapper;

        public ListUserTestsQueryHandler(IReadOnlyRepositoryAsync<UserTestSession> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ListUserTestsQueryResponse> Handle(ListUserTestsQuery request, CancellationToken cancellationToken)
        {
            var specification = new FetchUserTestSessionsByUserId(request.UserId);
            specification.ApplyOrderByDescending(s => s.CreationDate);
            if (request.Offset != null)
            {
                specification.Skip(request.Offset.Value);
            }

            if (request.Limit != null)
            {
                specification.Take(request.Limit.Value);
            }
            specification.AddInclude(s => s.Questions); // required for number of questions and count of correct answers
            
            var sessions = await _repository
                .ListAsync(specification, cancellationToken);
         
            return _mapper.Map<ListUserTestsQueryResponse>(sessions);
        }
    }
}

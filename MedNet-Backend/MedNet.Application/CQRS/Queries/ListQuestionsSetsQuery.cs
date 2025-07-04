using AutoMapper;
using MediatR;
using MedNet.Application.DTOs;
using MedNet.Application.Specifications.Shared;
using MedNet.Domain.Entities;
using MedNet.Domain.Repositories;

namespace MedNet.Application.CQRS.Queries;

using ListQuestionsSetsQueryResponse = IReadOnlyList<QuestionsSetWithNumOfQuestionsDto>;

public class ListQuestionsSetsQuery : IRequest<ListQuestionsSetsQueryResponse>
{
   /// <summary>
   /// Number of sets to skip
   /// </summary>
   public int? Offset { get; init; } = null;
   
   /// <summary>
   /// Number of sets to get
   /// </summary>
   public int? Limit { get; init; } = null;

   public class ListQuestionsSetsQueryHandler : IRequestHandler<ListQuestionsSetsQuery, ListQuestionsSetsQueryResponse>
   {
      private readonly IReadOnlyRepositoryAsync<QuestionsSet> _questionsSetRoRepository;
      private readonly IMapper _mapper;

      public ListQuestionsSetsQueryHandler(IReadOnlyRepositoryAsync<QuestionsSet> questionsSetRoRepository, IMapper mapper)
      {
         _questionsSetRoRepository = questionsSetRoRepository;
         _mapper = mapper;
      }

      public async Task<ListQuestionsSetsQueryResponse> Handle(ListQuestionsSetsQuery request, CancellationToken cancellationToken)
      {
         var specification = new FetchAllEntitiesSpecification<QuestionsSet>();
         if (request.Offset != null)
         {
            specification.Skip(request.Offset.Value);
         }

         if (request.Limit != null)
         {
            specification.Take(request.Limit.Value);
         }
         specification.AddInclude(qs => qs.Questions); // required for number of questions

         var questionsSets = await _questionsSetRoRepository
            .ListAsync(specification, cancellationToken);
         
         return _mapper.Map<ListQuestionsSetsQueryResponse>(questionsSets);
      }
   }
}

using MedNet.Domain.Entities;
using MedNet.Domain.Specifications;

namespace MedNet.Application.Specifications.QuestionSpecifications;

public class FetchQuestionsInQuestionsSetIdExceptIdsSpecification : BaseSpecification<Question>
{
    public FetchQuestionsInQuestionsSetIdExceptIdsSpecification(int qsId, IEnumerable<int> exceptIds) : base(q => q.ParentQuestionsSetId == qsId && !exceptIds.Contains(q.Id))
    {
    }
}

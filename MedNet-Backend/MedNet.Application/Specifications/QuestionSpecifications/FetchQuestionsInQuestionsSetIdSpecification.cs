using MedNet.Domain.Entities;
using MedNet.Domain.Specifications;

namespace MedNet.Application.Specifications.QuestionSpecifications;

public class FetchQuestionsInQuestionsSetIdSpecification : BaseSpecification<Question>
{
    public FetchQuestionsInQuestionsSetIdSpecification(int qsId) : base(q => q.ParentQuestionsSetId == qsId)
    {
    }
}

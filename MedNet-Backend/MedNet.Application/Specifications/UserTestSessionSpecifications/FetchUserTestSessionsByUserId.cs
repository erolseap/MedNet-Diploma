using MedNet.Domain.Entities;
using MedNet.Domain.Specifications;

namespace MedNet.Application.Specifications.UserTestSessionSpecifications;

public class FetchUserTestSessionsByUserId : BaseSpecification<UserTestSession>
{
    public FetchUserTestSessionsByUserId(int userId) : base(s => s.UserId == userId)
    {
    }
}

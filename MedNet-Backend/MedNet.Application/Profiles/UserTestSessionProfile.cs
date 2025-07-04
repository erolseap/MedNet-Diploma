using AutoMapper;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;

namespace MedNet.Application.Profiles;

public class UserTestSessionProfile : Profile
{
    public UserTestSessionProfile()
    {
        CreateMap<UserTestSession, UserTestSessionDto>()
            .ConstructUsing(src => new UserTestSessionDto(src.Id, src.QuestionsSetId, src.CreationDate,
                src.Questions.Count, src.Questions.Count(q => q.IsCorrectlyAnswered)));
    }
}

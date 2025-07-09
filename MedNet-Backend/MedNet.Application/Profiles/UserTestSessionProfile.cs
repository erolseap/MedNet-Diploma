using AutoMapper;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;

namespace MedNet.Application.Profiles;

public class UserTestSessionProfile : Profile
{
    public UserTestSessionProfile()
    {
        CreateMap<UserTestSession, UserTestSessionDto>()
            .ConstructUsing(src => new UserTestSessionDto(src.Id, null!, src.CreationDate,
                src.Questions.Count, src.Questions.Count(q => q.IsCorrectlyAnswered)))
            .ForMember(dest => dest.ParentQuestionsSet,
                opt => opt.MapFrom((src, _, _, context) =>
                    context.Mapper.Map<QuestionsSetWithNumOfQuestionsDto>(src.QuestionsSet))
            );
    }
}

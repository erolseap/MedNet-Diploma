using AutoMapper;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;

namespace MedNet.Application.Profiles;

public class UserTestSessionQuestionProfile : Profile
{
    public UserTestSessionQuestionProfile()
    {
        CreateMap<UserTestSessionQuestion, UserTestSessionQuestionDto>()
            .ConstructUsing(src => new UserTestSessionQuestionDto(src.Id, src.Question!.Body, src.Question!.BlankQuestionNumber, new List<AnswerDto>(), src.AnswerId, null))
            .ForMember(dest => dest.Answers,
                opt => opt.MapFrom((src, _, _, context) =>
                    context.Mapper.Map<IReadOnlyList<AnswerWithoutStatusDto>>(src.Question!.Answers))
            )
            .ForMember(dest => dest.CorrectAnswerId,
                opt => opt.MapFrom((src, _, _, context) =>
                    src.AnswerId == null ? (int?)null : src.Question!.CorrectAnswerId
                )
            );
    }
}

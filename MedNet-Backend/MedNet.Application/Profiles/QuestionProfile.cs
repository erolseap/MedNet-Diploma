using AutoMapper;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;

namespace MedNet.Application.Profiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<Question, QuestionDto>()
            .ConstructUsing(src => new QuestionDto(src.Id, src.Body, src.BlankQuestionNumber, new List<AnswerDto>()))
            .ForMember(dest => dest.Answers,
                opt => opt.MapFrom((src, _, _, context) =>
                    context.Mapper.Map<IReadOnlyList<AnswerDto>>(src.Answers))
            );

        CreateMap<Question, QuestionWithoutAnswerStatusDto>()
            .ConstructUsing(src =>
                new QuestionWithoutAnswerStatusDto(src.Id, src.Body, new List<AnswerWithoutStatusDto>()))
            .ForMember(dest => dest.Answers,
                opt => opt.MapFrom((src, _, _, context) =>
                    context.Mapper.Map<IReadOnlyList<AnswerWithoutStatusDto>>(src.Answers))
            );
    }
}

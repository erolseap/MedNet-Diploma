using AutoMapper;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;

namespace MedNet.Application.Profiles;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<Answer, AnswerDto>()
            .ConstructUsing(src => new AnswerDto(src.Id, src.Body, src.IsCorrect))
            .ReverseMap();
        
        CreateMap<Answer, AnswerWithoutStatusDto>()
            .ConstructUsing(src => new AnswerWithoutStatusDto(src.Id, src.Body));
    }
}

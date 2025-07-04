using AutoMapper;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;

namespace MedNet.Application.Profiles;

public class QuestionsSetProfile : Profile
{
    public QuestionsSetProfile()
    {
        CreateMap<QuestionsSet, QuestionsSetWithNumOfQuestionsDto>()
            .ConstructUsing(src => new QuestionsSetWithNumOfQuestionsDto(src.Id, src.Name, src.Questions.Count));
    }
}

using MedNet.Application.DTOs;
using MedNet.Domain.Entities;

namespace MedNet.WebApi.DTOs;

public class QuestionControllerUpdateDto
{
    public string? Body { get; init; }
    public ICollection<AnswerDto>? Answers { get; init; }
}

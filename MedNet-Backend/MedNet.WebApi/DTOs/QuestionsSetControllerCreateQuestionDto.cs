using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MedNet.Application.DTOs;
using MedNet.Domain.Entities;

namespace MedNet.WebApi.DTOs;

public class QuestionsSetControllerCreateQuestionDto
{
    [Required]
    [NotNull]
    public string? Body { get; init; }

    public int BlankQuestionNumber { get; init; } = 0;
    
    public ICollection<AnswerDto> Answers { get; init; } = [];
}

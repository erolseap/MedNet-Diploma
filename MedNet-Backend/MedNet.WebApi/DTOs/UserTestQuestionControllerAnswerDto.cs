using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MedNet.WebApi.DTOs;

public class UserTestQuestionControllerAnswerDto
{
    [Required]
    [NotNull]
    public int? AnswerId { get; init; }
}

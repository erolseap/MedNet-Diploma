using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MedNet.WebApi.DTOs;

public class UserTestsControllerGenerateDto
{
    [Required]
    [NotNull]
    public int? SetId { get; init; }

    public int NumOfQuestions { get; init; } = 50;
}

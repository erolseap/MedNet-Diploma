using MedNet.Domain.Entities;

namespace MedNet.Application.DTOs;

public record QuestionWithoutAnswerStatusDto(int Id, string Body, IReadOnlyList<AnswerWithoutStatusDto> Answers);

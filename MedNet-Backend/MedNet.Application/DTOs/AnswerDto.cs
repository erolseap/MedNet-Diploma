namespace MedNet.Application.DTOs;

public record AnswerDto(int Id, string Body, bool IsCorrect) : AnswerWithoutStatusDto(Id, Body);

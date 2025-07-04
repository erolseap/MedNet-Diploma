namespace MedNet.Application.DTOs;

public record UserTestSessionQuestionDto(int Id, string Body, int BlankQuestionNumber, IReadOnlyList<AnswerWithoutStatusDto> Answers, int? SelectedAnswerId, int? CorrectAnswerId);

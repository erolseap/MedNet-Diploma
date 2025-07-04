namespace MedNet.Application.DTOs;

public record QuestionDto(int Id, string Body, int BlankQuestionNumber, IReadOnlyList<AnswerDto> Answers);

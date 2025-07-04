namespace MedNet.Application.DTOs;

public record UserTestSessionDto(int Id, int ParentQuestionsSetId, DateTime CreationDate, int NumOfQuestions, int CorrectAnswersCount);

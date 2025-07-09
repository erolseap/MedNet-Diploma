namespace MedNet.Application.DTOs;

public record UserTestSessionDto(int Id, QuestionsSetWithNumOfQuestionsDto ParentQuestionsSet, DateTime CreationDate, int NumOfQuestions, int CorrectAnswersCount);

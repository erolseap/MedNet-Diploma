namespace MedNet.WebApi.DTOs;

public class QuestionsSetControllerGetQuestionsDto
{
    /// <summary>
    /// Number of questions to skip
    /// </summary>
    public int? Offset { get; init; } = null;
   
    /// <summary>
    /// Number of questions to get
    /// </summary>
    public int? Limit { get; init; } = null;
}

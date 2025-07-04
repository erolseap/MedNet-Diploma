using MedNet.Domain.Models;

namespace MedNet.Domain.Entities;

public class UserTestSessionQuestion : BaseKeyedEntity
{
    public bool IsCorrectlyAnswered => Answer != null && Answer.IsCorrect;

    #region DB Properties
    /// <summary>An id of user test session</summary>
    public int SessionId { get; set; }

    /// <summary>An id of question id</summary>
    public int QuestionId { get; set; }

    /// <summary>An id of answer the user has passed in</summary>
    public int? AnswerId { get; set; } = null;
    #endregion
    
    #region Relations
    /// <summary>User test session entity</summary>
    public UserTestSession? Session { get; set; }

    /// <summary>Question entity itself</summary>
    public Question? Question { get; set; }
    
    /// <summary>Answer entity itself the user has passed in</summary>
    public Answer? Answer { get; set; }
    #endregion
}

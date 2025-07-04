using MedNet.Domain.Models;

namespace MedNet.Domain.Entities;

/**
 * This is an Answer entity, which is going to be a child entity inside of Question one
 */
public class Answer : BaseKeyedEntity
{
    #region DB Properties
    /// <summary>A body of the answer. Markdown can be used here too</summary>
    public required string Body { get; set; }

    /// <summary>Whether the specified answer is correct or not</summary>
    public bool IsCorrect { get; set; } = false;

    /// <summary>A foreign key of ParentQuestion property</summary>
    public int ParentQuestionId { get; set; } = 0;
    #endregion
    
    #region Relations
    /// <summary>A parent question holding a reference to this answer</summary>
    public Question? ParentQuestion { get; set; } = null;
    #endregion
}

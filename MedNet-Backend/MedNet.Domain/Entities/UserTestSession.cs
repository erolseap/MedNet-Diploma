using MedNet.Domain.Interfaces;
using MedNet.Domain.Models;

namespace MedNet.Domain.Entities;

public class UserTestSession : BaseKeyedEntity
{
    #region DB Properties
    public int UserId { get; set; }
    public int QuestionsSetId { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    #endregion
    
    #region Relations
    public IAppUser? User { get; set; }
    public QuestionsSet? QuestionsSet { get; set; }
    public ICollection<UserTestSessionQuestion> Questions { get; set; } = [];
    #endregion
}

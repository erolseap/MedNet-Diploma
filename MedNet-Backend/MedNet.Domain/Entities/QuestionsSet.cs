using MedNet.Domain.Models;

namespace MedNet.Domain.Entities;

public class QuestionsSet : BaseKeyedEntity
{
    #region DB Properties
    public required string Name { get; set; }
    #endregion

    #region Relations
    public ICollection<Question> Questions { get; set; } = [];
    #endregion
}

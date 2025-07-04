using MedNet.Domain.Models;

namespace MedNet.Domain.Entities;

public class Question : BaseKeyedEntity
{
    /// <summary>Get the correct answer id</summary>
    public int CorrectAnswerId => Answers.FirstOrDefault(a => a.IsCorrect)?.Id ?? 0;

    #region DB Properties
    /// <summary>A number of this question inside gov exams. Should stay zero if that's some custom question</summary>
    public int BlankQuestionNumber { get; set; } = 0;

    /// <summary>A body of the question. Markdown can be used here too</summary>
    public required string Body { get; set; }

    /// <summary>Parent questions set id</summary>
    public int ParentQuestionsSetId { get; set; } = 0;
    #endregion

    #region Relations
    /// <summary>List of answers of this question</summary>
    public ICollection<Answer> Answers { get; set; } = [];

    /// <summary>A parent questions set</summary>
    public QuestionsSet? ParentQuestionsSet { get; set; } = null;
    #endregion
}

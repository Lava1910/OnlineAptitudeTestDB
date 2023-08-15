using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class Question
{
    public int QuestionId { get; set; }

    public int TopicId { get; set; }

    public string ContentQuestion { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int DifficultyLevel { get; set; }

    public DateTime? TimeStamp { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();

    public virtual QuestionTopic Topic { get; set; } = null!;
}

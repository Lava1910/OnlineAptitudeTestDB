using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class QuestionTopic
{
    public int TopicId { get; set; }

    public string TopicName { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}

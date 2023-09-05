using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class QuestionType
{
    public int TypeId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}

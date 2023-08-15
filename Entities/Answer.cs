using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class Answer
{
    public int Id { get; set; }

    public string ContentAnswer { get; set; } = null!;

    public int QuestionId { get; set; }

    public bool CorrectAnswer { get; set; }

    public virtual Question Question { get; set; } = null!;
}

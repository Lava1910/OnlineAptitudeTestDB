using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class TestQuestion
{
    public int Id { get; set; }

    public int TestCode { get; set; }

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual Test TestCodeNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class CandidateAnswer
{
    public int Id { get; set; }

    public int CandidateTestId { get; set; }

    public int QuestionId { get; set; }

    public int TestCode { get; set; }

    public string ContentCandidateAnswer { get; set; } = null!;

    public virtual CandidateTestDetail CandidateTest { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual Test TestCodeNavigation { get; set; } = null!;
}

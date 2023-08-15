using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class CandidateTestDetail
{
    public int Id { get; set; }

    public int TestCode { get; set; }

    public int CandidateId { get; set; }

    public string CandidateAnswer { get; set; } = null!;

    public double Score { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual Test TestCodeNavigation { get; set; } = null!;
}

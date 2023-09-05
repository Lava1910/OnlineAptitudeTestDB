using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class CandidateTestDetail
{
    public int Id { get; set; }

    public int TestCode { get; set; }

    public int CandidateId { get; set; }

    public int ScoreTopic1 { get; set; }

    public int ScoreTopic2 { get; set; }

    public int ScoreTopic3 { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; } = new List<CandidateAnswer>();

    public virtual Test TestCodeNavigation { get; set; } = null!;
}

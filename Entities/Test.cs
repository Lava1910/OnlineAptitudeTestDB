using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class Test
{
    public int TestCode { get; set; }

    public int TimeToDo { get; set; }

    public DateTime TimeStart { get; set; }

    public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; } = new List<CandidateAnswer>();

    public virtual ICollection<CandidateTestDetail> CandidateTestDetails { get; set; } = new List<CandidateTestDetail>();

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
}

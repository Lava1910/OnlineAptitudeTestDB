using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class Candidate
{
    public int CandidateId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string? Phone { get; set; }

    public string? EducationDetails { get; set; }

    public string? WorkExperience { get; set; }

    public int Status { get; set; }

    public int? ScoreFinal { get; set; }

    public DateTime DisabledUntil { get; set; }

    public virtual ICollection<CandidateTestDetail> CandidateTestDetails { get; set; } = new List<CandidateTestDetail>();
}

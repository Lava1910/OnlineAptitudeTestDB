using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class Candidate
{
    public int CandidateId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string? Phone { get; set; }

    public string? EducationDetails { get; set; }

    public string? WorkExperience { get; set; }

    public int? Pass { get; set; }

    public double? ScoreFinal { get; set; }

    public virtual ICollection<CandidateTestDetail> CandidateTestDetails { get; set; } = new List<CandidateTestDetail>();

    public virtual Role Role { get; set; } = null!;
}

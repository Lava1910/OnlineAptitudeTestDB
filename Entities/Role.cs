using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<AdminManager> AdminManagers { get; set; } = new List<AdminManager>();

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
}

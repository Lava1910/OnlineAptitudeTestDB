using System;
using System.Collections.Generic;

namespace OnlineAptitudeTestDB.Entities;

public partial class AdminManager
{
    public int AdminManagerId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string? Phone { get; set; }
}

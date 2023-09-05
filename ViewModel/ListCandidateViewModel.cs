namespace OnlineAptitudeTestDB.ViewModel
{
    public class ListCandidateViewModel
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string Birthday { get; set; } = null!;

        public string? Phone { get; set; }

        public string? EducationDetails { get; set; }

        public string? WorkExperience { get; set; }

        public double? ScoreFinal { get; set; }

        public int StatusId { get; set; }

        public DateTime DisabledUntil { get; set; }
    }
}

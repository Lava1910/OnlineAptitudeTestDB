namespace OnlineAptitudeTestDB.Dto.Candidate
{
    public class CandidateUpdateRequest
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public DateTime Birthday { get; set; }

        public string? Phone { get; set; }

        public string? EducationDetails { get; set; }

        public string? WorkExperience { get; set; }
    }
}

namespace OnlineAptitudeTestDB.Dto.Question
{
    public class QuestionUpdateRequest
    {
        public int TopicId { get; set; }

        public string ContentQuestion { get; set; } = null!;

        public int DifficultyLevel { get; set; }
        public string Type { get; set; } = null!;
        public List<string> ContentAnswer { get; set; } = null!;

        public List<string>? CorrectAnswers { get; set; }
    }
}

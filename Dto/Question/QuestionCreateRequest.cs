namespace OnlineAptitudeTestDB.Dto.Question
{
    public class QuestionCreateRequest
    {
        public string TopicName { get; set; } = null!;

        public string ContentQuestion { get; set; } = null!;

        public int DifficultyLevel { get; set; }
        public string Type { get; set; } = null!;

        public List<string> ContentAnswers { get; set; } = null!;

        public List<bool> CorrectAnswers { get; set; } = null!;

    }
}

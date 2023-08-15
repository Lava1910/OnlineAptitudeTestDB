namespace OnlineAptitudeTestDB.Dto.Question
{
    public class QuestionCreateRequest
    {
        public int TopicId { get; set; }

        public string ContentQuestion { get; set; } = null!;

        public string Type { get; set; } = null!;

        public int DifficultyLevel { get; set; }

        public List<string> ContentAnswers { get; set; } = null!;

        public List<bool> CorrectAnswers { get; set; } = null!;

    }
}

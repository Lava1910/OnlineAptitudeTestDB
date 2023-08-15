namespace OnlineAptitudeTestDB.ViewModel
{
    public class QuestionViewModel
    {
        public string TopicName { get; set; } = null!;

        public string ContentQuestion { get; set; } = null!;

        public int DifficultyLevel { get; set; }
        public string Type { get; set; } = null!;
        public List<string> ContentAnswer { get; set; } = null!;

        public List<string>? CorrectAnswers { get; set; }
        
    }
}

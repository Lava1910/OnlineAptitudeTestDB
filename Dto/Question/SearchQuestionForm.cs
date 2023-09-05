namespace OnlineAptitudeTestDB.Dto.Question
{
    public class SearchQuestionForm
    {
        public string? Search {get; set;}
        public int? TypeId { get; set; }
        public int? TopicId { get; set; }
        public int? DifficultyLevel { get; set; }
    }
}

namespace OnlineAptitudeTestDB.Dto.Test
{
    public class CandidateAnswerRequest
    {
        public int TopicId { get; set; }
        public int TestCode { get; set; }
        public int QuestionId { get; set; }
        public List<string> ContentAnswers { get; set; } = null!;
    }
}

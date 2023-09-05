namespace OnlineAptitudeTestDB.Dto.Test
{
    public class ContentCandidateAnswerRequest
    {
        public int QuestionId { get; set; }
        public List<int> ContentAnswerIds { get; set; } = null!;
    }
}
    
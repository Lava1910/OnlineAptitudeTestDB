﻿namespace OnlineAptitudeTestDB.ViewModel
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string TopicName { get; set; } = null!;

        public string ContentQuestion { get; set; } = null!;
        public List<string> ContentAnswer { get; set; } = null!;

    }
}

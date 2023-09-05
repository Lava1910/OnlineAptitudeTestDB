using OnlineAptitudeTestDB.Dto.Test;
using OnlineAptitudeTestDB.ViewModel;

namespace OnlineAptitudeTestDB.Interfaces
{
    public interface ITestService
    {
        Task<List<QuestionViewModel>> CreateTest();

        Task<int> Score(CandidateAnswerRequest request);
        Task<List<string>> CorrectAnswer(int questionId);
    }
}

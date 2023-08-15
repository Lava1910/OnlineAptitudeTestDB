using OnlineAptitudeTestDB.Dto.Question;
using OnlineAptitudeTestDB.ViewModel;

namespace OnlineAptitudeTestDB.Interfaces
{
    public interface IManageQuestionService
    {
        Task<List<QuestionViewModel>> GetAll();
        Task<List<QuestionViewModel>> SearchByDifficulty(int difficultyLevel);
        Task<List<QuestionViewModel>> SearchByTopic(string topicName);
        Task<List<QuestionViewModel>> SearchByType(string type);
        Task<List<QuestionViewModel>> Search(string search);
        Task<List<QuestionViewModel>> Searching(string search,int difficultyLevel, string topicName, string type);
        Task<int> Create(QuestionCreateRequest request);
        Task<int> Update(QuestionCreateRequest request);

    }
}

using Microsoft.AspNetCore.Mvc;
using OnlineAptitudeTestDB.Dto.Question;
using OnlineAptitudeTestDB.ViewModel;

namespace OnlineAptitudeTestDB.Interfaces
{
    public interface IManageQuestionService
    {
        Task<List<ListQuestionViewModel>> GetAll();
        Task<List<ListQuestionViewModel>> SearchByDifficulty(int difficultyLevel);
        Task<List<ListQuestionViewModel>> SearchByTopic(string topicName);
        Task<List<ListQuestionViewModel>> SearchByType(string type);
        Task<List<ListQuestionViewModel>> Search(string search);
        Task<List<ListQuestionViewModel>> Searching(SearchQuestionForm request);
        Task<int> Create(QuestionCreateRequest request);
        Task<int> Update(QuestionCreateRequest request);
        bool Delete(int questionId);

    }
}

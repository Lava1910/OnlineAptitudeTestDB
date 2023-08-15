using Microsoft.EntityFrameworkCore;
using OnlineAptitudeTestDB.Dto.Question;
using OnlineAptitudeTestDB.Entities;
using OnlineAptitudeTestDB.Interfaces;
using OnlineAptitudeTestDB.ViewModel;

namespace OnlineAptitudeTestDB.Service
{
    public class ManageQuestionService : IManageQuestionService
    {
        private readonly OnlineAptitudeTestDbContext _context;
        public ManageQuestionService(OnlineAptitudeTestDbContext context)
        {
            _context = context;
        }

        public async Task<List<QuestionViewModel>> GetAll()
        {
            return await _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.Answers)
                .Select(rs => new QuestionViewModel
                {
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type,
                    ContentAnswer = rs.Answers
                        .Select(a => a.ContentAnswer)
                        .ToList(),
                    CorrectAnswers = rs.Answers
                        .Where(a => a.CorrectAnswer)
                        .Select(a => a.ContentAnswer)
                        .ToList()
                }).ToListAsync();
        }

        public async Task<List<QuestionViewModel>> SearchByDifficulty(int difficultyLevel)
        {
            return await _context.Questions
                .Where(q => q.DifficultyLevel == difficultyLevel)
                .Include(q => q.Topic)
                .Include(q => q.Answers)
                .Select(rs => new QuestionViewModel
                {
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type,
                    ContentAnswer = rs.Answers
                        .Select(a => a.ContentAnswer)
                        .ToList(),
                    CorrectAnswers = rs.Answers
                        .Where(a => a.CorrectAnswer)
                        .Select(a => a.ContentAnswer)
                        .ToList()
                }).ToListAsync();
        }

        public async Task<List<QuestionViewModel>> SearchByTopic(string topicName)
        {
            return await _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.Answers)
                .Where(q => q.Topic.TopicName == topicName)
                .Select(rs => new QuestionViewModel
                {
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type,
                    ContentAnswer = rs.Answers
                        .Select(a => a.ContentAnswer)
                        .ToList(),
                    CorrectAnswers = rs.Answers
                        .Where(a => a.CorrectAnswer)
                        .Select(a => a.ContentAnswer)
                        .ToList()
                }).ToListAsync();
            
        }

        public async Task<List<QuestionViewModel>> Search(string search)
        {
            return await _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.Answers)
                .Where(q => q.ContentQuestion.Contains(search.ToLower())
                    || q.Topic.TopicName.Contains(search.ToLower()))
                .Select(rs => new QuestionViewModel
                {
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type,
                    ContentAnswer = rs.Answers
                        .Select(a => a.ContentAnswer)
                        .ToList(),
                    CorrectAnswers = rs.Answers
                        .Where(a => a.CorrectAnswer)
                        .Select(a => a.ContentAnswer)
                        .ToList()
                }).ToListAsync();
        }

        public async Task<int> Create(QuestionCreateRequest request)
        {
            var question = new Question()
            {
                TopicId = request.TopicId,
                ContentQuestion = request.ContentQuestion,
                DifficultyLevel = request.DifficultyLevel,
                Type = request.Type,
                Answers = new List<Answer>()
            };
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            for (int i = 0; i < request.ContentAnswers.Count; i++)
            {
                var answer = new Answer()
                {
                    ContentAnswer = request.ContentAnswers[i],
                    QuestionId = question.QuestionId,
                    CorrectAnswer = request.CorrectAnswers[i]
                };
                _context.Answers.Add(answer);
            }
            await _context.SaveChangesAsync();
            return question.QuestionId;
        }

        public async Task<List<QuestionViewModel>> SearchByType(string type)
        {
            return await _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.Answers)
                .Where(q => q.Type == type)
                .Select(rs => new QuestionViewModel
                {
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type,
                    ContentAnswer = rs.Answers
                        .Select(a => a.ContentAnswer)
                        .ToList(),
                    CorrectAnswers = rs.Answers
                        .Where(a => a.CorrectAnswer)
                        .Select(a => a.ContentAnswer)
                        .ToList()
                }).ToListAsync();
        }

        public async Task<List<QuestionViewModel>> Searching(string search, int difficultyLevel, string topicName, string type)
        {
            var query = _context.Questions;

            if (!string.IsNullOrEmpty(search))
            {
                query = (DbSet<Question>)query.Where(q => q.ContentQuestion.Contains(search.ToLower())
                                     || q.Topic.TopicName.Contains(search.ToLower()));
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = (DbSet<Question>)query.Where(q => q.Type == type);
            }

            if (!string.IsNullOrEmpty(topicName))
            {
                query = (DbSet<Question>)query.Where(q => q.Topic.TopicName == topicName);
            }

            if (difficultyLevel > 0) // Assuming 0 means no filter for difficulty
            {
                query = (DbSet<Question>)query.Where(q => q.DifficultyLevel == difficultyLevel);
            }

            var result = await query
                .Include(q => q.Topic)
                .Include(q => q.Answers)
                .Select(rs => new QuestionViewModel
                {
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type,
                    ContentAnswer = rs.Answers
                        .Select(a => a.ContentAnswer)
                        .ToList(),
                    CorrectAnswers = rs.Answers
                        .Where(a => a.CorrectAnswer)
                        .Select(a => a.ContentAnswer)
                        .ToList()
                }).ToListAsync();

            return result;
        }

        public Task<int> Update(QuestionCreateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
    
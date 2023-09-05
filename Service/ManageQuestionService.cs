using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAptitudeTestDB.Dto.Question;
using OnlineAptitudeTestDB.Entities;
using OnlineAptitudeTestDB.Interfaces;
using OnlineAptitudeTestDB.ViewModel;
using System.Linq;
using System.Net.Mail;

namespace OnlineAptitudeTestDB.Service
{
    public class ManageQuestionService : IManageQuestionService
    {
        private readonly OnlineAptitudeTestDbContext _context;
        public ManageQuestionService(OnlineAptitudeTestDbContext context)
        {
            _context = context;
        }

        public async Task<List<ListQuestionViewModel>> GetAll()
        {
            return await _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.Type)
                .Include(q => q.Answers)
                .Select(rs => new ListQuestionViewModel
                {
                    QuestionId = rs.QuestionId,
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type.Type,
                    ContentAnswer = rs.Answers
                        .Select(a => a.ContentAnswer)
                        .ToList(),
                    CorrectAnswers = rs.Answers
                        .Where(a => a.CorrectAnswer)
                        .Select(a => a.ContentAnswer)
                        .ToList()
                }).ToListAsync();
        }

        public async Task<List<ListQuestionViewModel>> SearchByDifficulty(int difficultyLevel)
        {
            return await _context.Questions
                .Where(q => q.DifficultyLevel == difficultyLevel)
                .Include(q => q.Topic)
                .Include(q => q.Type)
                .Include(q => q.Answers)
                .Select(rs => new ListQuestionViewModel
                {
                    QuestionId = rs.QuestionId,
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type.Type,
                    ContentAnswer = rs.Answers
                        .Select(a => a.ContentAnswer)
                        .ToList(),
                    CorrectAnswers = rs.Answers
                        .Where(a => a.CorrectAnswer)
                        .Select(a => a.ContentAnswer)
                        .ToList()
                }).ToListAsync();
        }

        public async Task<List<ListQuestionViewModel>> SearchByTopic(string topicName)
        {
            return await _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.Type)
                .Include(q => q.Answers)
                .Where(q => q.Topic.TopicName == topicName)
                .Select(rs => new ListQuestionViewModel
                {
                    QuestionId = rs.QuestionId,
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type.Type,
                    ContentAnswer = rs.Answers
                        .Select(a => a.ContentAnswer)
                        .ToList(),
                    CorrectAnswers = rs.Answers
                        .Where(a => a.CorrectAnswer)
                        .Select(a => a.ContentAnswer)
                        .ToList()
                }).ToListAsync();
            
        }

        public async Task<List<ListQuestionViewModel>> Search(string search)
        {
            return await _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.Type)
                .Include(q => q.Answers)
                .Where(q => q.ContentQuestion.Contains(search.ToLower())
                    || q.Topic.TopicName.Contains(search.ToLower()))
                .Select(rs => new ListQuestionViewModel
                {
                    QuestionId= rs.QuestionId,
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type.Type,
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
            //var correctAnswerCount = request.CorrectAnswers.Count(correct => correct);
            var question = new Question()
            {
                TopicId = _context.QuestionTopics.First(t => t.TopicName == request.TopicName).TopicId,
                ContentQuestion = request.ContentQuestion,
                DifficultyLevel = request.DifficultyLevel,
                //Type = correctAnswerCount == 1 ? "checkbox" : "radio",
                TypeId = _context.QuestionTypes.First(t => t.Type == request.Type).TypeId,
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

        public async Task<List<ListQuestionViewModel>> SearchByType(string type)
        {
            return await _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.Type)
                .Include(q => q.Answers)
                .Where(q => q.Type.Type == type)
                .Select(rs => new ListQuestionViewModel
                {
                    TopicName = rs.Topic.TopicName,
                    ContentQuestion = rs.ContentQuestion,
                    DifficultyLevel = rs.DifficultyLevel,
                    Type = rs.Type.Type,
                    ContentAnswer = rs.Answers
                        .Select(a => a.ContentAnswer)
                        .ToList(),
                    CorrectAnswers = rs.Answers
                        .Where(a => a.CorrectAnswer)
                        .Select(a => a.ContentAnswer)
                        .ToList()
                }).ToListAsync();
        }

        public async Task<List<ListQuestionViewModel>> Searching([FromQuery]SearchQuestionForm search)
        {
            //return await _context.Questions
            //     .Include(q => q.Topic)
            //     .Include(q => q.Answers)
            //     .Where(q => q.ContentQuestion.Contains(request.Search.ToLower())
            //        || q.Topic.TopicName.Contains(request.Search.ToLower()))
            //     .Where(q => q.TypeId == request.TypeId)
            //     .Where(q => q.DifficultyLevel == request.DifficultyLevel)
            //     .Where(q => q.TopicId == request.TopicId)
            //     .Select(rs => new ListQuestionViewModel
            //     {
            //         QuestionId = rs.QuestionId,
            //         TopicName = rs.Topic.TopicName,
            //         ContentQuestion = rs.ContentQuestion,
            //         DifficultyLevel = rs.DifficultyLevel,
            //         Type = rs.Type.Type,
            //         ContentAnswer = rs.Answers
            //             .Select(a => a.ContentAnswer)
            //             .ToList(),
            //         CorrectAnswers = rs.Answers
            //             .Where(a => a.CorrectAnswer)
            //             .Select(a => a.ContentAnswer)
            //             .ToList()
            //     }).ToListAsync();
                var query = _context.Questions.AsQueryable();

                if (!string.IsNullOrEmpty(search.Search))
                {
                    query = query.Where(q => q.ContentQuestion.Contains(search.Search));
                }

                if (search.TopicId.HasValue)
                {
                    query = query.Where(q => q.TopicId == search.TopicId);
                }

                if (search.TypeId.HasValue)
                {
                    query = query.Where(q => q.TypeId == search.TypeId);
                }

                if (search.DifficultyLevel.HasValue)
                {
                    query = query.Where(q => q.DifficultyLevel == search.DifficultyLevel);
                }

                var result = await query
                    .Select(rs => new ListQuestionViewModel
                    {
                        QuestionId = rs.QuestionId,
                        TopicName = rs.Topic.TopicName,
                        ContentQuestion = rs.ContentQuestion,
                        DifficultyLevel = rs.DifficultyLevel,
                        Type = rs.Type.Type,
                        ContentAnswer = rs.Answers.Select(a => a.ContentAnswer).ToList(),
                        CorrectAnswers = rs.Answers
                            .Where(a => a.CorrectAnswer)
                            .Select(a => a.ContentAnswer)
                            .ToList()
                    })
                    .ToListAsync();
                return result;
        }

        public Task<int> Update(QuestionCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int questionId)
        {
            var deleteQuestion = _context.Questions
                .First(q => q.QuestionId == questionId);
            var deleteAnswer = _context.Answers
                .Where(q => q.QuestionId == questionId);
            if (deleteQuestion == null)
            {
                return false;
            }
            _context.Answers.RemoveRange(deleteAnswer);
            _context.Questions.Remove(deleteQuestion);
            _context.SaveChanges();
            return true;

        }
    }
}
    
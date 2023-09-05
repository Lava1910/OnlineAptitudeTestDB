using Azure.Core;
using Microsoft.EntityFrameworkCore;
using OnlineAptitudeTestDB.Dto.Candidate;
using OnlineAptitudeTestDB.Dto.Test;
using OnlineAptitudeTestDB.Entities;
using OnlineAptitudeTestDB.Interfaces;
using OnlineAptitudeTestDB.ViewModel;
using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace OnlineAptitudeTestDB.Service
{
    public class TestService : ITestService
    {
        private readonly OnlineAptitudeTestDbContext _context;
        private readonly Random _random;
        public TestService(OnlineAptitudeTestDbContext context, Random random)
        {
            _context = context;
            _random = random;
        }

        private string GenerateRandomTestCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder randomTestCode = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                randomTestCode.Append(chars[_random.Next(chars.Length)]);
            }

            return randomTestCode.ToString();
        }
        public async Task<List<QuestionViewModel>> CreateTest()
        {
            int[] topicIds = new int[3] { 1, 2, 3, };
            int[] difficultLevels = new int[2] { 1, 2 };
            List<QuestionViewModel> questions = new List<QuestionViewModel>();
            foreach( int topicId in topicIds)
            {
                foreach(int difficultLevel in difficultLevels)
                {
                    var questionsRandom = await _context.Questions
                    .Include(q => q.Answers)
                    .Include(q => q.Topic)
                    .Where(q => q.TopicId == topicId)
                    .Where(q => q.DifficultyLevel == difficultLevel)
                    .ToListAsync();
                    questionsRandom = questionsRandom.OrderBy(q => _random.Next()).Take(2).ToList();
                    for (int i = 0; i < questionsRandom.Count; i++)
                    {
                        questions.Add(new QuestionViewModel
                        {
                            QuestionId = questionsRandom[i].QuestionId,
                            TopicName = questionsRandom[i].Topic.TopicName,
                            ContentQuestion = questionsRandom[i].ContentQuestion,
                            ContentAnswer = questionsRandom[i].Answers
                            .Select(a => a.ContentAnswer)
                            .ToList()
                        });
                    }                   
                }
                var questionsRandomLv3 = await _context.Questions
                        .Include(q => q.Answers)
                        .Include(q => q.Topic)
                        .Where(q => q.TopicId == topicId)
                        .Where(q => q.DifficultyLevel == 3)
                        .ToListAsync();
                questionsRandomLv3 = questionsRandomLv3.OrderBy(q => _random.Next()).Take(1).ToList();
                questions.Add(new QuestionViewModel
                {
                    QuestionId = questionsRandomLv3[0].QuestionId,
                    TopicName = questionsRandomLv3[0].Topic.TopicName,
                    ContentQuestion = questionsRandomLv3[0].ContentQuestion,
                    ContentAnswer = questionsRandomLv3[0].Answers
                        .Select(a => a.ContentAnswer)
                        .ToList()
                });
            }
            var test = new Test()
            {
                TimeToDo = 30*60,
                TimeStart = DateTime.Now
            };
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            for (int i = 0; i < questions.Count; i++)
            {
                var testQuestion = new TestQuestion()
                {
                    TestCode = test.TestCode,
                    QuestionId = questions[i].QuestionId
                };
                _context.TestQuestions.Add(testQuestion);              
            }
            await _context.SaveChangesAsync();
            return questions;          
        }

        public async Task<int> Score(CandidateAnswerRequest request)
        {
            int totalScore = 0;

                var correctAnswers = await _context.Answers
                .Where(a => a.QuestionId == request.QuestionId)
                .Where(a => a.CorrectAnswer)
                .Select(a => a.ContentAnswer)
                .ToListAsync();

                if (correctAnswers != null)
                {
                    bool isAnswerCorrect = CheckAnswersMatch(correctAnswers, request.ContentAnswers);
                    if (isAnswerCorrect)
                    {
                        totalScore = 1;
                    }
                }
            
            return totalScore;
        }

        private bool CheckAnswersMatch(List<string> correctAnswer, List<string> candidateAnswer)
        {
            return correctAnswer.SequenceEqual(candidateAnswer);
        }

        public async Task<List<string>> CorrectAnswer(int questionId)
        {
            var correctAnswers = await _context.Answers
                .Where(a => a.QuestionId == questionId)
                .Where(a => a.CorrectAnswer)
                .Select(a => a.ContentAnswer)
                .ToListAsync();
            return correctAnswers;
        }
    }
}


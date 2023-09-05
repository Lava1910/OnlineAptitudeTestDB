using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAptitudeTestDB.Dto.Test;
using OnlineAptitudeTestDB.Entities;
using OnlineAptitudeTestDB.Interfaces;
using OnlineAptitudeTestDB.Service;
using OnlineAptitudeTestDB.ViewModel;

namespace OnlineAptitudeTestDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("get-questions")]
        public async Task<List<QuestionViewModel>> GetQuestions()
        {
            var questions = await _testService.CreateTest();
            return questions;
        }

        [HttpPost("score")]
        public async Task<IActionResult> CalculateScore(CandidateAnswerRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid input data");
            }
            int score = await _testService.Score(request);
            return Ok(score);
        }

        [HttpGet("abc")]
        public async Task<IActionResult> GetCorrectAnswer(int questionId)
        {
            List<string> correctAnswer = await _testService.CorrectAnswer(questionId);
            return Ok(correctAnswer);
        }

    }
}

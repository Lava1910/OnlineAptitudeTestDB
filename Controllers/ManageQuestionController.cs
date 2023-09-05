using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAptitudeTestDB.Dto.Question;
using OnlineAptitudeTestDB.Interfaces;
using OnlineAptitudeTestDB.ViewModel;

namespace OnlineAptitudeTestDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ManageQuestionController : ControllerBase
    {
        private readonly IManageQuestionService _manageQuestionService;
        public ManageQuestionController(IManageQuestionService questionService)
        {
            _manageQuestionService = questionService;
        }

        [HttpGet("getAll")]
        public async Task<List<ListQuestionViewModel>> GetAll()
        {
            //limit = limit != null ? limit : 10;
            //page = page != null ? page : 1;
            //int offset = (int)((page - 1) * limit);
            var questions = await _manageQuestionService.GetAll();
            //List<ListQuestionViewModel> selectedQuestions = questions.Skip(offset).Take((int)limit).ToList();
            return questions;
        }

        [HttpGet("searchByDifficulty")]
        public async Task<List<ListQuestionViewModel>> SearchByDifficulty(int difficultyLevel)
        {
            var questions = await _manageQuestionService.SearchByDifficulty(difficultyLevel);
            return questions;
        }

        [HttpGet("searchByTopic")]
        public async Task<List<ListQuestionViewModel>> SearchByTopic(string topicName)
        {
            var questions = await _manageQuestionService.SearchByTopic(topicName);
            return questions;
        }

        [HttpGet("search/{search}")]
        public async Task<List<ListQuestionViewModel>> Search(string search)
        {
            var questions = await _manageQuestionService.Search(search);
            return questions;
        }

        [HttpGet("searchByType")]
        public async Task<List<ListQuestionViewModel>> SearchByType(string type)
        {
            var questions = await _manageQuestionService.SearchByType(type);
            return questions;
        }

        [HttpGet("searching")]
        public async Task<List<ListQuestionViewModel>> Searching([FromQuery]SearchQuestionForm request)
        {
            var questions = await _manageQuestionService.Searching(request);
            return questions;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(QuestionCreateRequest request)
        {
            var questionId = await _manageQuestionService.Create(request);
            if (questionId == 0)
            {
                return BadRequest("An error occurred while creating the question.");
            }
            return Ok("Question created successfully.");
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _manageQuestionService.Delete(id);
            if(result)
            {
                return Ok();
            }
            return NoContent();
        }

        
    }
}

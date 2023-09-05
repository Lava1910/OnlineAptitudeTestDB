using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAptitudeTestDB.Dto.Candidate;
using OnlineAptitudeTestDB.Interfaces;
using OnlineAptitudeTestDB.ViewModel;

namespace OnlineAptitudeTestDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageCandidateController : ControllerBase
    {
        private readonly IManageCandidateService _manageCandidateService;
        public ManageCandidateController(IManageCandidateService candidateService)
        {
            _manageCandidateService = candidateService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(CandidateCreateRequest request)
        {
            var candidateId = await _manageCandidateService.Create(request);
            if (candidateId == 0)
            {
                return BadRequest("An error occurred while creating the question.");
            }
            return Ok("Question created successfully.");
        }

        [HttpGet("getAll")]
        public async Task<List<ListCandidateViewModel>> GetAll()
        {
            var candidates = await _manageCandidateService.GetAll();
            return candidates;
        }

        [HttpPut("update-candidate")]
        public async Task<IActionResult> Update(CandidateUpdateRequest request)
        {
            var result = await _manageCandidateService.Update(request);
            if (result > 0)
            {
                return Ok(new { message = "Successful candidate update!" });
            }
            return BadRequest("Candidate update failed!");
        }

        [HttpPut("{id}/active")]
        public IActionResult ActiveUser(int id)
        {
            var result = _manageCandidateService.ActiveUser(id);
            if (result)
            {
                return Ok("Active account!!!");
            }
            else
            {
                return NotFound("No account found to disable.");
            }
        }
    }
}

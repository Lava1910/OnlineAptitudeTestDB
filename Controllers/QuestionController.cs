using Microsoft.AspNetCore.Mvc;
using OnlineAptitudeTestDB.Entities;
using OnlineAptitudeTestDB.Interfaces;

namespace OnlineAptitudeTestDB.Controllers
{
    public class QuestionController : ControllerBase
    {
        private readonly OnlineAptitudeTestDbContext _context;
        public QuestionController(OnlineAptitudeTestDbContext context)
        {
            _context = context;
        }
        
    }
}

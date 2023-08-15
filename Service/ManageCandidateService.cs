using Microsoft.AspNetCore.Mvc;
using OnlineAptitudeTestDB.Entities;
using OnlineAptitudeTestDB.Interfaces;

namespace OnlineAptitudeTestDB.Service
{
    public class ManageCandidateService : IManageCandidateService
    {
        private readonly OnlineAptitudeTestDbContext _context;
        public ManageCandidateService(OnlineAptitudeTestDbContext context)
        {
            _context = context;
        }
        public Task<int> Create(int candidateId)
        {
            return Task.FromResult(0);
        }
    }
}

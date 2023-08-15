using Microsoft.AspNetCore.Mvc;
using System;

namespace OnlineAptitudeTestDB.Interfaces
{
    public interface IManageCandidateService
    {
        Task<int> Create(int candidateId);
    }
}

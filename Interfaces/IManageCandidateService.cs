using OnlineAptitudeTestDB.Dto.Candidate;
using OnlineAptitudeTestDB.Dto.Question;
using OnlineAptitudeTestDB.ViewModel;

namespace OnlineAptitudeTestDB.Interfaces
{
    public interface IManageCandidateService
    {
        Task<List<ListCandidateViewModel>> GetAll();
        Task<int> Create(CandidateCreateRequest request);
        Task<int> Update(CandidateUpdateRequest request);
        bool ActiveUser(int candidateId);
    }
}


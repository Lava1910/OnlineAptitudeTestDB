using Microsoft.EntityFrameworkCore;
using OnlineAptitudeTestDB.Dto.Candidate;
using OnlineAptitudeTestDB.Entities;
using OnlineAptitudeTestDB.Interfaces;
using OnlineAptitudeTestDB.ViewModel;
using System.Net;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;

namespace OnlineAptitudeTestDB.Service
{
    public class ManageCandidateService : IManageCandidateService
    {
        private readonly OnlineAptitudeTestDbContext _context;
        private readonly Random _random;
        public ManageCandidateService(OnlineAptitudeTestDbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task<int> Create(CandidateCreateRequest request)
        {
            string randomPassword = GenerateRandomPassword();
            var candidate = new Candidate()
            {
                Username = request.Email,
                Password = randomPassword,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Gender = request.Gender,
                Birthday = request.Birthday,
                EducationDetails = request.EducationDetails,
                WorkExperience = request.WorkExperience,
                Role = "Candidate",
                Status = 3,
                DisabledUntil = DateTime.UtcNow.AddDays(10)
        };
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            SendEmail(candidate.Username, candidate.Password, candidate.Email);
            return candidate.CandidateId;
        }
        // note
        private static void SendEmail(string username, string password, string email)
        {
            string senderEmail = "webstercompany1234@gmail.com"; // Địa chỉ email của bạn
            string senderPassword = "ecsjuwqkxnjqjffg"; // Mật khẩu email của bạn
            string recipientEmail = email; // Địa chỉ email của người nhận
            string subject = "Thông tin tài khoản";
            string body = $"Tài khoản: {username}\nMật khẩu: {password}";

            var client = new SmtpClient("smtp.gmail.com") // Thay thế bằng thông tin SMTP của nhà cung cấp email
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };

            client.Send(senderEmail, recipientEmail, subject, body);
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder randomUserName = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                randomUserName.Append(chars[_random.Next(chars.Length)]);
            }

            return randomUserName.ToString();
        }

        private string GenerateRandomUserName()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder randomPassword = new StringBuilder();

            for (int i = 0; i < 12; i++)
            {
                randomPassword.Append(chars[_random.Next(chars.Length)]);
            }

            return randomPassword.ToString();
        }
        public async Task<List<ListCandidateViewModel>> GetAll()
        {
            return await _context.Candidates
                .Select(rs => new ListCandidateViewModel
                {
                    Name = rs.Name,
                    Email = rs.Email,
                    Phone = rs.Phone,
                    Gender = rs.Gender,
                    Birthday = rs.Birthday.ToString("dd-MM-yyyy"),
                    EducationDetails = rs.EducationDetails,
                    WorkExperience = rs.WorkExperience,
                    Username = rs.Username,
                    ScoreFinal = rs.ScoreFinal,
                    StatusId = (rs.ScoreFinal == null) ? 3 : ((rs.ScoreFinal >= 10) ? 1 : 0),
                    DisabledUntil = rs.DisabledUntil
                }).ToListAsync();
        }

        public async Task<int> Update(CandidateUpdateRequest request)
        {
            var candidate = new Candidate
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Gender = request.Gender,
                Birthday = request.Birthday,
                EducationDetails = request.EducationDetails,
                WorkExperience = request.WorkExperience,
                Role = "Candidate"
            };
            _context.Candidates.Update(candidate);
            return await _context.SaveChangesAsync(); 
        }

        public bool ActiveUser(int candidateId)
        {
            var candidate = _context.Candidates.Find(candidateId);

            if (candidate == null)
            {
                return false;
            }

            candidate.DisabledUntil = DateTime.UtcNow.AddDays(10);
            _context.SaveChanges();
            return true;
        }
    }
}

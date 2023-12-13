using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume_Management_project.Core.Context;
using Resume_Management_project.Core.Dtos.Candidate;
using Resume_Management_project.Core.Entities;

namespace Resume_Management_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }

        public CandidateController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // CRUD 

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCandidate([FromForm] CandidateCreateDto dto)
        {
            var newCandidate = _mapper.Map<Candidate>(dto);
            await _context.Candidates.AddAsync(newCandidate);
            await _context.SaveChangesAsync();

            return Ok("Candidate Saved Successfully");
        }

        // Read
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<CandidateGetDto>>> GetCandidates()
        {
            var candidates = await _context.Candidates.Include(c => c.Job).OrderByDescending(q => q.CreatedAt).ToListAsync();
            var convertedCandidates = _mapper.Map<IEnumerable<CandidateGetDto>>(candidates);

            return Ok(convertedCandidates);
        }

        [HttpGet]
        [Route("GetByJob")]
        public async Task<ActionResult<IEnumerable<CandidateGetDto>>> GetCandidatesByJobs(int jobId)
        {
            // Получаем список кандидатов для конкретной вакансии
            var candidates = await _context.Candidates
                .Include(c => c.Job)
                .Where(c => c.JobId == jobId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            // Маппим кандидатов на DTO
            var convertedCandidates = _mapper.Map<IEnumerable<CandidateGetDto>>(candidates);

            return Ok(convertedCandidates);
        }

        [HttpGet]
        [Route("GetByCompany")]
        public async Task<ActionResult<IEnumerable<CandidateGetDto>>> GetCandidatesByCompany(int companyId)
        {
            // Получаем список кандидатов для конкретной компании
            var candidates = await _context.Candidates
                .Include(c => c.Job)
                
                .Where(c => c.Job.CompanyId == companyId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            // Маппим кандидатов на DTO
            var convertedCandidates = _mapper.Map<IEnumerable<CandidateGetDto>>(candidates);

            return Ok(convertedCandidates);
        }
    }
}

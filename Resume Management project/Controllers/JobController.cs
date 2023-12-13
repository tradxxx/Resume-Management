using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume_Management_project.Core.Context;
using Resume_Management_project.Core.Dtos.Job;
using Resume_Management_project.Core.Entities;
using System.ComponentModel.Design;

namespace Resume_Management_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public JobController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateJob([FromBody] JobCreateDto dto)
        {
            var newJob = _mapper.Map<Job>(dto);
            await _context.Jobs.AddAsync(newJob);
            await _context.SaveChangesAsync();

            return Ok("Job Created Successfully");
        }

        // Read
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetJobs()
        {
            var jobs = await _context.Jobs.Include(company => company.Company).OrderByDescending(q => q.CreatedAt).ToListAsync();
            var convertdJobs = _mapper.Map<IEnumerable<JobGetDto>>(jobs);

            return Ok(convertdJobs);
        }

        [HttpGet]
        [Route("Get/{companyid}")]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetJobsByCompany(int companyid)
        {
            var jobs = await _context.Jobs
                .Include(j => j.Company)
                .Where(j => j.CompanyId == companyid)
                .OrderByDescending(j => j.CreatedAt)
                .ToListAsync();

            
            var convertedJobs = _mapper.Map<IEnumerable<JobGetDto>>(jobs);

            return Ok(convertedJobs);
        }
    }
}

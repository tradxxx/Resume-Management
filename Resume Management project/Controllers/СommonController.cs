using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume_Management_project.Core.Context;
using Resume_Management_project.Core.Entities;

namespace Resume_Management_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class СommonController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }

        public СommonController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllInfo")]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllInfo()
        {
            var companies = await _context.Companies.ToListAsync();

            return Ok(companies);
        }
    }
}

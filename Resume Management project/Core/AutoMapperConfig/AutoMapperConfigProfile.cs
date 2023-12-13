using AutoMapper;
using Resume_Management_project.Core.Dtos.Candidate;
using Resume_Management_project.Core.Dtos.Company;
using Resume_Management_project.Core.Dtos.Job;
using Resume_Management_project.Core.Entities;

namespace Resume_Management_project.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile() 
        {
            //Company
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<Company, CompanyGetDto>();

            //Job
            CreateMap<JobCreateDto, Job>();
            CreateMap<Job, JobGetDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));

            //Candidate
            CreateMap<CandidateCreateDto, Candidate>();
            CreateMap<Candidate, CandidateGetDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title));
        }
    }
}

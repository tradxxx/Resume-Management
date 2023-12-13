using Resume_Management_project.Core.Enums;

namespace Resume_Management_project.Core.Entities
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        public JobLevel Level { get; set; }

        //Relations
        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
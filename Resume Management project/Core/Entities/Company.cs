using Resume_Management_project.Core.Enums;

namespace Resume_Management_project.Core.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public CompanySize Size { get; set; }

        //Relations
        public virtual ICollection<Job> Jobs { get; set; }
    }
}

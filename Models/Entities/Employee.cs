using System.ComponentModel.DataAnnotations;

namespace AI_Makers_TechAssessment.Models.Entities
{
    public class Employee:BaseClass
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<MobileNumber> MobileNumbers { get; set; } = new HashSet<MobileNumber>();

        public string JobTitle { get; set; } = null!;

        public DateTime HireDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public int DepartmentId { get; set; }
        public string Photo { get; set; } = null!;

        public Department Department { get; set; } = null!;
    }
}

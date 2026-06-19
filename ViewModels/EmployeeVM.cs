using AI_Makers_TechAssessment.Models.Entities;

namespace AI_Makers_TechAssessment.Models.Dto
{
    public record EmployeeVM
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public List<string> MobileNumbers { get; set; } = new();

        public string JobTitle { get; set; } = null!;

        public DateTime HireDate { get; set; } 

        public bool IsActive { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string DepartmentDescription { get; set; } = null!;
        public string Photo { get; set; } = null!;

    }
}

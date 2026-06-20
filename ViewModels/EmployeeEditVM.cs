using AI_Makers_TechAssessment.Models.Dto;
using AI_Makers_TechAssessment.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace AI_Makers_TechAssessment.ViewModels

{
    public class EmployeeEditVM
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100)]
        public string? FullName { get; set; } 

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [StringLength(100)]
        public string? JobTitle { get; set; }

        public string? Photo { get; set; } //نلابول لان الصورة ممكن تكون اتحذفت من السيرفر
        public string? CurrentPhoto { get; set; }
        public IFormFile? PhotoFile { get; set; }

        public List<string> MobileNumbers { get; set; } = new();
    }
}

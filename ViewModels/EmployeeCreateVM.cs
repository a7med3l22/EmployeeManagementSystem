using AI_Makers_TechAssessment.Models.Dto;
using System.ComponentModel.DataAnnotations;
namespace AI_Makers_TechAssessment.ViewModels
{
    public class EmployeeCreateVM
    {
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string? FullName { get; set; } //"الخاصية ممكن تكون null في أي وقت أثناء الـ binding" ف يبتدي يمسكها ويظهر الخطأ

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; } 

        [Range(1, int.MaxValue, ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [StringLength(100, ErrorMessage = "Job Title cannot exceed 100 characters")]
        public string? JobTitle { get; set; } 

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Employee photo is required")]
        public IFormFile? PhotoFile { get; set; }
        public List<string> MobileNumbers { get; set; } = new();
    }

}
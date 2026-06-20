using AI_Makers_TechAssessment.Models.Dto;
using System.ComponentModel.DataAnnotations;
namespace AI_Makers_TechAssessment.ViewModels
{
    public class EmployeeCreateVM
    {
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100)]
        public string? FullName { get; set; } // Nullable لأن الـ Model Binder ينشئ كائنًا من الـ ViewModel أولًا ثم يملأ الخصائص بالقيم التي يدخلها المستخدم، لذا قد لا تكون للخاصية قيمة ابتدائية وتكون null وقت الإنشاء.

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; } 

        [Range(1, int.MaxValue, ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [StringLength(100)]
        public string? JobTitle { get; set; } 

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Employee photo is required")]
        public IFormFile? PhotoFile { get; set; }
        public List<string> MobileNumbers { get; set; } = new();
    }

}

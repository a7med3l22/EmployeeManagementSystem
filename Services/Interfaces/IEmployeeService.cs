using AI_Makers_TechAssessment.Models.Dto;
using AI_Makers_TechAssessment.Models.Entities;
using AI_Makers_TechAssessment.ViewModels;
using System.Linq.Expressions;

namespace AI_Makers_TechAssessment.Services.Interfaces
{
    public interface IEmployeeService
    {
         Task AddEmployee(EmployeeCreateVM EmployeeVM);
         Task RemoveEmployee(int EmployeeId);
        Task UpdateEmployee(EmployeeEditVM EmployeeVM, int EmployeeId);

        Task<List<EmployeeVM>> FilterEmployee(string? employeeName, int? DepartmentId);
        Task<List<EmployeeVM>> GetAllEmployees();

        Task<EmployeeVM?> GetByIdSpecAsync(Expression<Func<Employee, bool>> predicate, params Expression<Func<Employee, object>>[] funcs);
        Task<List<DepartmentVM>> GetAllDepartments();

    }
}

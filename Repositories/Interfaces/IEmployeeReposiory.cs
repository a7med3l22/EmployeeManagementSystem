using AI_Makers_TechAssessment.Models.Entities;

namespace AI_Makers_TechAssessment.Repositories.Interfaces
{
    public interface IEmployeeRepository : IGenericSpecRepo<Employee>
    {
        Task<bool> EmailExistsAsync(string email);

        Task<bool> EmailExistsForAnotherEmployee(
    string email,
    int employeeId
);
    }
}

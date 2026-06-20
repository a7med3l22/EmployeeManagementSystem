using AI_Makers_TechAssessment.Data;
using AI_Makers_TechAssessment.Models.Entities;
using AI_Makers_TechAssessment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AI_Makers_TechAssessment.Repositories
{
    public class EmployeeRepository : GenericSpecRepo<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var emailExists = await dbContext.Employees.AnyAsync(e => e.Email == email);//واستخدام Async يمنع حجز الـ Thread أثناء انتظار قاعدة البيانات.
            return emailExists;

        }
        public async Task<bool> EmailExistsForAnotherEmployee(
        string email,
        int employeeId)
        {
            return await dbContext.Employees
                .AnyAsync(e =>
                    e.Email == email &&
                    e.Id != employeeId);
        }

    }
}

using AI_Makers_TechAssessment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AI_Makers_TechAssessment.Data.Seeding
{
    public class AddSeeding
    {
        public static async Task Seeding(AppDbContext dbContext, ILogger logger)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var rootPath = Directory.GetCurrentDirectory();

                var departmentsPath = Path.Combine(rootPath, "Data", "Seeding", "Departments.json");
                var employeesPath = Path.Combine(rootPath, "Data", "Seeding", "Employees.json");
                var mobileNumbersPath = Path.Combine(rootPath, "Data", "Seeding", "MobileNumbers.json");

                var departments = JsonSerializer.Deserialize<List<Department>>(
                    await File.ReadAllTextAsync(departmentsPath)
                ) ?? new();

                var employees = JsonSerializer.Deserialize<List<Employee>>(
                    await File.ReadAllTextAsync(employeesPath)
                ) ?? new();

                var mobileNumbers = JsonSerializer.Deserialize<List<MobileNumber>>(
                    await File.ReadAllTextAsync(mobileNumbersPath)
                ) ?? new();

                if (!await dbContext.Departments.AnyAsync())
                    await dbContext.Departments.AddRangeAsync(departments);

                if (!await dbContext.Employees.AnyAsync())
                    await dbContext.Employees.AddRangeAsync(employees);

                if (!await dbContext.MobileNumbers.AnyAsync())
                    await dbContext.MobileNumbers.AddRangeAsync(mobileNumbers);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                logger.LogInformation("Seeding completed successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "Seeding Failed");
            }
        }
    }
}
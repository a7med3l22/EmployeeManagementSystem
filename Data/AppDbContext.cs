using AI_Makers_TechAssessment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AI_Makers_TechAssessment.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)  //لتمرير إعدادات قاعدة البيانات مثل Connection String عن طريق Dependency Injection.
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), type => type.Namespace == "AI_Makers_TechAssessment.Data.Configurations");
        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<MobileNumber> MobileNumbers { get; set; }

    }
}

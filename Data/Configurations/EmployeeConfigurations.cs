using AI_Makers_TechAssessment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI_Makers_TechAssessment.Data.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Email)
           .IsRequired()
           .HasMaxLength(150);

            builder.HasIndex(e => e.Email)
                   .IsUnique();

            builder.HasIndex(e => e.FullName)
        .HasDatabaseName("IX_Employees_FullName");

            builder.HasOne(e => e.Department)
             .WithMany(d => d.Employees)
             .HasForeignKey(e => e.DepartmentId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.JobTitle)
       .IsRequired()
       .HasMaxLength(100);


            builder.Property(e => e.Photo)
       .HasMaxLength(500);

            builder.HasIndex(e => e.DepartmentId)
                   .HasDatabaseName("IX_Employees_DepartmentId");
        }
    }
}

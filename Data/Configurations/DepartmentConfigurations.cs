using AI_Makers_TechAssessment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI_Makers_TechAssessment.Data.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
           builder.Property(d => d.Name)
         .IsRequired()
         .HasMaxLength(100);

            builder.HasIndex(d => d.Name)
          .IsUnique()
          .HasDatabaseName("IX_Departments_Name"); //استخدم الاسم ده بالضبط للـ Index.
        }
    }
}

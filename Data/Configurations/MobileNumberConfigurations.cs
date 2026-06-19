using AI_Makers_TechAssessment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI_Makers_TechAssessment.Data.Configurations
{
    public class MobileNumberConfigurations : IEntityTypeConfiguration<MobileNumber>
    {
        public void Configure(EntityTypeBuilder<MobileNumber> builder)
        {
            builder.HasIndex(m => m.Number)
       .IsUnique();
            builder.Property(m => m.Number).IsRequired().HasMaxLength(20);
            builder.HasOne(m => m.Employee)
                .WithMany(e => e.MobileNumbers)
                .HasForeignKey(m => m.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

         
        }
    }
}

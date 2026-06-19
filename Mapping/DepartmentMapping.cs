using AI_Makers_TechAssessment.Models.Entities;
using AI_Makers_TechAssessment.ViewModels;
using AutoMapper;

namespace AI_Makers_TechAssessment.Mapping
{
    public class DepartmentMapping: Profile
    {
        public DepartmentMapping()
        {
            CreateMap<Department, DepartmentVM>();
        }
    }
}

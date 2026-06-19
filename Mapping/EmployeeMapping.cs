using AI_Makers_TechAssessment.Models.Dto;
using AI_Makers_TechAssessment.Models.Entities;
using AI_Makers_TechAssessment.ViewModels;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AI_Makers_TechAssessment.Mapping
{
    public class EmployeeMapping : Profile
    {
        public EmployeeMapping()
        {
            CreateMap<Employee, EmployeeVM>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom<photoReslover>())
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(s => s.Department.Name))
                .ForMember(dest => dest.DepartmentDescription, opt => opt.MapFrom(s => s.Department.Description))
                .ForMember(dest => dest.MobileNumbers,
                    opt => opt.MapFrom(src => src.MobileNumbers.Select(m => m.Number)));

            CreateMap<EmployeeCreateVM, Employee>();

            CreateMap<Employee, EmployeeEditVM>()
                .ForMember(dest => dest.MobileNumbers,
                    opt => opt.MapFrom(src => src.MobileNumbers.Select(m => m.Number)));

            CreateMap<EmployeeVM, EmployeeEditVM>();


        }
    }
    public class photoReslover : IValueResolver<Employee, EmployeeVM, string> 
    {
        private readonly IHttpContextAccessor httpContext;

        public photoReslover(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public string Resolve(Employee source, EmployeeVM destination, string destMember, ResolutionContext context)
        {
            var RequestUrl = httpContext.HttpContext?.Request;
            var HostUrl = $"{RequestUrl?.Scheme}://{RequestUrl?.Host}";
            return $"{HostUrl}/{source.Photo.Replace("\\", "/")}";
        }
    }

}

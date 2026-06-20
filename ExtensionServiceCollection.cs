using AI_Makers_TechAssessment.Data;
using AI_Makers_TechAssessment.Mapping;
using AI_Makers_TechAssessment.Repositories;
using AI_Makers_TechAssessment.Repositories.Interfaces;
using AI_Makers_TechAssessment.Services;
using AI_Makers_TechAssessment.Services.Interfaces;
using AutoMapper;
using Full_E_Commerce_Project.Handle_MiddleWares;
using Microsoft.EntityFrameworkCore;

namespace AI_Makers_TechAssessment //ok
{
    public static class ExtensionServiceCollection
    {
        public static IServiceCollection MyOwnServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(
        opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection"))
        );
            services.AddHttpContextAccessor(); //بيسمح لأي Service إنها توصل للـ HttpContext. للوصول إلى معلومات الـ Request أو الـ User من داخل Service بدون الحاجة لتمرير HttpContext بين الطبقات.
            services.AddAutoMapper(config => config.AddProfiles(new List<Profile>() { new EmployeeMapping() , new DepartmentMapping() }));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeService, EmployeeService>(); //لفصل Business Logic عن Controllers وتحقيق Separation of Concerns وتحسين الاختبار والصيانة.
            services.AddTransient<HandleErrorMiddleWare>();
            services.AddScoped<photoReslover>();



            return services;
        }

    }
}

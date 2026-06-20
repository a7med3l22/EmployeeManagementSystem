using AI_Makers_TechAssessment.Data;
using AI_Makers_TechAssessment.Data.Seeding;
using Full_E_Commerce_Project.Handle_MiddleWares;
using Microsoft.EntityFrameworkCore;

namespace AI_Makers_TechAssessment
{
    public static class ExtensionWebApp
    {
        public static async Task<IApplicationBuilder> MyOwnApp(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();  //تطبق جميع Migrations غير المنفذة تلقائيًا على قاعدة البيانات عند تشغيل التطبيق.
            await AddSeeding.Seeding(dbContext, app.Logger); 

            return app;

        }

    }
}

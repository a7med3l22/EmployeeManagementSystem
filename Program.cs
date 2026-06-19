using Full_E_Commerce_Project.Handle_MiddleWares;

namespace AI_Makers_TechAssessment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.MyOwnServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMiddleware<HandleErrorMiddleWare>();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.UseStaticFiles();
            app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Employee}/{action=Index}/{id?}")
        .WithStaticAssets();

            await app.MyOwnApp();

            app.Run();
        }
    }
}

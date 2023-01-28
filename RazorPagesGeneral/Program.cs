using Microsoft.EntityFrameworkCore;
using RazorPages.Services;

namespace RazorPagesGeneral
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContextPool<AppDbContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("EmoloyeeDBConnection"));
            });
            //builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.Configure<RouteOptions>(options => 
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
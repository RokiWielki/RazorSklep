using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorSklep.Data;
using RazorSklep.Models;

namespace RazorSklep
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<RazorSklepContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RazorSklepContext") ?? throw new InvalidOperationException("Connection string 'RazorSklepContext' not found.")));

            var app = builder.Build();

            using (var moviesScope = app.Services.CreateScope())
            {
                var services = moviesScope.ServiceProvider;

                DataMovies.Initialize(services);
            }

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
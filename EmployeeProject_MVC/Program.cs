using EmployeeProject_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject_MVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			//Add Connection String
			builder.Services.AddDbContext<ApplicationDbContext>(options 
				=> options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection")));


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Employees}/{action=Index}/{id?}");

			app.Run();
		}
	}
}

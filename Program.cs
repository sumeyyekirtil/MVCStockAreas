using Microsoft.AspNetCore.Authentication.Cookies;
using MVCStockAreas.Models;

namespace MVCStockAreas
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddSession(); //hatas� i�in -> InvalidOperationException: Session has not been configured for this application or request.

			//projede kullanaca��m�z dbcontext s�n�f�m�z� uygulamaya tan�ml�yoruz.
			builder.Services.AddDbContext<Context>();

			//Schema hatas� ��z�m�
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
			{
				x.LoginPath = "/Home/Index"; //admin oturum a�ma sayfam�z� belirttik
			});
			var app = builder.Build(); //builder nesnesi �zerinden eklenen servislerle beraber app nesnesi olu�turuluyor


			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRouting();

			app.UseSession();
			app.UseAuthorization();

			app.MapStaticAssets();

			//scaffoldingreadme pages and copy- paste because with for area used
			app.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
			);//bu route kodlar� admin areas�na gelecek olan istekleri kar��lay�p, ilgili controller ve action lara y�nlendirme yapacak.

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}")
				.WithStaticAssets();

			app.Run();
		}
	}
}

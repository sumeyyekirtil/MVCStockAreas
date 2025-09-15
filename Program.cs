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

			builder.Services.AddSession(); //hatasý için -> InvalidOperationException: Session has not been configured for this application or request.

			//projede kullanacaðýmýz dbcontext sýnýfýmýzý uygulamaya tanýmlýyoruz.
			builder.Services.AddDbContext<Context>();

			//Schema hatasý çözümü
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
			{
				x.LoginPath = "/Home/Index"; //admin oturum açma sayfamýzý belirttik
			});
			var app = builder.Build(); //builder nesnesi üzerinden eklenen servislerle beraber app nesnesi oluþturuluyor


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
			);//bu route kodlarý admin areasýna gelecek olan istekleri karþýlayýp, ilgili controller ve action lara yönlendirme yapacak.

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}")
				.WithStaticAssets();

			app.Run();
		}
	}
}

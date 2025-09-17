using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCStockAreas.Models;
using System.Diagnostics;
using System.Drawing;
using System.Security.Claims;

//for add Migration -> package manage -> add-migration InitialCreate (migration eklendi) -> lvl2 : update-database (sql data g�nderildi) - EF

//login page
namespace MVCStockAreas.Controllers
{
    //admin deki contoller a oturum a�mayan giremeyecek
    //home control : login
    //logini ge�en admine ge�ebilecek
    //crud i�lemleri area da olacak
    [AllowAnonymous] //authorize sistemi import edilip program.cs yaz�l�r
	public class HomeController : Controller
    {
        private readonly Context _context; //haz�rda contructor oldu�undan add parameters ile ekleme yap�ld�
        private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, Context context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		//veri g�nderiminde input i�indeki name k�sm� e�le�mez ise veri g�nderimi do�ru d�nmez
		public async Task<IActionResult> Index(string email, string sifre) //await metodunu kullanabilmek i�in asenkron i�lemler yap�lmas�n� sa�lar (make)
		{
			var kullanici = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == sifre);
			if (kullanici != null)
			{
				HttpContext.Session.SetString("Email", kullanici.Email);
				var haklar = new List<Claim>() //kullan�c� haklar� tan�mlad�k
				{
					new(ClaimTypes.Email, kullanici.Email), //claim = hak (kullan�c�ya tan�mlanan haklar)
						new(ClaimTypes.Role, "Admin")
				};
				var kullaniciKimligi = new ClaimsIdentity(haklar, "Index"); //kullan�c� i�in bir kimlik olu�turduk
				ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi); //bu s�n�ftan bir nesne olu�turup bilgilerde sakl� haklar ile kural olu�turulabilir
				await HttpContext.SignInAsync(claimsPrincipal); //yukar�daki yetkilerle sisteme giri� yapt�k
				return RedirectToAction("Index", "Admin");
			}
			else
				@TempData["Message"] = "<div class='alert alert-danger'>Giri� Ba�ar�s�z</div>";

			return RedirectToAction("Index", "Home");
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		//LogOut action olmazsa sayfa admin giri� a��k kal�yor ve tekrar giri� istemiyor
		public ActionResult LogOut() //��k�� yap aktivasyonu : layout
		{
			//FormsAuthentication.SignOut(); //.net faramework mvc ��k��
			HttpContext.SignOutAsync();
			//Session.Abandon(); //b�rakmak, terketmek
			return RedirectToAction("Index", "Home");
		}
	}
}
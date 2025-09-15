using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MVCStockAreas.Models;
using System.Diagnostics;
using System.Drawing;
using System.Security.Claims;

//login page
namespace MVCStockAreas.Controllers
{

    //admin deki contoller a oturum açmayan giremeyecek
    //home control : login
    //logini geçen admine geçebilecek
    //crud iþlemleri area da olacak
    [AllowAnonymous] //authorize sistemi import edilip program.cs yazýlýr
	public class HomeController : Controller
    {
        private readonly Context _context; //hazýrda contructor olduðundan add parameters ile ekleme yapýldý
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
		//veri gönderiminde input içindeki name kýsmý eþleþmez ise veri gönderimi doðru dönmez
		public async Task<IActionResult> Index(string email, string sifre) //await metodunu kullanabilmek için asenkron iþlemler yapýlmasýný saðlar (make)
		{
			var kullanici = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == sifre);
			if (kullanici != null)
			{
				HttpContext.Session.SetString("Email", kullanici.Email);
				var haklar = new List<Claim>() //kullanýcý haklarý tanýmladýk
				{
					new(ClaimTypes.Email, kullanici.Email), //claim = hak (kullanýcýya tanýmlanan haklar)
						new(ClaimTypes.Role, "Admin")
				};
				var kullaniciKimligi = new ClaimsIdentity(haklar, "Index"); //kullanýcý için bir kimlik oluþturduk
				ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi); //bu sýnýftan bir nesne oluþturup bilgilerde saklý haklar ile kural oluþturulabilir
				await HttpContext.SignInAsync(claimsPrincipal); //yukarýdaki yetkilerle sisteme giriþ yaptýk
				return RedirectToAction("Index", "Users");
			}
			else
				@TempData["Message"] = "<div class='alert alert-danger'>Giriþ Baþarýsýz</div>";

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
    }
}

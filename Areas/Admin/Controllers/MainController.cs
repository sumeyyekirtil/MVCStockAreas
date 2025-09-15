using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCStockAreas.Models;

namespace MVCStockAreas.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")] //bu controller admin areası altında çalışacak bilgisini atıyoruz
	public class MainController : Controller
	{
		private readonly Context _context;

		public MainController(Context context)
		{
			_context = context;
		}

		// GET: MainController
		public ActionResult Index()
		{
			return View(_context.Users);
		}

		// GET: MainController/Details/5
		public ActionResult Details(int id)
		{
			var kayit = _context.Users.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(kayit);
		}

		// GET: MainController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: MainController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(User collection)
		{
			try
			{
				_context.Users.Add(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: MainController/Edit/5
		public ActionResult Edit(int id)
		{
			var kayit = _context.Users.Find(id); //uyeler tablosundan route dan gelen id ile eşleşen kaydı bul ve ekrana gönder.
			return View(kayit);
		}

		// POST: MainController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, User collection)
		{
			try
			{
				_context.Users.Update(collection); //ekrandan gelen modeli veritabanındaki kaydı değiştirecek şekilde ayarla
				_context.SaveChanges(); //değişiklikleri db kaydet
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: MainController/Delete/5
		public ActionResult Delete(int id)
		{
			var kayit = _context.Users.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(kayit);
		}

		// POST: MainController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, User collection)
		{
			try
			{
				_context.Users.Remove(collection); //ekrandan gelen üye nesnesini silinecek olarak işaretle
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}

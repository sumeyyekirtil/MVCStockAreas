using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCStockAreas.Models;

//Area -> project -> add scaffolding item -> name: Admin -> add controller and views

//main - admin pages kullanılacak sayfa
namespace MVCStockAreas.Areas.Admin.Controllers
{
	[Authorize] //login işlemsiz giriş engeller (admin hesabı için özel)
	[Area("Admin")] //bu controller admin areası altında çalışacak bilgisini atıyoruz
	public class MainController : Controller
	{
		private readonly Context _context; //Added constructor after getting database connection data

		public MainController(Context context)
		{
			_context = context;
		}

		// GET: MainController
		public ActionResult Index() // add- view list
		{
			return View(_context.Users); //null exceptions error resolution
		}

		// GET: MainController/Details/5
		public ActionResult Details(int id) // add- view details
		{
			var kayit = _context.Users.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(kayit);
		}

		// GET: MainController/Create
		public ActionResult Create() // add- view create
		{
			return View();
		}

		// POST: MainController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(User collection) //herhangi bir actiondan eklenebilir
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
		public ActionResult Edit(int id) // add- view edit
		{
			var kayit = _context.Users.Find(id); //uyeler tablosundan route dan gelen id ile eşleşen kaydı bul		 
			return View(kayit); //ekrana gönder.
		}

		// POST: MainController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, User collection)
		{
			try
			{
				_context.Users.Update(collection); ////güncelleme işlemi
				_context.SaveChanges(); //değişiklikleri db kaydet
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: MainController/Delete/5
		public ActionResult Delete(int id) // add- view delete
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
				_context.SaveChanges(); //kaydetme işlemi
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
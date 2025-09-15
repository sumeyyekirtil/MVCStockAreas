using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCStockAreas.Models;

namespace MVCStockAreas.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")] //bu controller admin areası altında çalışacak bilgisini atıyoruz

	public class UsersController : Controller
	{
		private readonly Context _context;

		public UsersController(Context context)
		{
			_context = context;
		}

		// GET: MainController
		public ActionResult Index()
		{
			return View();
		}

		// GET: MainController/Details/5
		public ActionResult Details(int id)
		{
			return View();
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

				return RedirectToAction("Index", "Main");
			}
			catch
			{
				return View();
			}
		}

		// GET: MainController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: MainController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, User collection)
		{
			try
			{
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
			return View();
		}

		// POST: MainController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, User collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}

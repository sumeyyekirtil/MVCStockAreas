using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCStockAreas.Models;
using static System.Net.Mime.MediaTypeNames;

namespace MVCStockAreas.Areas.Admin.Controllers
{
	[Authorize] //login onaylanmayan kullanıcının girişini engeller
	[Area("Admin")] //bu controller admin areası altında çalışacak bilgisini atıyoruz
	public class ProductsController : Controller
	{
		private readonly Context _context;
		public ProductsController(Context context)
		{
			_context = context;
		}

		// GET: ProductsController
		public ActionResult Index() //add view - razor - list (giriş sayfası)
		{
			return View(_context.Products);
		}

		// GET: ProductsController/Details/5
		public ActionResult Details(int id) //add view - razor - details
		{
			var bilgi = _context.Products.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(bilgi);
		}

		// GET: ProductsController/Create
		public ActionResult Create() //add view - razor - create
		{
			return View();
		}

		// POST: ProductsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Product collection, IFormFile? Image)
		{
			if (ModelState.IsValid) //model içi boş ise döngüye girme
			{
				try
				{
					if (Image is not null)
					{
						collection.Image = FileHelper.FileLoader(Image);
					}
					//crud ekleme
					_context.Products.Add(collection);
					_context.SaveChanges();
					return RedirectToAction(nameof(Index));
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			return View(collection);
		}

		// GET: ProductsController/Edit/5
		public ActionResult Edit(int id) //add view - razor - edit
		{
			return View(_context.Products.Find(id)); //verileri db getirme 2. yol
		}

		// POST: ProductsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Product collection, IFormFile? Image)
		{
			if (ModelState.IsValid) //model içi boş ise döngüye girme
				return View(collection);
			try
			{
				if (Image is not null)
				{
					collection.Image = FileHelper.FileLoader(Image);
				}
				//crud güncelleme
				_context.Products.Update(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!");
			}
			return View(collection);
		}

		// GET: ProductsController/Delete/5
		public ActionResult Delete(int id) //add view - razor - delete
		{
			return View(_context.Products.Find(id));
		}

		// POST: ProductsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, Product collection)
		{//özellikle resim silmek için kontrol kodu girilmesine gerek yok, id silme işlemi yapılacak
			try
			{
				//crud silme
				_context.Products.Remove(collection); //ekrandan gelen üye nesnesini silinecek olarak işaretle
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!");
			}
			return View(collection);
		}
	}
}
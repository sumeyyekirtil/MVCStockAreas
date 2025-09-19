using System.ComponentModel.DataAnnotations;

namespace MVCStockAreas.Models
{//kullanılacak ürün sınıfı
	public class Product
	{
		public int Id { get; set; }

		[Display(Name = "İsim")] //ekran görünüm adı ayarlandı
		public string Name { get; set; }

		[Display(Name = "Fiyat")]
		public decimal Price { get; set; }

		[Display(Name = "Stok")]
		public int Stock { get; set; }

		[Display(Name = "Resim")]
		public string? Image { get; set; }

		[Display(Name = "Kayıt Tarihi")]
		public DateTime RecordDate { get; set; } = DateTime.Now;
	}
}
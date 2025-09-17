using System.ComponentModel.DataAnnotations;

namespace MVCStockAreas.Models
{//kullanılacak ürün sınıfı
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Photo { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }

		[Display(Name = "Resim"), StringLength(100)]
		public string? Image { get; set; }
		public DateTime RecordDate { get; set; } = DateTime.Now;
	}
}
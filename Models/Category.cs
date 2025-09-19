using System.ComponentModel.DataAnnotations;

namespace MVCStockAreas.Models
{//kullanılacak kategori sınıfı
	public class Category
	{
		public int Id { get; set; }

		[Display(Name = "İsim")] //ekranda görünüm ayarlandı
		public string Name { get; set; }

		[Display(Name = "Açıklama")]
		public string? Description { get; set; }

		[Display(Name = "Aktif")]
		public bool Active { get; set; }

		[Display(Name = "Oluşturulma Tarihi")]
		public DateTime CreateDate { get; set; } = DateTime.Now;
	}
}
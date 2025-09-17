namespace MVCStockAreas.Tools
{
	public class FileHelper //projede kullanılmak üzere resim ekleme control işlemlerini daha az kod ile yapmak ve kod sadeliği kuramına uymak için açılan tools - class
	{
		public static string FileLoader(IFormFile formFile) //resim dosyası yükleme işlemi için yeni metot tanımladık
		{
			string dosyaAdi = "";

			dosyaAdi = formFile.FileName; //geri döndürülen değere dosya adı eşitlendi
			string klasor = Directory.GetCurrentDirectory() + "/wwwroot/Images/"; //projedeki resim dosyasına ulaş
			using var stream = new FileStream(klasor + formFile.FileName, FileMode.Create); //yeni dosya olarak yükle
			formFile.CopyTo(stream); //kopyalayıp akıma bırak

			return dosyaAdi;
		}
	}
}
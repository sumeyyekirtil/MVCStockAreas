using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics; // for warning error

//Context classı model e bağlı olup bağlantı şemasını çizmeye yarar.
//override (ezici-farklı davranan metot) yapabileceğimiz iki on metodu var 1- OnConfiguring, 2- OnModelCreating
//1 -> Connection string oluşturulup builder tamamlanıyor
//2 -> Db için örnek veri bütünü yollanıyor
//3. Adım Tools - NuGet Package - Console ile DB SqlServer bağlantı ile oluşturuluyor (create)
namespace MVCStockAreas.Models
{//db bağlantı için kullanılacak sınıf
	public class Context : DbContext //before ef packages installed
									 //ef ile vt işlemlerini yapabilmemizi sağlar
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //baplantı nesnesi için -> override on enter
		{
			//add-migration InitialCreate (with package manager console write enter) migration created
			//update-database enter - sql in db created
			optionsBuilder.UseSqlServer("server=ASUS-PRO; database=MVCAreas; Integrated Security=True; TrustServerCertificate=True");
			optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
			//vt oluştururken aldığımız PendingModelChangesWarning hatasının çözümü
			//ignore - görmezden gel
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder) //examples db with data
		{
			modelBuilder.Entity<User>().HasData(
				new User
				{
					Id = 1,
					Email = "admin@gmail.com",
					Name = "User",
					Surname = "Admin",
					Birthday = DateTime.Now,
					Nickname = "admin",
					Password = "123",
					RepeatPassword = "123",
					IdentificationNumber = "12345678911",
					Phone = "2626262622"
				});
			base.OnModelCreating(modelBuilder);
		}
	}
}

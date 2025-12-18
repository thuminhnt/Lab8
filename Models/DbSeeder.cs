using System.Linq;
using Lab8.Models;
namespace Lab8.Models
{
    public class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Brands.Any())
            {
                var toyota = new Brand { Name = "Toyota", Country = "Japan" };
                var hyundai = new Brand { Name = "Hyundai", Country = "Korea" };
                context.Brands.AddRange(toyota, hyundai);
                context.SaveChanges();
                context.CarModels.AddRange(
                    new CarModel { Name = "Vios", BrandId = toyota.Id },
                    new CarModel { Name = "Camry", BrandId = toyota.Id },
                    new CarModel { Name = "Accent", BrandId = hyundai.Id }
                     );
                context.SaveChanges();
            }
        }
    }
}

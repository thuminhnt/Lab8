using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace Lab8.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
            : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModel> CarModels { get; set; }

    }
}

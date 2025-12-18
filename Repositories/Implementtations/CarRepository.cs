using Lab8.Models;
using Lab8.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab8.Repositories.Implementtations
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Car> GetAll()
        {
            return _context.Cars
                .Include(c => c.CarModel)
                .ToList();
        }

        public Car? GetById(int id)
        {
            return _context.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public void Update(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var car = _context.Cars.Find(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            _context.SaveChanges();
        }
    }
}

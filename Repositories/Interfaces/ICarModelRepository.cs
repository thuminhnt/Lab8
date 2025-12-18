using Lab8.Models;

namespace Lab8.Repositories.Interfaces
{
    public interface ICarModelRepository
    {
        List<CarModel> GetAll();
        CarModel? GetById(int id);
        void Add(CarModel carModel);
        void Update(CarModel carModel);
        void Delete(int id);
    }
}

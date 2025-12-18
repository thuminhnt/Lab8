using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICarModelService _carModelService;

        public CarController(ICarService carService, ICarModelService carModelService)
        {
            _carService = carService;
            _carModelService = carModelService;
        }

        // GET: /Car 
        public IActionResult Index()
        {
            return View(_carService.GetAllCars());
        }

        // GET: /Car/Details/5 
        public IActionResult Details(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();

            return View(car);
        }

        // GET: /Car/Create 
        public IActionResult Create()
        {
            ViewBag.CarModels = _carModelService.GetCarModels();
            return View();
        }

        // POST: /Car/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CarModels = _carModelService.GetCarModels();
                return View(car);
            }

            _carService.CreateCar(car);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Car/Edit/5 
        public IActionResult Edit(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();

            ViewBag.CarModels = _carModelService.GetCarModels();
            return View(car);
        }

        // POST: /Car/Edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Car car)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CarModels = _carModelService.GetCarModels();
                return View(car);
            }

            _carService.UpdateCar(car);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Car/Delete/5 
        public IActionResult Delete(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

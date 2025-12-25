using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICarModelService _carModelService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarController(ICarService carService, ICarModelService carModelService, IWebHostEnvironment webHostEnvironment)
        {
            _carService = carService;
            _carModelService = carModelService;
            _webHostEnvironment = webHostEnvironment;
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

            if (car.ImageFile != null)
            {
                car.ImageUrl = SaveImage(car.ImageFile, "car");
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

            if (car.ImageFile != null)
            {
                DeleteImage(car.ImageUrl);
                car.ImageUrl = SaveImage(car.ImageFile, "car");
            }

            _carService.UpdateCar(car);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Car/Delete/5 
        public IActionResult Delete(int id)
        {
            var car = _carService.GetCarById(id);
            if (car != null)
            {
                DeleteImage(car.ImageUrl);
            }
            _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }

        private string SaveImage(IFormFile imageFile, string folder)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", folder);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            return $"/images/{folder}/{uniqueFileName}";
        }

        private void DeleteImage(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}

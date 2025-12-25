using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab8.Controllers
{
    public class CarModelController : Controller
    {
        private readonly ICarModelService _carModelService;
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarModelController(
            ICarModelService carModelService,
            IBrandService brandService,
            IWebHostEnvironment webHostEnvironment)
        {
            _carModelService = carModelService;
            _brandService = brandService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: /CarModel 
        public IActionResult Index()
        {
            var data = _carModelService.GetCarModels();
            return View(data);
        }

        // GET: /CarModel/Create 
        public IActionResult Create()
        {
            ViewBag.Brands = _brandService.GetAllBrands();
            return View();
        }

        // POST: /CarModel/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarModel carModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = _brandService.GetAllBrands();
                return View(carModel);
            }

            if (carModel.ImageFile != null)
            {
                carModel.ImageUrl = SaveImage(carModel.ImageFile, "carmodel");
            }

            _carModelService.CreateCarModel(carModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: /CarModel/Edit/5 
        public IActionResult Edit(int id)
        {
            var carModel = _carModelService.GetCarModelById(id);
            if (carModel == null) return NotFound();

            ViewBag.Brands = _brandService.GetAllBrands();
            return View(carModel);
        }

        // POST: /CarModel/Edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarModel carModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = _brandService.GetAllBrands();
                return View(carModel);
            }

            if (carModel.ImageFile != null)
            {
                DeleteImage(carModel.ImageUrl);
                carModel.ImageUrl = SaveImage(carModel.ImageFile, "carmodel");
            }

            _carModelService.UpdateCarModel(carModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: /CarModel/Delete/5 
        public IActionResult Delete(int id)
        {
            var carModel = _carModelService.GetCarModelById(id);
            if (carModel != null)
            {
                DeleteImage(carModel.ImageUrl);
            }
            _carModelService.DeleteCarModel(id);
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

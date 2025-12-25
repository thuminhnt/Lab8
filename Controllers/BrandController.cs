using Lab8.Models;
using Lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;

namespace Lab8.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(IBrandService brandService, IWebHostEnvironment webHostEnvironment)
        {
            _brandService = brandService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: /Brand 
        public IActionResult Index()
        {
            var brands = _brandService.GetAllBrands();
            return View(brands);
        }

        // GET: /Brand/Create 
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Brand/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand brand)
        {
            if (!ModelState.IsValid)
                return View(brand);

            if (brand.ImageFile != null)
            {
                brand.ImageUrl = SaveImage(brand.ImageFile, "brand");
            }

            _brandService.CreateBrand(brand);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Brand/Edit/5 
        public IActionResult Edit(int id)
        {
            var brand = _brandService.GetBrandById(id);
            if (brand == null) return NotFound();

            return View(brand);
        }

        // POST: /Brand/Edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand brand)
        {
            if (!ModelState.IsValid)
                return View(brand);

            if (brand.ImageFile != null)
            {
                // Delete old image if exists
                DeleteImage(brand.ImageUrl);
                brand.ImageUrl = SaveImage(brand.ImageFile, "brand");
            }

            _brandService.UpdateBrand(brand);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Brand/Delete/5 
        public IActionResult Delete(int id)
        {
            var brand = _brandService.GetBrandById(id);
            if (brand != null)
            {
                DeleteImage(brand.ImageUrl);
            }
            _brandService.DeleteBrand(id);
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

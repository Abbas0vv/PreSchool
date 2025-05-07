using System;
using Microsoft.AspNetCore.Mvc;
using PreSchool.Database.Models;
using PreSchool.Database.Repository;

namespace PreSchool.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideBannerController : Controller
    {
        private readonly SlideBannerRepository _slideBannerRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public SlideBannerController(IWebHostEnvironment environment)
        {
            _slideBannerRepository = new SlideBannerRepository();
            _webHostEnviroment = environment;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var slideBanners = _slideBannerRepository.GetAll();
            var result = View(slideBanners);
            return result;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name, string designation, IFormFile file)
        {
            if (!ModelState.IsValid) return View();

            SlideBanner slideBanner = new SlideBanner(name, designation, file);
            if (!slideBanner.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "Invalid Image Format");
                return View();
            }
            if (slideBanner.File.Length > 200000)
            {
                ModelState.AddModelError("File", "File cannot be larger than 2mb");
                return View();
            }

            slideBanner.Image = slideBanner.File.CreateFile(_webHostEnviroment.WebRootPath,"Upload/Slider");

            await _slideBannerRepository.Insert(slideBanner);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var slieBanner = _slideBannerRepository.GetById(id);

            if (slieBanner is null) return RedirectToAction(nameof(NotFound));

            return View(slieBanner);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string name, string designation, IFormFile file)
        {
            if (!ModelState.IsValid) return View();

            var slideBanner = _slideBannerRepository.GetById(id);
            if (slideBanner is null) return RedirectToAction(nameof(NotFound));

            slideBanner.Name = name;
            slideBanner.Designation = designation;
            if (file is not null)
            {
                if (!file.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("File", "Invalid Image Format");
                    return View();
                }
                if (file.Length > 200000)
                {
                    ModelState.AddModelError("File", "File cannot be larger than 2mb");
                    return View();
                }
                string oldFilePath = Path.Combine(_webHostEnviroment.WebRootPath, slideBanner.Image ?? "");
                if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);

                var newPath = file.CreateFile(_webHostEnviroment.WebRootPath, "Upload/Slider");
                slideBanner.Image = newPath;
            }

            _slideBannerRepository.Update(slideBanner);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var slideBanner = _slideBannerRepository.GetById(id);

            if (slideBanner is null) return RedirectToAction(nameof(NotFound));

            _slideBannerRepository.RemoveById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

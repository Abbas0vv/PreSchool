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
        private const string folderName = "Upload/Slider";
        public SlideBannerController(IWebHostEnvironment environment)
        {
            _slideBannerRepository = new SlideBannerRepository();
            _webHostEnviroment = environment;

        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var slideBanners = _slideBannerRepository.GetAll();
            var result = View(slideBanners);
            return result;
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SlideBanner banner)
        {
            if (!ModelState.IsValid) return View(banner);
            if (banner.File is null)
            {
                ModelState.AddModelError("File", "Please upload an image.");
                return View(banner);
            }
            if (!banner.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "Invalid Image Format");
                return View();
            }
            if (banner.File.Length > 2097152)
            {
                ModelState.AddModelError("File", "File cannot be larger than 2mb");
                return View();
            }

            banner.ImageUrl = banner.File.CreateFile(_webHostEnviroment.WebRootPath,folderName);

            await _slideBannerRepository.Insert(banner);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var slideBanner = _slideBannerRepository.GetById(id);

            if (slideBanner is null) return RedirectToAction(nameof(NotFound));

            return View(slideBanner);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SlideBanner banner)
        {
            if (!ModelState.IsValid) return View(banner);

            var slideBanner = _slideBannerRepository.GetById(banner.Id);
            if (slideBanner is null) return RedirectToAction(nameof(NotFound));

            slideBanner.Name = banner.Name;
            slideBanner.Designation = banner.Designation;
            if (banner.File is not null)
            {
                if (!banner.File.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("File", "Invalid Image Format");
                    return View();
                }
                if (banner.File.Length > 2097152)
                {
                    ModelState.AddModelError("File", "File cannot be larger than 2mb");
                    return View();
                }

                FileExtention.RemoveFile(_webHostEnviroment.WebRootPath, folderName, slideBanner.ImageUrl);

                var newImageUrl = banner.File.CreateFile(_webHostEnviroment.WebRootPath, folderName);
                slideBanner.ImageUrl = newImageUrl;
            }

            await _slideBannerRepository.Update(slideBanner);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var slideBanner = _slideBannerRepository.GetById(id);

            if (slideBanner is null) return RedirectToAction(nameof(NotFound));

            string oldFilePath = Path.Combine(_webHostEnviroment.WebRootPath, slideBanner.ImageUrl ?? "");
            if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);
            _slideBannerRepository.RemoveById(id);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}

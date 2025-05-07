using Microsoft.AspNetCore.Mvc;
using PreSchool.Database.Repository;
using PreSchool.ViewModels;

namespace PreSchool.Controllers;

public class HomeController : Controller
{
    private readonly SlideBannerRepository _slideBannerRepository;

    public HomeController()
    {
        _slideBannerRepository = new SlideBannerRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        HomeViewModel model = new HomeViewModel
        {
            SlideBanners = _slideBannerRepository.GetAll().OrderBy(p => p.Id).ToList()
        };

        return View(model);
    }
}

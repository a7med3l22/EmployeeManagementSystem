using Microsoft.AspNetCore.Mvc;

namespace AI_Makers_TechAssessment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

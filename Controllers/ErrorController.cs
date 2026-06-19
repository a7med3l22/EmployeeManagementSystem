using Microsoft.AspNetCore.Mvc;

namespace AI_Makers_TechAssessment.Controllers
{
    //error controller to handle errors and display error messages to the user
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Index(string msg)
        {
            ViewBag.ErrorMessage = msg ?? "Something went wrong";
            return View();
        }
    }
}
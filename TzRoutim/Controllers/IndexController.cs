using Microsoft.AspNetCore.Mvc;

namespace TzRoutim.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

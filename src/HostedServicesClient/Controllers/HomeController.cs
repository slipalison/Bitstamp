using Microsoft.AspNetCore.Mvc;

namespace HostedServicesClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
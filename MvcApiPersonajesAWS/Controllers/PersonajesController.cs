using Microsoft.AspNetCore.Mvc;

namespace MvcApiPersonajesAWS.Controllers
{
    public class PersonajesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

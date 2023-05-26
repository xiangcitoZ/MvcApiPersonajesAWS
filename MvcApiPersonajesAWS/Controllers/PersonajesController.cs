using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesAWS.Models;
using MvcApiPersonajesAWS.Services;

namespace MvcApiPersonajesAWS.Controllers
{

    public class PersonajesController : Controller
    {
        private ServiceApiPersonajes service;
        public PersonajesController(ServiceApiPersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Test()
        {
            ViewData["TEXT"] = await this.service.TestApiAsync();
            return View();
        }


        public IActionResult ApiCliente()
        {
            return View();
        }

        public async Task<IActionResult> ApiPersonajes()
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesAsync();
            return View(personajes);
        }

    }
}

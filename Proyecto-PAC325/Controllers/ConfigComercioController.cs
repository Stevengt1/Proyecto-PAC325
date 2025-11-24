using Microsoft.AspNetCore.Mvc;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Controllers
{
    public class ConfigComercioController : Controller
    {
        private readonly ConfigComercioBusiness _configComercioBusiness;

        public ConfigComercioController(ConfigComercioBusiness configComercioBusiness)
        {
            _configComercioBusiness = configComercioBusiness;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _configComercioBusiness.GetConfiguraciones());
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ConfigComercioModel config)
        {
            ConfigComercioModel config_temp = await _configComercioBusiness.Add(config);
            if (config_temp == null)
            {
                TempData["error"] = "Ocurrio un error a la hora de insertar el registro en la base de datos";
            }
            else if (config_temp.IdConfiguracion == -1)
            {
                TempData["error"] = "El comercio ya presenta una configuración, edita la ya registrada";
            }
            else
            {
                TempData["success"] = "El comercio fue guardado con exito";
            }
            return RedirectToAction("Registro");
        }

        public async Task<IActionResult> Editar(int id)
        {
            return View(await _configComercioBusiness.GetConfiguracion(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ConfigComercioModel config)
        {
            var result = await _configComercioBusiness.Update(config);
            if (result == null)
            {
                TempData["error"] = "Ocurrio un error a la hora de actualizar los datos";
                return RedirectToAction("Editar", config);
            }
            TempData["success"] = "Los datos fueron editados de manera correcta";
            return RedirectToAction("Editar", result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioBusiness _usuarioBusiness;
        private readonly ComercioBusiness _comercioBusiness;

        public UsuarioController(UsuarioBusiness usuarioBusiness, ComercioBusiness comercioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
            _comercioBusiness = comercioBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioBusiness.GetAllUsuarios();
            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> Registro()
        {
            var comercios = await _comercioBusiness.GetAllComercio();
            ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
            {
                var comercios = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre");
                return View("Registro", usuario);
            }

            var result = await _usuarioBusiness.Add(usuario);

            if (result == null)
            {
                TempData["error"] = "Ya existe un usuario con esa identificación o ocurrió un error.";
                var comercios = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre");
                return View("Registro", usuario);
            }

            TempData["success"] = "Usuario registrado correctamente.";
            return RedirectToAction("Index");
        }

        [HttpGet("Usuario/Editar/{idUsuario}")]
        public async Task<IActionResult> Editar(int idUsuario)
        {
            var usuario = await _usuarioBusiness.GetUsuario(idUsuario);
            if (usuario == null) return NotFound();

            var comercios = await _comercioBusiness.GetAllComercio();
            ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre", usuario.IdComercio);

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
            {
                var comercios = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre", usuario.IdComercio);
                return View("Editar", usuario);
            }

            var result = await _usuarioBusiness.Update(usuario);

            if (result == null)
            {
                TempData["error"] = "Ocurrió un error al editar el usuario.";
                var comercios = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre", usuario.IdComercio);
                return View("Editar", usuario);
            }

            TempData["success"] = "Usuario editado correctamente.";
            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Pjanalista_DosCrucez.DataInfo;
using Pjanalista_DosCrucez.Models;
using System.Data.SqlClient;
using System.Data;

namespace Pjanalista_DosCrucez.Controllers
{
    public class EmpleadoController : Controller
    {
        ReclamosDatos _reclamosDatos = new ReclamosDatos();
        public IActionResult ListarReclamos()
        {
            var oLista = _reclamosDatos.listarReclamos();
            return View(oLista);
        }
        [HttpPost]
        public IActionResult ClasificarReclamos(ReclamosModel oReclamos)
        {
            var respuesta = _reclamosDatos.GuardarReclamo(oReclamos);

            if (respuesta)
                return RedirectToAction("ListarReclamos");
            return View();
        }
        public IActionResult LoginEmpleado()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginEmpleado(EmpleadoModel oEmpleado)
        {
            bool exists = false;
            if(oEmpleado.UsuarioEmpleado != null)
            {
                exists = _reclamosDatos.verificarUsuario(oEmpleado.UsuarioEmpleado, oEmpleado.PasswordEmpleado);
                if (exists) {
                    return RedirectToAction("ListarReclamos");
                }
            }
            return View();
        }
        public IActionResult ClasificarReclamo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ClasificarReclamo(int idReclamo)
        {
            var oContacto = _reclamosDatos.buscarReclamo(idReclamo);
            return View(oContacto);
        }
        public IActionResult VerDetalleReclamo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult VerDetalleReclamo(int idReclamo)
        {
            var oContacto = _reclamosDatos.buscarReclamo(idReclamo);
            return View(oContacto);
        }
    }
}

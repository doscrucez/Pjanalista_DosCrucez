using Microsoft.AspNetCore.Mvc;
using Pjanalista_DosCrucez.DataInfo;
using Pjanalista_DosCrucez.Models;

namespace Pjanalista_DosCrucez.Controllers
{
    public class CiudadanoController : Controller
    {
        ReclamosDatos _reclamosDatos = new ReclamosDatos();

        public IActionResult RegistrarReclamo(ReclamosModel oReclamos)
        {
            //if (!ModelState.IsValid)
            //    return View();

                var respuesta = _reclamosDatos.GuardarReclamo(oReclamos);

                if (respuesta)
                    return RedirectToAction("Index", "Home");
                else
                    return View();
        }
    }
}

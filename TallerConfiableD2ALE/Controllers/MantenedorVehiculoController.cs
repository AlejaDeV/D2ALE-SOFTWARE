using Microsoft.AspNetCore.Mvc;
using TallerConfiableD2ALE.Datos;
using TallerConfiableD2ALE.Models;

namespace TallerConfiableD2ALE.Controllers
{
    public class MantenedorVehiculoController : Controller
    {
        VehiculoDatos vehiculoDatos = new VehiculoDatos();
        public IActionResult Listar()
        {
            var oLista = vehiculoDatos.Listar();
            return View(oLista);
        }

        public IActionResult Guardar()
        {
            //Solo nos devuelve la vista del formulario guardar
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(VehiculoModel oVehiculo)
        {
            //Recibe un objeto y lo guarda en la Base de datos
            if (!ModelState.IsValid)//Verificamos si las validaciones no se cumplen
                return View();

            var respuesta = vehiculoDatos.Guardar(oVehiculo);
            if (respuesta)
                return RedirectToAction("Listar");//Si se guarda redireccionamos a la vista listar
            else
                return View();
        }
        public IActionResult Editar(string placa)
        {
            //Solo nos devuelve la vista del formulario guardar
            var oVehiculo = vehiculoDatos.Obtener(placa);
            return View(oVehiculo);
        }
        [HttpPost]
        public IActionResult Editar(VehiculoModel oVehiculo)
        {
            //Recibe un objeto y lo guarda en la Base de datos
            if (!ModelState.IsValid)//Verificamos si las validaciones no se cumplen
                return View();

            var respuesta = vehiculoDatos.Editar(oVehiculo);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
        public IActionResult Eliminar(string placa)
        {
            //Solo nos devuelve la vista del formulario guardar
            var oVehiculo = vehiculoDatos.Obtener(placa);
            return View(oVehiculo);
        }

        [HttpPost]
        public IActionResult Eliminar(VehiculoModel oVehiculo)
        {
            //Recibe un objeto y lo guarda en la Base de datos

            var respuesta = vehiculoDatos.Eliminar(oVehiculo.placa);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}

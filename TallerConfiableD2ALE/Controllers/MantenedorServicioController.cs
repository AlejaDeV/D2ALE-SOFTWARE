using Microsoft.AspNetCore.Mvc;
using TallerConfiableD2ALE.Datos;
using TallerConfiableD2ALE.Models;

namespace TallerConfiableD2ALE.Controllers
{
    public class MantenedorServicioController : Controller
    {
        ServicioDatos servicioDatos = new ServicioDatos();
        public IActionResult Listar()
        {
            //VISUALIZACIÓN DE LOS DATOS DE LA TABLA SERVICIO
            var oLista = servicioDatos.Listar();
            return View(oLista);
        }        
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(ServicioModel oServicio)
        {
            //SE RECIBE UN OBJETO PARA GUARDARLO EN LA BD
            if (!ModelState.IsValid)//Verificamos si las validaciones no se cumplen
                return View();

            var respuesta = servicioDatos.Guardar(oServicio);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
        //Metodo para Editar
        public IActionResult Editar(int IdServicio)
        {
            //Metodo que nos devuelve la vista del formulario.
            var oservicio = servicioDatos.Obtener(IdServicio);
            return View(oservicio);
        }
        [HttpPost]
        public IActionResult Editar(ServicioModel oServicio)
        {
            //SE RECIBE UN OBJETO PARA GUARDARLO EN LA BD
            if (!ModelState.IsValid)//Verificamos si las validaciones no se cumplen
                return View();

            var respuesta = servicioDatos.Editar(oServicio);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }        
        //METODO PARA ELIMINAR
        public IActionResult Eliminar(int IdServicio)
        {
            //Solo nos devuelve la vista del formulario guardar
            var oservicio = servicioDatos.Obtener(IdServicio);
            return View(oservicio);
        }
        [HttpPost]
        public IActionResult Eliminar(ServicioModel oServicio)
        {
            var respuesta = servicioDatos.Eliminar(oServicio.IdServicio);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}

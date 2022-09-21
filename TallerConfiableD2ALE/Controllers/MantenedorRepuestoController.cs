using Microsoft.AspNetCore.Mvc;
using TallerConfiableD2ALE.Datos;
using TallerConfiableD2ALE.Models;

namespace TallerConfiableD2ALE.Controllers
{
    public class MantenedorRepuestoController : Controller
    {
        RepuestoDatos repuestoDatos = new RepuestoDatos();
        public IActionResult Listar()
        {
            //VISUALIZACIÓN DE LOS DATOS DE LA TABLA REPUESTO
            var oLista = repuestoDatos.Listar();
            return View(oLista);
        }
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(RepuestoModel oRepuesto)
        {
            //SE RECIBE UN OBJETO PARA GUARDARLO EN LA BD
            if (!ModelState.IsValid)//Verificamos si las validaciones no se cumplen
                return View();

            var respuesta = repuestoDatos.Guardar(oRepuesto);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
        //Metodo para Editar
        public IActionResult Editar(int IdRepuesto)
        {
            //Metodo que nos devuelve la vista del formulario.
            var oservicio = repuestoDatos.Obtener(IdRepuesto);
            return View(oservicio);
        }
        [HttpPost]
        public IActionResult Editar(RepuestoModel oRepuesto)
        {
            //SE RECIBE UN OBJETO PARA GUARDARLO EN LA BD
            if (!ModelState.IsValid)//Verificamos si las validaciones no se cumplen
                return View();

            var respuesta = repuestoDatos.Editar(oRepuesto);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
        //METODO PARA ELIMINAR
        public IActionResult Eliminar(int IdRepuesto)
        {
            //Solo nos devuelve la vista del formulario guardar
            var oservicio = repuestoDatos.Obtener(IdRepuesto);
            return View(oservicio);
        }
        [HttpPost]
        public IActionResult Eliminar(RepuestoModel oRepuesto)
        {
            var respuesta = repuestoDatos.Eliminar(oRepuesto.IdRepuesto);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}

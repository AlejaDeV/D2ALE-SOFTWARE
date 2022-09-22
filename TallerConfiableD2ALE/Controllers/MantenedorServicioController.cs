using Microsoft.AspNetCore.Mvc;
using TallerConfiableD2ALE.Datos;
using TallerConfiableD2ALE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TallerConfiableD2ALE.Controllers
{
    public class MantenedorServicioController : Controller
    {
        ServicioDatos servicioDatos = new ServicioDatos();
        UsuarioDatos listaMecanicos = new UsuarioDatos();
        VehiculoDatos listaVehiculos = new VehiculoDatos();

        [Authorize(Roles = "Mecanico,JefeOperaciones,Cliente")]
        public IActionResult Listar()
        {
            //VISUALIZACIÓN DE LOS DATOS DE LA TABLA SERVICIO
            var oLista = servicioDatos.Listar();
            return View(oLista);
        }

        [Authorize(Roles = "Mecanico,JefeOperaciones")]


        public IActionResult Guardar()
        {
            ViewBag.ListMec = new SelectList(listaMecanicos.Listar(),"idUsuario","nombres");
            ViewBag.ListVehi = new SelectList(listaVehiculos.Listar(), "placa", "placa");
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(ServicioModel oServicio)
        {
            //SE RECIBE UN OBJETO PARA GUARDARLO EN LA BD
            if (!ModelState.IsValid)//Verificamos si las validaciones no se cumplen
                   return RedirectToAction("Guardar");

            var respuesta = servicioDatos.Guardar(oServicio);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [Authorize(Roles = "Mecanico,JefeOperaciones")]
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

        [Authorize(Roles = "Mecanico,JefeOperaciones")]
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

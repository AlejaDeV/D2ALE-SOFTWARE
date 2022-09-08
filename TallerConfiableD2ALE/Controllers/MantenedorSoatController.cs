using Microsoft.AspNetCore.Mvc;
using TallerConfiableD2ALE.Datos;
using TallerConfiableD2ALE.Models;

namespace TallerConfiableD2ALE.Controllers
{
    public class MantenedorSoatController : Controller
    {
        SoatDatos _SoatDatos = new SoatDatos();
        public IActionResult Listar()
        {
            //LA LISTA MOSTRARA UNA LISTA DE SOAT
            var oLista = _SoatDatos.Listar();
            return View(oLista);
        }

        public IActionResult Guardar()
        {
            //METODO QUE DEVUELVE LA LISTA
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(SoatModel oSoat)
        {
            //METODO QUE RECIBE UN OBJETO Y LO GUARDA EN LA BASE DE DATOS

            if (!ModelState.IsValid)
                return View();

            var respuesta = _SoatDatos.Guardar(oSoat);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Editar(int idSOAT)
        {
            //METODO QUE DEVUELVE LA LISTA
            var oSoat = _SoatDatos.Obtener(idSOAT);

            return View(oSoat);
        }

        [HttpPost]
        public IActionResult Editar(SoatModel oSoat)
        {
            //METODO QUE RECIBE UN OBJETO Y LO GUARDA EN LA BASE DE DATOS

            if (!ModelState.IsValid)
                return View();

            var respuesta = _SoatDatos.Editar(oSoat);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Eliminar(int idSOAT)
        {
            //METODO QUE DEVUELVE LA LISTA
            var oSoat = _SoatDatos.Obtener(idSOAT);

            return View(oSoat);
        }

        [HttpPost]
        public IActionResult Eliminar(SoatModel oSoat)
        {
            //METODO QUE RECIBE UN OBJETO Y LO GUARDA EN LA BASE DE DATOS


            var respuesta = _SoatDatos.Eliminar(oSoat.idSOAT);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.CodeAnalysis;
using TallerConfiableD2ALE.Datos;
using TallerConfiableD2ALE.Models;
using Microsoft.AspNetCore.Authorization;

namespace TallerConfiableD2ALE.Controllers
{
    public class MantenedorUsuarioController : Controller
    {
        UsuarioDatos usuarioDatos = new UsuarioDatos();
        public IActionResult Listar()
        {
            var oLista = usuarioDatos.Listar();
            return View(oLista);
        }
        //Hacemos sobrecarga de métodos
        public IActionResult Guardar()
        {
            //Solo nos devuelve la vista del formulario guardar
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(UsuarioModel oUsuario)
        {
            //Recibe un objeto y lo guarda en la Base de datos
            //if (!ModelState.IsValid)//Verificamos si las validaciones no se cumplen
                //return View();
            try
            {
                var respuesta = usuarioDatos.Guardar(oUsuario);
                if (respuesta)
                    return RedirectToAction("Listar");
                else
                    return View();
            }
            catch (Exception e)
            {
                if (e.InnerException.Message.Contains("duplicate"))
                {
                    
                }
                //ModelState.AddModelError(identiExistAttribute.GetCustomAttribute, e.InnerException.Message);
                
                return View();
            }

            
        }

        [Authorize(Roles = "Auxiliar,JefeOperaciones")]
        public IActionResult Editar(int idUsuario)
        {
            //Solo nos devuelve la vista del formulario guardar
            var oUsuario = usuarioDatos.Obtener(idUsuario);
            return View(oUsuario);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioModel oUsuario)
        {
            //Recibe un objeto y lo guarda en la Base de datos
            
            if (!ModelState.IsValid)//Verificamos si las validaciones no se cumplen
                return View();

            var respuesta = usuarioDatos.Editar(oUsuario);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [Authorize(Roles = "Auxiliar,JefeOperaciones")]
        public IActionResult Eliminar(int idUsuario)
        {
            //Solo nos devuelve la vista del formulario guardar
            var oUsuario = usuarioDatos.Obtener(idUsuario);
            return View(oUsuario);
        }

        [HttpPost]
        public IActionResult Eliminar(UsuarioModel oUsuario)
        {
            //Recibe un objeto y lo guarda en la Base de datos

            var respuesta = usuarioDatos.Eliminar(oUsuario.idUsuario);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}

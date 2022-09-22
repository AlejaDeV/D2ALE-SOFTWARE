using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using TallerConfiableD2ALE.Datos;
using TallerConfiableD2ALE.Models;
using System.Web;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using NuGet.Configuration;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace TallerConfiableD2ALE.Controllers
{
    public class MantenedorLoginController : Controller
    {
        public static class Datos
        {
            public static string roles;

            public static string identificacionListarCliente;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UsuarioModel oUsuario)
        {

            try
            {
                var cn = new Conexion();


                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", conexion);//Definimos que procedimiento almacenado se utilizara
                    cmd.Parameters.AddWithValue("identificacion", oUsuario.identificacion);
                    cmd.Parameters.AddWithValue("contrasena", oUsuario.contrasena);
                    cmd.CommandType = CommandType.StoredProcedure;//Definimos que vamso a trabajar con un procedimiento almacenado                    
                    conexion.Open();

                    oUsuario.idUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    var usuario = oUsuario.idUsuario;

                    conexion.Close();

                    SqlCommand cmd2 = new SqlCommand("sp_ValidarUsuarioRol", conexion);//Definimos que procedimiento almacenado se utilizara
                    cmd2.Parameters.AddWithValue("identificacion", oUsuario.identificacion);
                    cmd2.Parameters.AddWithValue("contrasena", oUsuario.contrasena);
                    cmd2.CommandType = CommandType.StoredProcedure;//Definimos que vamso a trabajar con un procedimiento almacenado                    
                    conexion.Open();

                    oUsuario.rolFK = Convert.ToInt32(cmd2.ExecuteScalar().ToString());


                    switch (oUsuario.rolFK)
                    {
                        case 1:
                            Datos.roles = "Cliente";
                            break;
                        case 2:
                            Datos.roles = "Auxiliar";
                            break;
                        case 3:
                            Datos.roles = "JefeOperaciones";
                            break;
                        case 4:
                            Datos.roles = "Mecanico";
                            break;
                    }

                    if (usuario != 0)
                    {
                        Datos.identificacionListarCliente = oUsuario.identificacion;

                        var claims = new List<Claim>
                        {
                            new Claim("identificacion",oUsuario.identificacion),
                            new Claim(ClaimTypes.Role,value: Datos.roles)

                        };



                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(oUsuario));

                        return RedirectToAction("Listar", "MantenedorUsuario");
                    }
                    else
                    {
                        ViewData["Mensaje"] = "Usuario no encontrado";
                        return View();
                    }


                }
            }
            catch (Exception e)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }

        }


        public async Task<ActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("usuario");
            return RedirectToAction("Login", "MantenedorLogin");
        }
    }
}
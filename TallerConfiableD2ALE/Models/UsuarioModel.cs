using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using TallerConfiableD2ALE.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using System;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TallerConfiableD2ALE.Models
{
    public class UsuarioModel
    {
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "El campo nombres es obligatorio")]
        public string? nombres { get; set; }
        [Required(ErrorMessage = "El campo apellidos es obligatorio")]
        public string? apellidos { get; set; }
        [Required(ErrorMessage = "El número de identificación es obligatorio.")]
        //[identiExist]
        public string? identificacion { get; set; }
        //public string? identi2 { get; set; }//variable para la identificación en vista editar(ya que cambia la forma de validarla)

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string? telefono { get; set; }
        [Required(ErrorMessage = "El correo es un campo obligatorio.")]
        [EmailAddress()]
        public string? correo { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string? contrasena { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime fechaNacimiento { get; set; }
        public int rolFK { get; set; }
        public string? rol { get; set; }
    }

    public class identiExistAttribute : ValidationAttribute//Creamos una clase para verificar si el número de identificación está registrado en la base de datos.
    {
        //Método para hacer la validación, en caso de que exista un registro en la DB que contenga el número de identificación ingresado devolverá false
        public override bool IsValid(object? value)//Evaluaremos si es válido el valor ingresado de identificación
        {
            string identi ="";
            int idUsuario = 0;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    var query = "select idUsuario, identificacion from usuario WHERE (identificacion = @identificacion)";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.Add("identificacion", SqlDbType.VarChar).Value = value;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            idUsuario = Convert.ToInt32(dr["idUsuario"]);
                            identi = dr["identificacion"].ToString();
                        }
                    }
                }
                if (idUsuario == 0)
                {
                    ErrorMessage = idUsuario + "";
                    return true;
                }
                else
                {
                    if (identi == value.ToString())
                    {
                        ErrorMessage = "El número de identificación ingresado ya está registrado, intenta con otro.";
                        return false;
                    }
                }
                
            }
            catch (Exception e)
            {
                ErrorMessage = idUsuario +"";
                return true;
            }
            return true;
        }
    }

}

using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using TallerConfiableD2ALE.Datos;

namespace TallerConfiableD2ALE.Models
{
    public class VehiculoModel
    {
        [Required(ErrorMessage = "El campo 'Placa' es obligatorio.")]
        [StringLength(7)]
        public string? placa { get; set; }
        [Required(ErrorMessage = "El campo 'Marca' es obligatorio.")]
        public string? marca { get; set; }
        [Required(ErrorMessage = "El campo 'Modelo' es obligatorio.")]
        public string? modelo { get; set; }
        [Required(ErrorMessage = "El campo 'Tipo de vehículo' es obligatorio.")]
        public string? tipoVehiculo { get; set; }
        [Required(ErrorMessage = "El campo 'Cilindraje' es obligatorio.")]
        public string? cilindraje { get; set; }
        [Required(ErrorMessage = "La ciudad de registro es obligatoria.")]
        public string? ciudadRegistro { get; set; }
        //[Required(ErrorMessage = "Es obligatorio asignar un usuario al vehículo.")]
        public int usuarioFK { get; set; }

        public string? nombre { get; set; }
        public string? apellido { get; set; }
        //[Display(identificacion="Identificación")]
        [Required(ErrorMessage = "Es obligatorio ingresar un número de identificación válido.")]
        [IdentiExist(ErrorMessage ="El número de identificación ingresado no está registrado.")]
        public string? identificacion { get; set; }
    }

    //Ahora haremos una clase dentro de esta misma para validar si está registrado el número de identificación

    public class IdentiExistAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)//Validamos si el campo (object? value) que estamos ingresando es válido
        {
            var oUsuario = new UsuarioModel();
            var idUsuario = 0;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    var query = "select idUsuario from usuario WHERE (identificacion = @identificacion)";
                    using (var cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.Add("@identificacion", SqlDbType.VarChar).Value = value;
                        idUsuario = (int)cmd.ExecuteScalar();
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}

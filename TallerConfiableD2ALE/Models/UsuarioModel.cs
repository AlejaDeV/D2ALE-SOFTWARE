using System.ComponentModel.DataAnnotations;
namespace TallerConfiableD2ALE.Models
{
    public class UsuarioModel
    {
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "El campo nombres es obligatorio")]
        public string? nombres { get; set; }
        [Required(ErrorMessage = "El campo apellidos es obligatorio")]
        public string? apellidos { get; set; }
        [Required(ErrorMessage = "El número de identificación es obligatorio")]
        public string? identificacion { get; set; }
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string? telefono { get; set; }
        [Required(ErrorMessage = "El correo es un campo obligatorio.")]
        [EmailAddress]
        public string? correo { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string? contrasena { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime fechaNacimiento { get; set; }
        public int rolFK { get; set; }
        public string? rol { get; set; }
    }
}

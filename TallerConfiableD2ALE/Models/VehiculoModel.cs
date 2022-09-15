using System.ComponentModel.DataAnnotations;
namespace TallerConfiableD2ALE.Models
{
    public class VehiculoModel
    {
        [Required(ErrorMessage = "El campo 'Placa' es obligatorio.")]
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
        [Required(ErrorMessage = "Es obligatorio asignar un usuario al vehículo.")]
        public int usuarioFK { get; set; }

        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? identificacion { get; set; }
    }
}

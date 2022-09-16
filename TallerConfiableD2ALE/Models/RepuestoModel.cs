using System.ComponentModel.DataAnnotations;
namespace TallerConfiableD2ALE.Models
{
    public class RepuestoModel
    {
        public int IdRepuesto { get; set; }
        [Required(ErrorMessage = "El campo 'Nombre del Repuesto' es obligatorio")]
        public string? NombreRepuesto { get; set; }
        [Required(ErrorMessage = "El campo 'Precio del Repuesto' es obligatorio")]
        public int Precio { get; set; }
    }
}

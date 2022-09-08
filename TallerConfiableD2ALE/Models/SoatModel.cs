using System.ComponentModel.DataAnnotations;
namespace TallerConfiableD2ALE.Models
{
    public class SoatModel
    {
        public int idSOAT { get; set; }

        [Required(ErrorMessage = "El campo de la placa es obligatorio")]
        public string? vehiculoFK { get; set; }

        [Required(ErrorMessage = "El campo Fecha Compra es obligatorio")]
        public DateTime fechaCompra { get; set; }

        public DateTime fechaVencimiento { get; set; }
    }
}

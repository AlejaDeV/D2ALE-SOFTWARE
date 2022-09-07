using System.ComponentModel.DataAnnotations;
namespace TallerConfiableD2ALE.Models
{
    public class ServicioModel
    {
        public int IdServicio { get; set; }
        [Required(ErrorMessage = "El campo 'Tipo Servicio' es obligatorio")]
        public string? TipoServicio { get; set; }
        [Required(ErrorMessage = "El campo 'Descripción' es obligatorio")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El campo 'Niveles de Aceite' es obligatorio")]
        public string? NivAceite { get; set; }
        public string? NivLiquidoFrenos { get; set; }
        public string? NivRefrigerante{ get; set; }
        public string? NivLiquidoDireccion{ get; set; }
        [Required(ErrorMessage = "El campo de la fecha es obligatorio")]
        public DateTime FechaCompra{ get; set; }
        public int MaestroFK { get; set; }
        [Required(ErrorMessage = "El campo 'Placa Vehículo' es obligatorio")]
        public string? VehiculoFK { get; set; }
        public int UsuarioFK { get; set; }
    }
}

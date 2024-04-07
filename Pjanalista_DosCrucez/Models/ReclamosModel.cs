using System.ComponentModel.DataAnnotations;

namespace Pjanalista_DosCrucez.Models
{
    public class ReclamosModel
    {
        public int IdReclamo {  get; set; }
        
        [Required(ErrorMessage = "Este campo es obigatorio")]
        public string NombreProveedor {  get; set; }

        [Required(ErrorMessage = "Este campo es obigatorio")]
        public string DireccionProveedor {  get; set; }

        [Required(ErrorMessage = "Este campo es obigatorio")]
        public string TelefonoProveedor {  get; set; }

        [Required(ErrorMessage = "Este campo es obigatorio")]
        public string DetalleReclamo {  get; set; }

        [Required(ErrorMessage = "Este campo es obigatorio")]
        public string NombreConsumidor {  get; set; }

        [Required(ErrorMessage = "Este campo es obigatorio")]
        public string ApellidoConsumidor {  get; set; }
        public string EstadoReclamo {  get; set; }

        [Required(ErrorMessage = "Este campo es obigatorio")]
        public string EmailConsumidor {  get; set; }

        [Required(ErrorMessage = "Este campo es obigatorio")]
        public string DuiConsumidor {  get; set; }

        [Required(ErrorMessage = "Este campo es obigatorio")]
        public string DireccionConsumidor {  get; set; }

        [Required(ErrorMessage = "Este campo es obigatorio")]
        public double MontoReclamo {  get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaRevision { get; set; }
        public int IdEmpleado { get; set; }
        public int IdConsumidor { get; set; }
        public int IdEstado { get; set; }
        public bool activo { get; set; }


    }
}

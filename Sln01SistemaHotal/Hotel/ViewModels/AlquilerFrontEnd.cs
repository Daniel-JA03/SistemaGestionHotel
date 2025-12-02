using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hotel.ViewModels
{
    public class AlquilerFrontEnd
    {
        public int id { get; set; }

        // Datos de Habitación
        public int HabitacionId { get; set; }
        public string? Numero { get; set; }
        public string? TipoHabitacion { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int estado { get; set; }

        // Datos del cliente
        public int idCli { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de documento.")]
        public int? tipoDocId { get; set; }

        public string? tipoDoc { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio.")]
        [StringLength(20, ErrorMessage = "El documento no debe superar los 20 caracteres.")]
        public string? Documento { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(15, ErrorMessage = "El teléfono no debe superar los 15 caracteres.")]
        public string? Telefono { get; set; }

        // Datos del alquiler
        [Required(ErrorMessage = "Debe ingresar la cantidad de días.")]
        [Range(1, 365, ErrorMessage = "Los días deben ser un número válido mayor a 0.")]
        public int dias { get; set; }

        public DateTime FechaActual { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get; set; }
        public decimal total { get; set; }
    }
}

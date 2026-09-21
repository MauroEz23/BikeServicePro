using System.ComponentModel.DataAnnotations;
using BikeServicePro.Models;

namespace BikeServicePro.ViewModels.Servicios
{
    /// <summary>
    /// ViewModel para crear o editar un servicio
    /// </summary>
    public class ServicioCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio base es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo")]
        [Display(Name = "Precio base")]
        public decimal PrecioBase { get; set; }

        [Required(ErrorMessage = "El tiempo estimado es obligatorio")]
        [Range(1, 600, ErrorMessage = "El tiempo estimado debe estar entre 1 y 600 minutos")]
        [Display(Name = "Tiempo estimado (minutos)")]
        public int TiempoEstimado { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [Display(Name = "Categoría")]
        public CategoriaServicio Categoria { get; set; }

        [Display(Name = "Icono")]
        public string? Icono { get; set; }

        [Display(Name = "Requiere repuesto")]
        public string? RequiereRepuesto { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;
    }
}
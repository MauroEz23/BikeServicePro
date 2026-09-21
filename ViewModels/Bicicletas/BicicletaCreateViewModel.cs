using System.ComponentModel.DataAnnotations;

namespace BikeServicePro.ViewModels.Bicicletas
{
    /// <summary>
    /// ViewModel para crear o editar una bicicleta
    /// </summary>
    public class BicicletaCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria")]
        [StringLength(50, ErrorMessage = "La marca no puede exceder los 50 caracteres")]
        [Display(Name = "Marca")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(50, ErrorMessage = "El modelo no puede exceder los 50 caracteres")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; } = string.Empty;

        [Display(Name = "Año")]
        public int? Anio { get; set; }

        [Required(ErrorMessage = "El número de serie es obligatorio")]
        [StringLength(50, ErrorMessage = "El número de serie no puede exceder los 50 caracteres")]
        [Display(Name = "Número de serie")]
        public string NumeroSerie { get; set; } = string.Empty;

        [Display(Name = "Color")]
        public string Color { get; set; } = string.Empty;

        [Display(Name = "Material")]
        public string Material { get; set; } = string.Empty;

        [Display(Name = "Peso (kg)")]
        public double? Peso { get; set; }

        [Display(Name = "Talla")]
        public string Talla { get; set; } = string.Empty;

        [Display(Name = "Grupo")]
        public string Grupo { get; set; } = string.Empty;

        [Display(Name = "Tipo de frenos")]
        public string TipoFrenos { get; set; } = string.Empty;

        [Display(Name = "URL de la foto")]
        public string? FotoUrl { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;
    }
}
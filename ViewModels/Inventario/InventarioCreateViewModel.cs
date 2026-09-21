using System.ComponentModel.DataAnnotations;

namespace BikeServicePro.ViewModels.Inventario
{
    /// <summary>
    /// ViewModel para crear o editar un ítem de inventario
    /// </summary>
    public class InventarioCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [Display(Name = "Categoría")]
        public string Categoria { get; set; } = string.Empty;

        [Display(Name = "Subcategoría")]
        public string SubCategoria { get; set; } = string.Empty;

        [Display(Name = "Código interno")]
        public string? CodigoInterno { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El stock mínimo es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo debe ser un número positivo")]
        [Display(Name = "Stock mínimo")]
        public int StockMinimo { get; set; }

        [Display(Name = "Proveedor")]
        public string? Proveedor { get; set; }

        [Required(ErrorMessage = "El costo es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser un valor positivo")]
        [Display(Name = "Costo")]
        public decimal Costo { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Display(Name = "Código de barras")]
        public string? CodigoBarras { get; set; }

        [Display(Name = "Ubicación")]
        public string? Ubicacion { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;
    }
}
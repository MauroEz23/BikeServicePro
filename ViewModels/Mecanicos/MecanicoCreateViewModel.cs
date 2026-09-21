using System;
using System.ComponentModel.DataAnnotations;

namespace BikeServicePro.ViewModels.Mecanicos
{
    /// <summary>
    /// ViewModel para crear o editar un mecánico
    /// </summary>
    public class MecanicoCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(50, ErrorMessage = "Los apellidos no pueden exceder los 50 caracteres")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "La especialidad es obligatoria")]
        [StringLength(100, ErrorMessage = "La especialidad no puede exceder los 100 caracteres")]
        [Display(Name = "Especialidad")]
        public string Especialidad { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no es válido")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de contratación es obligatoria")]
        [Display(Name = "Fecha de contratación")]
        public DateTime FechaContratacion { get; set; } = DateTime.Now;

        [Display(Name = "URL de la foto")]
        public string? FotoUrl { get; set; }

        [Display(Name = "Certificaciones")]
        public string? Certificaciones { get; set; }

        [Display(Name = "Horario de trabajo")]
        public string? HorarioTrabajo { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;
    }
}
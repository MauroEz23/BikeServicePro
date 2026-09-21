using System;

namespace BikeServicePro.ViewModels.Mecanicos
{
    /// <summary>
    /// ViewModel para el detalle de un mecánico
    /// </summary>
    public class MecanicoDetailViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaContratacion { get; set; }
        public string? FotoUrl { get; set; }
        public string? Certificaciones { get; set; }
        public string? HorarioTrabajo { get; set; }
        public bool Activo { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int AñosExperiencia { get; set; }
        public int ReparacionesAsignadas { get; set; }
        public int ReparacionesActivas { get; set; }
    }
}
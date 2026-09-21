using System;
using System.Collections.Generic;

namespace BikeServicePro.Models
{
    /// <summary>
    /// Modelo de dominio que representa un mecánico del taller
    /// </summary>
    public class Mecanico
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
        public bool Activo { get; set; } = true;
        public string? HorarioTrabajo { get; set; }
        
        // Relaciones
        public List<Reparacion> ReparacionesAsignadas { get; set; } = new();
        
        // Propiedades calculadas
        public string NombreCompleto => $"{Nombre} {Apellidos}";
        public int AñosExperiencia => DateTime.Now.Year - FechaContratacion.Year;
        public int ReparacionesActivas => ReparacionesAsignadas?.Count(r => 
            r.Estado != EstadoReparacion.Entregada && 
            r.Estado != EstadoReparacion.Cancelada) ?? 0;
    }
}
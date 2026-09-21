using System;
using System.Collections.Generic;
using BikeServicePro.Models;

namespace BikeServicePro.ViewModels.Reparaciones
{
    /// <summary>
    /// ViewModel para el detalle completo de una reparación
    /// </summary>
    public class ReparacionDetailViewModel
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public int BicicletaId { get; set; }
        public int? MecanicoId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public EstadoReparacion Estado { get; set; }
        public string Diagnostico { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public Prioridad Prioridad { get; set; }
        public decimal CostoManoObra { get; set; }
        public decimal CostoRepuestos { get; set; }
        public bool EsGarantia { get; set; }
        public string? NotasInternas { get; set; }
        public DateTime FechaActualizacion { get; set; }
        
        // Propiedades calculadas
        public decimal CostoTotal { get; set; }
        public int DiasEnTaller { get; set; }
        public bool EstaAtrasada { get; set; }
        
        // Nombres para mostrar
        public string ClienteNombre { get; set; } = string.Empty;
        public string BicicletaNombre { get; set; } = string.Empty;
        public string MecanicoNombre { get; set; } = string.Empty;
        
        // Historial de cambios (para futura implementación)
        public List<ReparacionHistorial> Historial { get; set; } = new();
    }

    /// <summary>
    /// Modelo para el historial de cambios de una reparación
    /// </summary>
    public class ReparacionHistorial
    {
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public string? Detalles { get; set; }
    }
}
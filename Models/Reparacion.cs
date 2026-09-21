using System;
using System.Collections.Generic;

namespace BikeServicePro.Models
{
    /// <summary>
    /// Modelo de dominio que representa una reparación
    /// </summary>
    public class Reparacion
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
        public bool EsGarantia { get; set; } = false;
        public string? NotasInternas { get; set; }
        public DateTime FechaActualizacion { get; set; }
        
        // Relaciones
        public Cliente Cliente { get; set; } = null!;
        public Bicicleta Bicicleta { get; set; } = null!;
        public Mecanico? Mecanico { get; set; }
        public List<ReparacionServicio> ServiciosRealizados { get; set; } = new();
        
        // Propiedades calculadas
        public decimal CostoTotal => CostoManoObra + CostoRepuestos;
        public int DiasEnTaller => FechaEntrega.HasValue 
            ? (FechaEntrega.Value - FechaIngreso).Days 
            : (DateTime.Now - FechaIngreso).Days;
        public bool EstaAtrasada => Estado != EstadoReparacion.Entregada && 
                                   Estado != EstadoReparacion.Cancelada && 
                                   DiasEnTaller > 7;
    }

    /// <summary>
    /// Estados posibles de una reparación
    /// </summary>
    public enum EstadoReparacion
    {
        Recibida = 1,
        Diagnostico = 2,
        EsperandoRepuestos = 3,
        EnReparacion = 4,
        Lista = 5,
        Entregada = 6,
        Cancelada = 7
    }

    /// <summary>
    /// Niveles de prioridad para una reparación
    /// </summary>
    public enum Prioridad
    {
        Baja = 1,
        Media = 2,
        Alta = 3,
        Urgente = 4
    }

    /// <summary>
    /// Relación entre reparación y servicios
    /// </summary>
    public class ReparacionServicio
    {
        public int Id { get; set; }
        public int ReparacionId { get; set; }
        public int ServicioId { get; set; }
        public int Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; }
        public string? Observaciones { get; set; }
        
        // Relaciones
        public Reparacion Reparacion { get; set; } = null!;
        public Servicio Servicio { get; set; } = null!;
    }
}
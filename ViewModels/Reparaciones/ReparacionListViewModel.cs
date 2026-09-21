using System;
using BikeServicePro.Models;

namespace BikeServicePro.ViewModels.Reparaciones
{
    /// <summary>
    /// ViewModel para la lista de reparaciones
    /// </summary>
    public class ReparacionListViewModel
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
        public DateTime FechaActualizacion { get; set; }
        
        // Propiedades calculadas
        public decimal CostoTotal { get; set; }
        public int DiasEnTaller { get; set; }
        public bool EstaAtrasada { get; set; }
        
        // Nombres para mostrar
        public string ClienteNombre { get; set; } = string.Empty;
        public string BicicletaNombre { get; set; } = string.Empty;
        public string MecanicoNombre { get; set; } = string.Empty;
        
        // Propiedades para UI
        public string EstadoNombre => Estado.ToString();
        public string PrioridadNombre => Prioridad.ToString();
        public string EstadoColor => GetEstadoColor();
        public string PrioridadColor => GetPrioridadColor();

        private string GetEstadoColor()
        {
            return Estado switch
            {
                EstadoReparacion.Recibida => "warning",
                EstadoReparacion.Diagnostico => "info",
                EstadoReparacion.EsperandoRepuestos => "danger",
                EstadoReparacion.EnReparacion => "primary",
                EstadoReparacion.Lista => "success",
                EstadoReparacion.Entregada => "secondary",
                EstadoReparacion.Cancelada => "dark",
                _ => "secondary"
            };
        }

        private string GetPrioridadColor()
        {
            return Prioridad switch
            {
                Prioridad.Baja => "success",
                Prioridad.Media => "warning",
                Prioridad.Alta => "danger",
                Prioridad.Urgente => "danger",
                _ => "secondary"
            };
        }
    }
}
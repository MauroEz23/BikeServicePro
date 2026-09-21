using System;
using System.ComponentModel.DataAnnotations;
using BikeServicePro.Models;

namespace BikeServicePro.ViewModels.Reparaciones
{
    /// <summary>
    /// ViewModel para crear una nueva reparación
    /// </summary>
    public class ReparacionCreateViewModel
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;

        [Required(ErrorMessage = "El cliente es obligatorio")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "La bicicleta es obligatoria")]
        [Display(Name = "Bicicleta")]
        public int BicicletaId { get; set; }

        [Display(Name = "Mecánico")]
        public int? MecanicoId { get; set; }

        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        [Display(Name = "Estado")]
        public EstadoReparacion Estado { get; set; } = EstadoReparacion.Recibida;

        [Required(ErrorMessage = "El diagnóstico es obligatorio")]
        [StringLength(500, ErrorMessage = "El diagnóstico no puede exceder los 500 caracteres")]
        [Display(Name = "Diagnóstico")]
        public string Diagnostico { get; set; } = string.Empty;

        [Display(Name = "Observaciones")]
        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder los 500 caracteres")]
        public string Observaciones { get; set; } = string.Empty;

        [Display(Name = "Prioridad")]
        public Prioridad Prioridad { get; set; } = Prioridad.Media;

        [Display(Name = "Costo mano de obra")]
        public decimal CostoManoObra { get; set; }

        [Display(Name = "Costo repuestos")]
        public decimal CostoRepuestos { get; set; }

        [Display(Name = "Es garantía")]
        public bool EsGarantia { get; set; }

        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

        // Listas para selects
        public List<ClienteSelect> Clientes { get; set; } = new();
        public List<BicicletaSelect> Bicicletas { get; set; } = new();
        public List<MecanicoSelect> Mecanicos { get; set; } = new();
    }

    public class ClienteSelect
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
    }

    public class BicicletaSelect
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
    }

    public class MecanicoSelect
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
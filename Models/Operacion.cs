using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_ModParts.Models;

public enum TipoOperacion
{
    Venta,
    Ingreso,
    Ajuste
}

public enum EstadoOperacion
{
    Completada,
    Cancelada
}

public class Operacion
{
    [Key]
    public int IdOperacion { get; set; }

    [Required]
    public TipoOperacion TipoOperacion { get; set; }

    public long? IdEmpleado { get; set; }

    [ForeignKey(nameof(IdEmpleado))]
    public Empleado? Empleado { get; set; }

    public decimal Total { get; set; }

    public EstadoOperacion Estado { get; set; } = EstadoOperacion.Completada;

    public DateTime FechaOperacion { get; set; } = DateTime.UtcNow;

    public string? Notas { get; set; }

    public virtual ICollection<DetalleOperacion> Detalles { get; set; } = new List<DetalleOperacion>();
    public virtual ICollection<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();
}

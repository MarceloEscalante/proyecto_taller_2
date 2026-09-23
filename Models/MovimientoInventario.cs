using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_ModParts.Models;

public enum TipoMovimiento
{
    Entrada,
    Salida
}

public class MovimientoInventario
{
    [Key]
    public int IdMovimiento { get; set; }

    [Required]
    public int IdProducto { get; set; }

    [ForeignKey(nameof(IdProducto))]
    public Producto? Producto { get; set; }

    public int? IdOperacion { get; set; }

    [ForeignKey(nameof(IdOperacion))]
    public Operacion? Operacion { get; set; }

    [Required]
    public TipoMovimiento TipoMovimiento { get; set; }

    [Required]
    public int Cantidad { get; set; }

    [Required]
    public int StockAnterior { get; set; }

    [Required]
    public int StockNuevo { get; set; }

    public DateTime FechaMovimiento { get; set; } = DateTime.UtcNow;

    public string? Notas { get; set; }
}

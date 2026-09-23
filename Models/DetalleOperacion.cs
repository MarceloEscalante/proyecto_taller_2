using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_ModParts.Models;

public class DetalleOperacion
{
    [Key]
    public int IdDetalleOperacion { get; set; }

    [Required]
    public int IdOperacion { get; set; }

    [ForeignKey(nameof(IdOperacion))]
    public Operacion? Operacion { get; set; }

    [Required]
    public int IdProducto { get; set; }

    [ForeignKey(nameof(IdProducto))]
    public Producto? Producto { get; set; }

    [Required]
    public int Cantidad { get; set; }

    [Required]
    public decimal PrecioUnitario { get; set; }
}

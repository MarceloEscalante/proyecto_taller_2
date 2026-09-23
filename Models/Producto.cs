using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_ModParts.Models;

public class Producto
{
    [Key]
    public int IdProducto { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nombre { get; set; } = string.Empty;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public bool Estado { get; set; } = true;

    public int IdCategoria { get; set; }

    [ForeignKey(nameof(IdCategoria))]
    public Categoria? Categoria { get; set; }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ModParts.Models;

public class Categoria
{
    [Key]
    public int IdCategoria { get; set; }

    [Required]
    [MaxLength(100)]
    public string NombreCategoria { get; set; } = string.Empty;

    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

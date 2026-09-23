using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_ModParts.Models;

[Table("rol")]
public class Rol
{
    [Key]
    [Column("id_rol")]
    public long IdRol { get; set; }

    [Column("nombre_rol")]
    public string NombreRol { get; set; } = string.Empty;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}

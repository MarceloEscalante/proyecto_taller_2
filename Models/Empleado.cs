using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_ModParts.Models;

[Table("empleado")]
public class Empleado
{
    [Key]
    [Column("id_empleado")]
    public long IdEmpleado { get; set; }

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("contraseña")]
    public string Contrasena { get; set; } = string.Empty;

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("id_rol")]
    public long IdRol { get; set; }
}
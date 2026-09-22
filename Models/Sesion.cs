namespace Sistema_ModParts.Models;

public static class Sesion
{
    public static long IdEmpleado { get; private set; }
    public static string Email { get; private set; } = string.Empty;
    public static long IdRol { get; private set; }

    public static void IniciarSesion(long idEmpleado, string email, long idRol)
    {
        IdEmpleado = idEmpleado;
        Email = email;
        IdRol = idRol;
    }

    public static void CerrarSesion()
    {
        IdEmpleado = 0;
        Email = string.Empty;
        IdRol = 0;
    }
}
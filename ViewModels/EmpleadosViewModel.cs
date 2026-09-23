using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sistema_ModParts.Models;
using Sistema_ModParts.Repositories;
using Sistema_ModParts.Services;

namespace Sistema_ModParts.ViewModels;

public partial class EmpleadosViewModel : ViewModelBase
{
    private readonly IEmpleadoService _empleadoService;
    private readonly IRolRepository _rolRepository;

    public EmpleadosViewModel(IEmpleadoService empleadoService, IRolRepository rolRepository)
    {
        _empleadoService = empleadoService;
        _rolRepository = rolRepository;

        Empleados = new ObservableCollection<Empleado>();
        Roles = new ObservableCollection<Rol>();

        _ = CargarDatosAsync();
    }

    public ObservableCollection<Empleado> Empleados { get; }
    public ObservableCollection<Rol> Roles { get; }

    [ObservableProperty]
    private Empleado? _empleadoSeleccionado;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _contrasena = string.Empty;

    [ObservableProperty]
    private Rol? _rolSeleccionado;

    [ObservableProperty]
    private string _mensaje = string.Empty;

    [ObservableProperty]
    private bool _estado = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TituloFormulario))]
    [NotifyPropertyChangedFor(nameof(ContrasenaPlaceholder))]
    private bool _esEdicion;

    public string TituloFormulario => EsEdicion ? "Editar Usuario" : "Crear Usuario";
    public string ContrasenaPlaceholder => EsEdicion ? "(Dejar vacía para mantener)" : "(Obligatoria)";

    partial void OnEmpleadoSeleccionadoChanged(Empleado? value)
    {
        if (value != null)
        {
            Email = value.Email;
            Estado = value.Estado;
            Contrasena = string.Empty; // Nunca cargamos la contraseña existente en texto plano
            
            // Buscar el rol correspondiente en la colección de roles (por referencia u ID)
            foreach (var rol in Roles)
            {
                if (rol.IdRol == value.IdRol)
                {
                    RolSeleccionado = rol;
                    break;
                }
            }
            EsEdicion = true;
        }
        else
        {
            LimpiarFormulario();
        }
    }

    [RelayCommand]
    private async Task CargarDatosAsync()
    {
        Roles.Clear();
        var rolesDb = await _rolRepository.GetAllAsync();
        foreach (var rol in rolesDb)
        {
            Roles.Add(rol);
        }

        await RecargarEmpleadosAsync();
    }

    private async Task RecargarEmpleadosAsync()
    {
        Empleados.Clear();
        var empleadosDb = await _empleadoService.ObtenerTodosAsync();
        foreach (var emp in empleadosDb)
        {
            Empleados.Add(emp);
        }
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        if (RolSeleccionado == null)
        {
            Mensaje = "Debe seleccionar un rol.";
            return;
        }

        if (EsEdicion && EmpleadoSeleccionado != null)
        {
            // Actualizar
            EmpleadoSeleccionado.Email = Email;
            EmpleadoSeleccionado.IdRol = RolSeleccionado.IdRol;
            EmpleadoSeleccionado.Estado = Estado;

            var (exito, msg) = await _empleadoService.ActualizarEmpleadoAsync(EmpleadoSeleccionado, string.IsNullOrWhiteSpace(Contrasena) ? null : Contrasena);
            Mensaje = msg;
            if (exito)
            {
                await RecargarEmpleadosAsync();
                LimpiarFormulario();
            }
        }
        else
        {
            // Crear
            var nuevoEmpleado = new Empleado
            {
                Email = Email,
                IdRol = RolSeleccionado.IdRol,
                Estado = Estado
            };

            var (exito, msg) = await _empleadoService.CrearEmpleadoAsync(nuevoEmpleado, Contrasena);
            Mensaje = msg;
            if (exito)
            {
                await RecargarEmpleadosAsync();
                LimpiarFormulario();
            }
        }
    }

    [RelayCommand]
    private async Task EliminarAsync()
    {
        if (EmpleadoSeleccionado == null) return;

        var (exito, msg) = await _empleadoService.EliminarEmpleadoAsync(EmpleadoSeleccionado.IdEmpleado);
        Mensaje = msg;
        if (exito)
        {
            await RecargarEmpleadosAsync();
            LimpiarFormulario();
        }
    }

    [RelayCommand]
    private void LimpiarFormulario()
    {
        EmpleadoSeleccionado = null;
        Email = string.Empty;
        Estado = true;
        Contrasena = string.Empty;
        RolSeleccionado = null;
        EsEdicion = false;
        // No borramos el mensaje aquí para que el usuario pueda leer el resultado de la última acción.
    }
}

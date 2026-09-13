using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly Action showShell;

    public LoginViewModel(Action showShell)
    {
        this.showShell = showShell ?? throw new ArgumentNullException(nameof(showShell));
    }

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private bool hasError;

    [RelayCommand]
    private async Task IngresarAsync()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Completa Email y Contraseña para continuar.";
            HasError = true;
            return;
        }

        ErrorMessage = string.Empty;
        HasError = false;

        try
        {
            using (var dbContext = new AppDbContext())
            {
                var usuario = await dbContext.Empleados.FirstOrDefaultAsync(u => u.Email == Email);

                if (usuario == null || usuario.Contrasena != Password)
                {
                    ErrorMessage = "Usuario o contraseña incorrectos";
                    HasError = true;
                    Password = string.Empty;
                    return;
                }

                if (!usuario.Estado)
                {
                    ErrorMessage = "Cuenta deshabilitada. Contacte al administrador.";
                    HasError = true;
                    Password = string.Empty;
                    return;
                }

                showShell();
            }
        }
        
        catch (Exception ex)
        {
            Console.WriteLine("\n--- ERRO REAL DO BANCO ---");
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null) 
            {
                Console.WriteLine(ex.InnerException.Message);
            }
            Console.WriteLine("--------------------------\n");

            ErrorMessage = "No hay conexión con el servidor. Verifique su red.";
            HasError = true;
}
    }
}
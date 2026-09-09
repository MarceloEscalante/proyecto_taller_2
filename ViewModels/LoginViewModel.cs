using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    private void Ingresar()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Completa Email y Contraseña para continuar.";
            HasError = true;
            return;
        }

        ErrorMessage = string.Empty;
        HasError = false;
        showShell();
    }
}

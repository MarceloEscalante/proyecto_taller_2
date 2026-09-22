using Avalonia.Controls;
using Sistema_ModParts.ViewModels;

namespace Sistema_ModParts.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Vincula el ViewModel a la vista y define qué hace la acción 'showShell'
        DataContext = new LoginViewModel(() => 
        {
            var shell = new ShellView();
            shell.Show();
            
            // Oculta la ventana de login en lugar de cerrarla
            // Si usamos this.Close(), Avalonia apagará toda la aplicación
            this.Hide(); 
        });
    }
}
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Sistema_ModParts.Components.Operations;
using Sistema_ModParts.Models;
using Sistema_ModParts.ViewModels.Operations;

namespace Sistema_ModParts.Views.Operations;

public partial class PosView : UserControl
{
    public PosView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(System.EventArgs e)
    {
        base.OnDataContextChanged(e);
        
        if (DataContext is PosViewModel vm)
        {
            vm.RequestSearchPartAsync = ShowSearchDialogAsync;
            vm.ShowMessage = (title, message) => 
            {
                // TODO: Usar una librería de MessageBox o INotificationService
                System.Diagnostics.Debug.WriteLine($"[{title}] {message}");
            };
        }
    }

    private async Task<Producto?> ShowSearchDialogAsync(string? initialSearchText)
    {
        var topLevel = TopLevel.GetTopLevel(this) as Window;
        if (topLevel == null) return null;

        var context = new AppDbContext(); 
        var dialogVm = new PartSearchViewModel(context);
        
        if (!string.IsNullOrWhiteSpace(initialSearchText))
        {
            dialogVm.SearchText = initialSearchText;
            dialogVm.SearchCommand.Execute(null);
        }
        else
        {
            dialogVm.SearchCommand.Execute(null); // Load first 50 immediately
        }

        var dialog = new PartSearchDialog(dialogVm);
        return await dialog.ShowDialog<Producto?>(topLevel);
    }
}

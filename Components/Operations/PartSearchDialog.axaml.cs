using Avalonia.Controls;
using Avalonia.Interactivity;
using Sistema_ModParts.ViewModels.Operations;

namespace Sistema_ModParts.Components.Operations;

public partial class PartSearchDialog : Window
{
    public PartSearchDialog()
    {
        InitializeComponent();
    }

    public PartSearchDialog(PartSearchViewModel viewModel) : this()
    {
        DataContext = viewModel;
        viewModel.OnPartSelected = (part) => Close(part);
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close(null);
    }
}

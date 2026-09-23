using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.ViewModels.Operations;

public partial class PartSearchViewModel : ViewModelBase
{
    private readonly AppDbContext _context;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Producto> _searchResults = new();

    [ObservableProperty]
    private Producto? _selectedPart;

    public Action<Producto>? OnPartSelected { get; set; }

    public PartSearchViewModel(AppDbContext context)
    {
        _context = context;
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            var all = await _context.Productos.Take(50).ToListAsync();
            SearchResults = new ObservableCollection<Producto>(all);
            return;
        }

        var results = await _context.Productos
            .Where(p => p.Nombre.ToLower().Contains(SearchText.ToLower()))
            .Take(50)
            .ToListAsync();

        SearchResults = new ObservableCollection<Producto>(results);
    }

    [RelayCommand]
    private void SelectPart()
    {
        if (SelectedPart != null)
        {
            OnPartSelected?.Invoke(SelectedPart);
        }
    }
}

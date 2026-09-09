using Avalonia;
using Avalonia.Controls;

namespace Sistema_ModParts.Components.Dashboard;

public partial class DashboardSection : UserControl
{
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<DashboardSection, string>(nameof(Title), "Section");

    public static readonly StyledProperty<object?> BodyProperty =
        AvaloniaProperty.Register<DashboardSection, object?>(nameof(Body));

    public DashboardSection()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public object? Body
    {
        get => GetValue(BodyProperty);
        set => SetValue(BodyProperty, value);
    }
}

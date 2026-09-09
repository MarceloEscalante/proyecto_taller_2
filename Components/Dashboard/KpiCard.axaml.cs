using Avalonia;
using Avalonia.Controls;

namespace Sistema_ModParts.Components.Dashboard;

public partial class KpiCard : UserControl
{
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<KpiCard, string>(nameof(Title), "Indicator");

    public static readonly StyledProperty<string> ValuePlaceholderProperty =
        AvaloniaProperty.Register<KpiCard, string>(nameof(ValuePlaceholder), "Pending");

    public static readonly StyledProperty<string> StatusTextProperty =
        AvaloniaProperty.Register<KpiCard, string>(nameof(StatusText), "Placeholder");

    public KpiCard()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string ValuePlaceholder
    {
        get => GetValue(ValuePlaceholderProperty);
        set => SetValue(ValuePlaceholderProperty, value);
    }

    public string StatusText
    {
        get => GetValue(StatusTextProperty);
        set => SetValue(StatusTextProperty, value);
    }
}

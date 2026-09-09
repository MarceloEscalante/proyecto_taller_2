using Avalonia;
using Avalonia.Controls;

namespace Sistema_ModParts.Components.Dashboard;

public partial class EmptyState : UserControl
{
    public static readonly StyledProperty<string> IconTextProperty =
        AvaloniaProperty.Register<EmptyState, string>(nameof(IconText), "EMPTY");

    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<EmptyState, string>(nameof(Title), "No content");

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<EmptyState, string>(nameof(Description), "Content will appear here later.");

    public EmptyState()
    {
        InitializeComponent();
    }

    public string IconText
    {
        get => GetValue(IconTextProperty);
        set => SetValue(IconTextProperty, value);
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
}

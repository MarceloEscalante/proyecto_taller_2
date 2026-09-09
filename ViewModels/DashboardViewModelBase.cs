namespace Sistema_ModParts.ViewModels;

public abstract class DashboardViewModelBase : ViewModelBase
{
    protected DashboardViewModelBase(string title, string description, string variantId)
    {
        Title = title;
        Description = description;
        VariantId = variantId;
    }

    public string Title { get; }

    public string Description { get; }

    public string VariantId { get; }
}

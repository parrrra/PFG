namespace GymPlanner.Presentation.Services;

public class BreadcrumbService
{
    public event Action? OnChange;

    private List<BreadcrumbItem> _breadcrumbs = new()
    {
        new("Inicio", "/")
    };

    public IReadOnlyList<BreadcrumbItem> Breadcrumbs => _breadcrumbs;

    public void SetBreadcrumbs(List<BreadcrumbItem> items)
    {
        _breadcrumbs = items;
        NotifyStateChanged();
    }

    public void ResetBreadcrumbs()
    {
        _breadcrumbs = new() { new("Inicio", "/") };
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

    public void Clear() => SetBreadcrumbs([new("Inicio", "/")]);
}


public class BreadcrumbItem
{
    public string Label { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;

    public BreadcrumbItem(string label, string href)
    {
        Label = label;
        Href = href;
    }
}

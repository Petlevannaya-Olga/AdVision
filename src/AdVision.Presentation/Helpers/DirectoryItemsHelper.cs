namespace AdVision.Presentation.Helpers;

public static class DirectoryItemsHelper
{
    public static IReadOnlyList<T> PreparePage<T>(
        IEnumerable<T> items,
        string? nameFilter,
        Func<T, string> nameSelector,
        DirectoryListHelper paging)
    {
        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            items = items.Where(x =>
                nameSelector(x).Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
        }

        var filteredItems = items
            .OrderBy(nameSelector)
            .ToList();

        return paging.ApplyPaging(filteredItems);
    }
}
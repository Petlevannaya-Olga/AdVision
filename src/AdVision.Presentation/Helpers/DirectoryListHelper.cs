namespace AdVision.Presentation.Helpers;

public sealed class DirectoryListHelper(int pageSize)
{
    public int Page { get; private set; } = 1;
    public int TotalCount { get; private set; }
    public int PageSize { get; } = pageSize;

    public int TotalPages => TotalCount == 0
        ? 0
        : (int)Math.Ceiling((double)TotalCount / PageSize);

    public void ResetPage()
    {
        Page = 1;
    }

    public bool CanGoPrevious()
    {
        return Page > 1;
    }

    public bool CanGoNext()
    {
        return Page < TotalPages;
    }

    public void GoPrevious()
    {
        if (CanGoPrevious())
        {
            Page--;
        }
    }

    public void GoNext()
    {
        if (CanGoNext())
        {
            Page++;
        }
    }

    public IReadOnlyList<T> ApplyPaging<T>(IReadOnlyList<T> items)
    {
        TotalCount = items.Count;

        NormalizePage();

        return items
            .Skip((Page - 1) * PageSize)
            .Take(PageSize)
            .ToList();
    }

    private void NormalizePage()
    {
        if (TotalPages > 0 && Page > TotalPages)
        {
            Page = TotalPages;
        }

        if (Page <= 0)
        {
            Page = 1;
        }
    }
}
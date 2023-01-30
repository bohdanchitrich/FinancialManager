namespace UI.Models;

public class PaginationModel
{
    public int PageSize { get; set; } = 1;
    public int CurrentPage { get; set; } = 1;
    public bool NextPageAvailable => CurrentPage * PageSize < Count;
    public bool PreviousPageAvailable => CurrentPage > 1;
    public int Count { get; set; } = 1;

    public Task NextPageAsync(Func<int, Task> nextAction)
    {
        ArgumentNullException.ThrowIfNull(nextAction);
        return nextAction.Invoke(++this.CurrentPage);
    }


    public Task PreviouslyPageAsync(Func<int, Task> nextAction)
    {
        ArgumentNullException.ThrowIfNull(nextAction);
        return nextAction.Invoke(--this.CurrentPage);
    }


}
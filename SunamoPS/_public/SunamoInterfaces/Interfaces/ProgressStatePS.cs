namespace SunamoPS._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Tracks progress state for batch operations with event-based notifications.
/// </summary>
public class ProgressStatePS
{
    /// <summary>
    /// Gets or sets the current item count.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets whether this progress state has registered event handlers.
    /// </summary>
    public bool IsRegistered { get; set; }

    /// <summary>
    /// Initializes the progress state with event handlers.
    /// </summary>
    /// <param name="overallItems">Handler called with total item count.</param>
    /// <param name="anotherItem">Handler called when another item is processed.</param>
    /// <param name="writeProgressBarEnd">Handler called when progress bar should end.</param>
    public void Init(Action<int> overallItems, Action<int> anotherItem, Action writeProgressBarEnd)
    {
        IsRegistered = true;
        AnotherItem += anotherItem;
        OverallItems += overallItems;
        WriteProgressBarEnd += writeProgressBarEnd;
    }

    /// <summary>
    /// Event raised when another item is processed.
    /// </summary>
    public event Action<int>? AnotherItem;

    /// <summary>
    /// Event raised to set the total number of items.
    /// </summary>
    public event Action<int>? OverallItems;

    /// <summary>
    /// Event raised when the progress bar should be closed.
    /// </summary>
    public event Action? WriteProgressBarEnd;

    /// <summary>
    /// Increments count and raises the AnotherItem event.
    /// </summary>
    public void OnAnotherItem()
    {
        Count++;
        OnAnotherItem(Count);
    }

    /// <summary>
    /// Raises the AnotherItem event with the specified count.
    /// </summary>
    /// <param name="count">Current item count.</param>
    public void OnAnotherItem(int count)
    {
        AnotherItem?.Invoke(count);
    }

    /// <summary>
    /// Resets the count and raises the OverallItems event.
    /// </summary>
    /// <param name="totalCount">Total number of items to process.</param>
    public void OnOverallItems(int totalCount)
    {
        Count = 0;
        OverallItems?.Invoke(totalCount);
    }

    /// <summary>
    /// Raises the WriteProgressBarEnd event.
    /// </summary>
    public void OnWriteProgressBarEnd()
    {
        WriteProgressBarEnd?.Invoke();
    }
}

namespace SunamoPS._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Represents the progress state PS type.
/// </summary>
public class ProgressStatePS
{
    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the is registered.
    /// </summary>
    public bool IsRegistered { get; set; }

    /// <summary>
    /// Init.
    /// </summary>
    /// <param name="overallItems">The overall items.</param>
    /// <param name="anotherItem">The another item.</param>
    /// <param name="writeProgressBarEnd">The write progress bar end.</param>
    /// <returns>The init result.</returns>
    public void Init(Action<int> overallItems, Action<int> anotherItem, Action writeProgressBarEnd)
    {
        IsRegistered = true;
        AnotherItem += anotherItem;
        OverallItems += overallItems;
        WriteProgressBarEnd += writeProgressBarEnd;
    }

    /// <summary>
    /// The another item.
    /// </summary>
    public event Action<int>? AnotherItem;

    /// <summary>
    /// The overall items.
    /// </summary>
    public event Action<int>? OverallItems;

    /// <summary>
    /// The write progress bar end.
    /// </summary>
    public event Action? WriteProgressBarEnd;

    /// <summary>
    /// On another item.
    /// </summary>
    public void OnAnotherItem()
    {
        Count++;
        OnAnotherItem(Count);
    }

    /// <summary>
    /// On another item.
    /// </summary>
    /// <returns>The on another item result.</returns>
    public void OnAnotherItem(int count)
    {
        AnotherItem?.Invoke(count);
    }

    /// <summary>
    /// On overall items.
    /// </summary>
    /// <returns>The on overall items result.</returns>
    public void OnOverallItems(int totalCount)
    {
        Count = 0;
        OverallItems?.Invoke(totalCount);
    }

    /// <summary>
    /// On write progress bar end.
    /// </summary>
    /// <param name="Count">The count.</param>
    /// <returns>The on write progress bar end result.</returns>
    public void OnWriteProgressBarEnd()
    {
        WriteProgressBarEnd?.Invoke();
    }
}

namespace SunamoPS._public.SunamoInterfaces.Interfaces;

public class ProgressStatePS
{
    public int Count { get; set; }

    public bool IsRegistered { get; set; }

    public void Init(Action<int> overallItems, Action<int> anotherItem, Action writeProgressBarEnd)
    {
        IsRegistered = true;
        AnotherItem += anotherItem;
        OverallItems += overallItems;
        WriteProgressBarEnd += writeProgressBarEnd;
    }

    public event Action<int>? AnotherItem;

    public event Action<int>? OverallItems;

    public event Action? WriteProgressBarEnd;

    public void OnAnotherItem()
    {
        Count++;
        OnAnotherItem(Count);
    }

    public void OnAnotherItem(int count)
    {
        AnotherItem?.Invoke(count);
    }

    public void OnOverallItems(int totalCount)
    {
        Count = 0;
        OverallItems?.Invoke(totalCount);
    }

    public void OnWriteProgressBarEnd()
    {
        WriteProgressBarEnd?.Invoke();
    }
}

namespace SunamoPS._public.SunamoArgs;

public class PsInvokeArgs
{
    public static readonly PsInvokeArgs Default = new();

    public List<string>? AddBeforeEveryCommand { get; set; } = null;

    public bool IsImmediatelyWritingToStatus { get; set; } = false;

    public string? PathToSaveLoadPsOutput { get; set; } = null;

    public bool IsWritingProgressBar { get; set; } = false;
}

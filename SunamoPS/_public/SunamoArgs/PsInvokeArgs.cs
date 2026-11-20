namespace SunamoPS._public.SunamoArgs;

/// <summary>
/// Represents the ps invoke args type.
/// </summary>
public class PsInvokeArgs
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly PsInvokeArgs Default = new();

    /// <summary>
    /// Gets or sets the add before every command.
    /// </summary>
    public List<string>? AddBeforeEveryCommand { get; set; } = null;

    /// <summary>
    /// Gets or sets the is immediately writing to status.
    /// </summary>
    public bool IsImmediatelyWritingToStatus { get; set; } = false;

    /// <summary>
    /// Gets or sets the path to save load ps output.
    /// </summary>
    public string? PathToSaveLoadPsOutput { get; set; } = null;

    /// <summary>
    /// Gets or sets the is writing progress bar.
    /// </summary>
    public bool IsWritingProgressBar { get; set; } = false;
}

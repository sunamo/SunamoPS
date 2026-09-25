namespace SunamoPS._public.SunamoArgs;

/// <summary>
/// Arguments for PowerShell command invocation.
/// </summary>
public class PsInvokeArgs
{
    /// <summary>
    /// Default instance with no special configuration.
    /// </summary>
    public static readonly PsInvokeArgs Default = new();

    /// <summary>
    /// Gets or sets commands to prepend before every executed command (e.g., cd commands).
    /// </summary>
    public List<string>? AddBeforeEveryCommand { get; set; } = null;

    /// <summary>
    /// Gets or sets whether to immediately write output to console.
    /// </summary>
    public bool IsImmediatelyWritingToStatus { get; set; } = false;

    /// <summary>
    /// Gets or sets the file path for saving/loading PowerShell output cache.
    /// </summary>
    public string? PathToSaveLoadPsOutput { get; set; } = null;

    /// <summary>
    /// Gets or sets whether to write progress bar updates.
    /// </summary>
    public bool IsWritingProgressBar { get; set; } = false;
}

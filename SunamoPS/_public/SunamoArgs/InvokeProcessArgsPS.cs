namespace SunamoPS._public.SunamoArgs;

/// <summary>
/// Arguments for invoking an external process.
/// </summary>
public class InvokeProcessArgsPS
{
    /// <summary>
    /// Gets or sets the working directory for the process. If null, the current directory is used.
    /// </summary>
    public string? WorkingDirectory { get; set; } = null;
}

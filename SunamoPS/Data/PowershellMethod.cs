namespace SunamoPS.Data;

/// <summary>
/// Represents a parsed PowerShell method with its name, body content, and source line number.
/// </summary>
public class PowershellMethod
{
    /// <summary>
    /// Creates a new PowershellMethod instance.
    /// </summary>
    /// <param name="name">Method name including parameter signature.</param>
    /// <param name="content">Full body text of the method.</param>
    /// <param name="line">Starting line number in the source file.</param>
    public PowershellMethod(string name, string content, int line)
    {
        Name = name;
        Content = content;
        Line = line;
    }

    /// <summary>
    /// Gets or sets the method name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the method body content.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets the starting line number in the source file.
    /// </summary>
    public int Line { get; set; }
}

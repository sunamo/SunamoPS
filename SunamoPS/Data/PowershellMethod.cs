namespace SunamoPS.Data;

/// <summary>
/// Represents the powershell method type.
/// </summary>
public class PowershellMethod
{
    /// <summary>
    /// Initializes a new instance of the powershell method class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="content">The content.</param>
    /// <param name="line">The line.</param>
    public PowershellMethod(string name, string content, int line)
    {
        Name = name;
        Content = content;
        Line = line;
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// Gets or sets the line.
    /// </summary>
    public int Line { get; set; }
}

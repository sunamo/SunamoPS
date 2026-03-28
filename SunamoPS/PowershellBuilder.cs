namespace SunamoPS;

/// <summary>
/// Builds PowerShell command scripts using a text builder, supporting cd, cmd /c, yt-dlp, and other commands.
/// </summary>
public class PowershellBuilder : IPowershellBuilderPS
{
    /// <summary>
    /// Creates a new PowershellBuilder with the specified text builder factory.
    /// </summary>
    /// <param name="textBuilderFactory">Factory function that creates a TextBuilderPS. The bool parameter controls list mode.</param>
    public PowershellBuilder(Func<bool, TextBuilderPS> textBuilderFactory)
    {
        TextBuilder = textBuilderFactory(false);
        TextBuilder.PrependEveryNoWhite = "";
    }

    /// <summary>
    /// Gets or sets the text builder used for constructing commands.
    /// </summary>
    public TextBuilderPS TextBuilder { get; set; }

    /// <summary>
    /// Gets or sets the Git bash builder.
    /// </summary>
    public IGitBashBuilderPS? Git { get; set; }

    /// <summary>
    /// Gets or sets the NPM bash builder.
    /// </summary>
    public INpmBashBuilderPS? Npm { get; set; }

    /// <summary>
    /// Clears all accumulated commands.
    /// </summary>
    public void Clear()
    {
        TextBuilder.Clear();
    }

    /// <summary>
    /// Adds raw text to the current command without a newline. Automatically prepends configured prefix.
    /// </summary>
    /// <param name="text">Raw text to append.</param>
    public void AddRaw(string text)
    {
        TextBuilder.Append(text);
    }

    /// <summary>
    /// Adds raw text followed by a newline.
    /// </summary>
    /// <param name="text">Raw text to append as a line.</param>
    public void AddRawLine(string text = "")
    {
        TextBuilder.AppendLine(text);
    }

    /// <summary>
    /// Adds an argument name-value pair to the current command.
    /// </summary>
    /// <param name="argName">Name of the argument.</param>
    /// <param name="argValue">Value of the argument.</param>
    public void AddArg(string argName, string argValue)
    {
        TextBuilder.Append(argName);
        TextBuilder.Append(argValue);
    }

    /// <summary>
    /// Changes directory to the specified path.
    /// </summary>
    /// <param name="path">Target directory path.</param>
    public void Cd(string path)
    {
        TextBuilder.AppendLine("cd \"" + path + "\"");
    }

    /// <summary>
    /// Adds a Remove-Item command with -Force flag.
    /// </summary>
    /// <param name="path">Path of the item to remove.</param>
    public void RemoveItem(string path)
    {
        TextBuilder.AppendLine("Remove-Item " + path + " -Force");
        TextBuilder.AppendLine();
    }

    /// <summary>
    /// Adds a cmd /c command.
    /// </summary>
    /// <param name="command">Command to execute via cmd /c.</param>
    public void CmdC(string command)
    {
        TextBuilder.AppendLine("cmd /c " + command);
    }

    /// <summary>
    /// Returns all accumulated commands as a single string.
    /// </summary>
    /// <returns>String representation of all commands.</returns>
    public override string ToString()
    {
        return TextBuilder.ToString();
    }

    /// <summary>
    /// Converts accumulated commands to a list of strings.
    /// </summary>
    /// <returns>List of command strings.</returns>
    public List<string> ToList()
    {
        return TextBuilder.List ?? new List<string>();
    }

    /// <summary>
    /// Adds a command with a path argument.
    /// </summary>
    /// <param name="commandWithPath">Type of command.</param>
    /// <param name="path">Path argument for the command.</param>
    public void WithPath(CommandWithPath commandWithPath, string path)
    {
        TextBuilder.AppendLine(commandWithPath + " '" + path + "'");
    }

    /// <summary>
    /// Adds a yt-dlp download command.
    /// </summary>
    /// <param name="url">URL to download.</param>
    public void YtDlp(string url)
    {
        TextBuilder.AppendLine("ytp " + url);
    }

    /// <summary>
    /// Creates a new PowershellBuilder instance.
    /// </summary>
    /// <param name="textBuilderFactory">Factory function for creating a TextBuilderPS.</param>
    /// <returns>New PowershellBuilder instance.</returns>
    public static PowershellBuilder Create(Func<bool, TextBuilderPS> textBuilderFactory)
    {
        return new PowershellBuilder(textBuilderFactory);
    }
}

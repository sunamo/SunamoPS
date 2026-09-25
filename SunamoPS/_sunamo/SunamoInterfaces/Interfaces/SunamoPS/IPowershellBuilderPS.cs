namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

/// <summary>
/// Interface for building PowerShell command scripts.
/// </summary>
internal interface IPowershellBuilderPS
{
    /// <summary>
    /// Gets or sets the NPM bash builder.
    /// </summary>
    INpmBashBuilderPS? Npm { get; set; }

    /// <summary>
    /// Gets or sets the Git bash builder.
    /// </summary>
    IGitBashBuilderPS? Git { get; set; }

    /// <summary>
    /// Gets or sets the text builder used for constructing commands.
    /// </summary>
    TextBuilderPS TextBuilder { get; set; }

    /// <summary>
    /// Adds an argument name-value pair to the command.
    /// </summary>
    /// <param name="argName">Name of the argument.</param>
    /// <param name="argValue">Value of the argument.</param>
    void AddArg(string argName, string argValue);

    /// <summary>
    /// Adds raw text to the current command without a newline.
    /// </summary>
    /// <param name="text">Raw text to append.</param>
    void AddRaw(string text);

    /// <summary>
    /// Adds raw text followed by a newline.
    /// </summary>
    /// <param name="text">Raw text to append as a line.</param>
    void AddRawLine(string text);

    /// <summary>
    /// Changes directory to the specified path.
    /// </summary>
    /// <param name="path">Target directory path.</param>
    void Cd(string path);

    /// <summary>
    /// Clears all accumulated commands.
    /// </summary>
    void Clear();

    /// <summary>
    /// Executes a command via cmd /c.
    /// </summary>
    /// <param name="command">Command to execute.</param>
    void CmdC(string command);

    /// <summary>
    /// Adds a command with a path argument.
    /// </summary>
    /// <param name="commandWithPath">Type of command with path.</param>
    /// <param name="path">Path argument for the command.</param>
    void WithPath(CommandWithPath commandWithPath, string path);

    /// <summary>
    /// Adds a Remove-Item command for the specified path.
    /// </summary>
    /// <param name="path">Path of the item to remove.</param>
    void RemoveItem(string path);

    /// <summary>
    /// Converts accumulated commands to a list of strings.
    /// </summary>
    /// <returns>List of command strings.</returns>
    List<string> ToList();

    /// <summary>
    /// Returns the accumulated commands as a single string.
    /// </summary>
    /// <returns>String representation of all commands.</returns>
    string ToString();

    /// <summary>
    /// Adds a yt-dlp download command.
    /// </summary>
    /// <param name="url">URL to download.</param>
    void YtDlp(string url);
}

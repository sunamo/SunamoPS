namespace SunamoPS;

/// <summary>
/// Represents the powershell builder type.
/// </summary>
public class PowershellBuilder : IPowershellBuilderPS
{
    /// <summary>
    /// Initializes a new instance of the powershell builder class.
    /// </summary>
    /// <param name="textBuilderFactory">The text builder factory.</param>
    public PowershellBuilder(Func<bool, TextBuilderPS> textBuilderFactory)
    {
        TextBuilder = textBuilderFactory(false);
        TextBuilder.PrependEveryNoWhite = "";
    }

    /// <summary>
    /// Gets or sets the text builder.
    /// </summary>
    public TextBuilderPS TextBuilder { get; set; }

    /// <summary>
    /// Gets or sets the git.
    /// </summary>
    public IGitBashBuilderPS? Git { get; set; }

    /// <summary>
    /// Gets or sets the npm.
    /// </summary>
    public INpmBashBuilderPS? Npm { get; set; }

    /// <summary>
    /// Clear.
    /// </summary>
    /// <returns>The clear result.</returns>
    public void Clear()
    {
        TextBuilder.Clear();
    }

    /// <summary>
    /// Add raw.
    /// </summary>
    /// <returns>The add raw result.</returns>
    public void AddRaw(string text)
    {
        TextBuilder.Append(text);
    }

    /// <summary>
    /// Add raw line.
    /// </summary>
    /// <returns>The add raw line result.</returns>
    public void AddRawLine(string text = "")
    {
        TextBuilder.AppendLine(text);
    }

    /// <summary>
    /// Add arg.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The add arg result.</returns>
    public void AddArg(string argName, string argValue)
    {
        TextBuilder.Append(argName);
        TextBuilder.Append(argValue);
    }

    /// <summary>
    /// Initializes a new instance of the cd class.
    /// </summary>
    /// <param name="text">The text.</param>
    public void Cd(string path)
    {
        TextBuilder.AppendLine("cd \"" + path + "\"");
    }

    /// <summary>
    /// Initializes a new instance of the remove item class.
    /// </summary>
    /// <param name="argName">The arg name.</param>
    public void RemoveItem(string path)
    {
        TextBuilder.AppendLine("Remove-Item " + path + " -Force");
        TextBuilder.AppendLine();
    }

    /// <summary>
    /// Initializes a new instance of the cmd c class.
    /// </summary>
    /// <param name=""\""">The "\"".</param>
    public void CmdC(string command)
    {
        TextBuilder.AppendLine("cmd /c " + command);
    }

    /// <summary>
    /// Initializes a new instance of the to string class.
    /// </summary>
    /// <param name="-Force"">The force.</param>
    public override string ToString()
    {
        return TextBuilder.ToString();
    }

    /// <summary>
    /// To list.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <returns>The to list result.</returns>
    public List<string> ToList()
    {
        return TextBuilder.List ?? new List<string>();
    }

    /// <summary>
    /// With path.
    /// </summary>
    /// <returns>The with path result.</returns>
    public void WithPath(CommandWithPath commandWithPath, string path)
    {
        TextBuilder.AppendLine(commandWithPath + " '" + path + "'");
    }

    /// <summary>
    /// Yt dlp.
    /// </summary>
    /// <typeparam name="string">The type string.</typeparam>
    /// <returns>The yt dlp result.</returns>
    public void YtDlp(string url)
    {
        TextBuilder.AppendLine("ytp " + url);
    }

    /// <summary>
    /// Create.
    /// </summary>
    /// <param name=""'"">The "'".</param>
    /// <returns>The create result.</returns>
    public static PowershellBuilder Create(Func<bool, TextBuilderPS> textBuilderFactory)
    {
        return new PowershellBuilder(textBuilderFactory);
    }
}

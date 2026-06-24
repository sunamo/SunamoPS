namespace SunamoPS;

public class PowershellBuilder : IPowershellBuilderPS
{
    public PowershellBuilder(Func<bool, TextBuilderPS> textBuilderFactory)
    {
        TextBuilder = textBuilderFactory(false);
        TextBuilder.PrependEveryNoWhite = "";
    }

    public TextBuilderPS TextBuilder { get; set; }

    public IGitBashBuilderPS? Git { get; set; }

    public INpmBashBuilderPS? Npm { get; set; }

    public void Clear()
    {
        TextBuilder.Clear();
    }

    public void AddRaw(string text)
    {
        TextBuilder.Append(text);
    }

    public void AddRawLine(string text = "")
    {
        TextBuilder.AppendLine(text);
    }

    public void AddArg(string argName, string argValue)
    {
        TextBuilder.Append(argName);
        TextBuilder.Append(argValue);
    }

    public void Cd(string path)
    {
        TextBuilder.AppendLine("cd \"" + path + "\"");
    }

    public void RemoveItem(string path)
    {
        TextBuilder.AppendLine("Remove-Item " + path + " -Force");
        TextBuilder.AppendLine();
    }

    public void CmdC(string command)
    {
        TextBuilder.AppendLine("cmd /c " + command);
    }

    public override string ToString()
    {
        return TextBuilder.ToString();
    }

    public List<string> ToList()
    {
        return TextBuilder.List ?? new List<string>();
    }

    public void WithPath(CommandWithPath commandWithPath, string path)
    {
        TextBuilder.AppendLine(commandWithPath + " '" + path + "'");
    }

    public void YtDlp(string url)
    {
        TextBuilder.AppendLine("ytp " + url);
    }

    public static PowershellBuilder Create(Func<bool, TextBuilderPS> textBuilderFactory)
    {
        return new PowershellBuilder(textBuilderFactory);
    }
}

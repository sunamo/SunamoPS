namespace SunamoPS;

/// <summary>
/// Prvně jsem tu měl ci
/// Potom mě napadlo že v asychronním programování to není dobrý nápad a "jako stejné" mě napadlo udělat metodu CreateInstance
/// Nicméně zásada že nebudu měnit to co jsem napsal platí. Musím si prvně nastudovat jak je to s async. Kdy metoda může přistupovat 
/// </summary>
public class PowershellBuilder : IPowershellBuilder
{
    public TextBuilder sb { get; set; } = null;
    public IGitBashBuilder Git { get; set; }
    public INpmBashBuilder Npm { get; set; }

    /// <summary>
    /// musí být public aby šel vytvořit přes .Create
    /// </summary>
    public PowershellBuilder()
    {
        // 15.4.23 na false. čti public TextBuilder(bool useList = false) proč
        sb = new TextBuilder(false);
        sb.prependEveryNoWhite = AllStringsSE.space;
    }

    public static PowershellBuilder Create()
    {
        return new PowershellBuilder();
    }

    public void Clear()
    {
        sb.Clear();
    }

    /// <summary>
    /// Dont postfix with NewLine
    /// Automatically prepend by space
    /// Add to previous command, not create new!
    /// </summary>
    /// <param name="v"></param>
    public void AddRaw(string v)
    {
        sb.Append(v);
    }

    public void AddRawLine(string v = Consts.se)
    {
        sb.AppendLine(v);
    }



    public void AddArg(string argName, string argValue)
    {
        sb.Append(argName);
        sb.Append(argValue);
    }

    /// <summary>
    /// Returns string because of PowershellRunner
    /// </summary>
    /// <param name="path"></param>
    public
void
Cd(string path)
    {
        sb.AppendLine("cd \"" + path + AllStringsSE.qm);
    }

    public void RemoveItem(string v)
    {

        sb.AppendLine("Remove-Item " + v + " -Force");
        sb.AppendLine();
    }

    public void CmdC(string v)
    {
        sb.AppendLine("cmd /c " + v);
    }

    public override string ToString()
    {
        return sb.ToString();
    }

    public List<string> ToList()
    {
        return sb.list;
    }

    public void WithPath(CommandWithPath c, string path)
    {
        sb.AppendLine(c.ToString() + " '" + path + "'");
    }

    public void YtDlp(string url)
    {
        sb.AppendLine("ytp " + url);
    }
}

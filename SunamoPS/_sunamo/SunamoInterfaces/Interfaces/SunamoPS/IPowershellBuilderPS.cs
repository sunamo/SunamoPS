namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

internal interface IPowershellBuilderPS
{
    INpmBashBuilderPS? Npm { get; set; }

    IGitBashBuilderPS? Git { get; set; }

    TextBuilderPS TextBuilder { get; set; }

    void AddArg(string argName, string argValue);

    void AddRaw(string text);

    void AddRawLine(string text);

    void Cd(string path);

    void Clear();

    void CmdC(string command);

    void WithPath(CommandWithPath commandWithPath, string path);

    void RemoveItem(string path);

    List<string> ToList();

    string ToString();

    void YtDlp(string url);
}

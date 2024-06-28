namespace SunamoPS;


internal interface IPowershellBuilder
{
    INpmBashBuilder Npm { get; set; }
    IGitBashBuilder Git { get; set; }
    void AddArg(string argName, string argValue);
    void AddRaw(string v);
    void AddRawLine(string v);
    ///// <summary>
    ///// Musí být i bez QS protože když pracuji s interfacem, do něj nejde default hodnota - možná jde, viz výše
    ///// </summary>
    //void AddRawLine();
    /// <summary>
    /// dříve to vracelo string ale je to hovadina a zbytečně zmařený výkon
    /// </summary>
    /// <param name="path"></param>
    void Cd(string path);
    void Clear();
    void CmdC(string v);
    /// <summary>
    /// jsem předal při mergi, nevím zda tu patří
    /// </summary>
    /// <param name="c"></param>
    /// <param name="path"></param>
    void WithPath(CommandWithPath c, string path);
    void RemoveItem(string v);
    List<string> ToList();
    string ToString();
    void YtDlp(string url);
    ITextBuilder sb { get; set; }
}
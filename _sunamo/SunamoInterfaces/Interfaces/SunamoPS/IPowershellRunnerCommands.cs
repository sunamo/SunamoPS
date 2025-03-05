namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

internal interface IPowershellRunnerCommands
{
#if ASYNC
    Task<List<List<string>>>
#else
List<List<string>>
#endif
        Invoke(List<string> commands);
#if ASYNC
    Task<List<List<string>>>
#else
List<List<string>>
#endif
        Invoke(List<string> commands, PsInvokeArgs e = null);
    ProgressStatePS clpb { get; set; }
    #region Když to bylo instanční, nechtělo mi to z nějakého důvodu fungovat. Nastavilo se true ale vracelo se furt false
    bool SaveUsedCommandToDictionary { get; set; }
    Dictionary<string, List<string>> UsedCommandsInFolders { get; set; }
    #endregion
    //List<string> ProcessPSObjects(ICollection<PSObject> pso);
}
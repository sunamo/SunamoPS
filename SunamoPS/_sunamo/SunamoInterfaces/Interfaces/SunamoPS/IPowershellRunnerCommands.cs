namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

internal interface IPowershellRunnerCommands
{
    Task<List<List<string>>>
        Invoke(List<string> commands);
    Task<List<List<string>>>
        Invoke(List<string> commands, PsInvokeArgs? invokeArgs = null);

    ProgressStatePS ProgressState { get; set; }

    bool SaveUsedCommandToDictionary { get; set; }

    Dictionary<string, List<string>> UsedCommandsInFolders { get; set; }
}

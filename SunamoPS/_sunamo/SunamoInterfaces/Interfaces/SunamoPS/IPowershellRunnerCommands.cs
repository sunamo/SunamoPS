namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

/// <summary>
/// Interface for PowerShell command invocation with progress tracking and command history.
/// </summary>
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
        Invoke(List<string> commands, PsInvokeArgs? invokeArgs = null);

    /// <summary>
    /// Gets or sets the progress state for tracking command execution.
    /// </summary>
    ProgressStatePS ProgressState { get; set; }

    /// <summary>
    /// Gets or sets whether executed commands should be saved to a dictionary.
    /// </summary>
    bool SaveUsedCommandToDictionary { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of used commands mapped by folder.
    /// </summary>
    Dictionary<string, List<string>> UsedCommandsInFolders { get; set; }
}

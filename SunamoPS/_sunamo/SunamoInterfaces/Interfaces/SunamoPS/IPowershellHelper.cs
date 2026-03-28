namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

/// <summary>
/// Interface for PowerShell helper operations.
/// </summary>
internal interface IPowershellHelper
{
#if ASYNC
    Task
#else
void
#endif
        CmdC(string command, Func<bool, TextBuilderPS> textBuilderFactory);
#if ASYNC
    Task<string?>
#else
string?
#endif
        DetectLanguageForFileGithubLinguist(string windowsPath);

    /// <summary>
    /// Gets the names of all running processes.
    /// </summary>
    /// <returns>List of process names.</returns>
    List<string> ProcessNames();
}

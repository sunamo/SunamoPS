namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

/// <summary>
/// Interface for PowerShell helper operations.
/// </summary>
internal interface IPowershellHelper
{
    Task
        CmdC(string command, Func<bool, TextBuilderPS> textBuilderFactory);
    Task<string?>
        DetectLanguageForFileGithubLinguist(string windowsPath);

    /// <summary>
    /// Gets the names of all running processes.
    /// </summary>
    /// <returns>List of process names.</returns>
    List<string> ProcessNames();
}

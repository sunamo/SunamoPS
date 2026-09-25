namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

internal interface IPowershellHelper
{
    Task
        CmdC(string command, Func<bool, TextBuilderPS> textBuilderFactory);
    Task<string?>
        DetectLanguageForFileGithubLinguist(string windowsPath);

    List<string> ProcessNames();
}

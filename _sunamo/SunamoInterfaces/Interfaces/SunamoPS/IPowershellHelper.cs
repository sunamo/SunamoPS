namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;


internal interface IPowershellHelper
{
#if ASYNC
    Task
#else
void
#endif
    CmdC(string v, Func<bool, TextBuilderPS> ciTextBuilder);
#if ASYNC
    Task<string>
#else
string
#endif
    DetectLanguageForFileGithubLinguist(string windowsPath);
    List<string> ProcessNames();
}
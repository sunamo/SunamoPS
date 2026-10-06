namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

internal interface IPowershellRunner<T>
{
    Task<T> InvokeInFolder(string folder, string command);

    Task<T> InvokeSingle(string command);

    Task<T> InvokeLinesFromString(string text, bool isWritingProgressBar);

    Task<T> InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS? processArgs = null);
}

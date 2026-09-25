namespace SunamoPS;

public class PowershellRunnerString : IPowershellRunner<string>
{
    private PowershellRunnerString()
    {
    }

    public static PowershellRunnerString Instance { get; } = new();

    public async Task<string> InvokeInFolder(string folder, string command)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeInFolder(folder, command));
    }

    public async Task<string> InvokeLinesFromString(string text, bool isWritingProgressBar)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeLinesFromString(text, isWritingProgressBar));
    }

    public async Task<string> InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS? processArgs = null)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeProcess(exeFileNameWithoutPath, arguments, processArgs));
    }

    public async Task<string> InvokeSingle(string command)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeSingle(command));
    }
}

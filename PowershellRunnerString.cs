namespace SunamoPS;

public class PowershellRunnerString : IPowershellRunner<string>
{
    private PowershellRunnerString()
    {
    }
    public static PowershellRunnerString ci = new();
    //public bool SaveUsedCommandToDictionary { get => PowershellRunner.ci.SaveUsedCommandToDictionary; set => PowershellRunner.ci.SaveUsedCommandToDictionary = value; }
    //public Dictionary<string, List<string>> UsedCommandsInFolders { get => PowershellRunner.ci.UsedCommandsInFolders; set => PowershellRunner.ci.UsedCommandsInFolders = value; }
    public async Task<string> InvokeInFolder(string folder, string command)
    {
        return SHJoin.JoinNL(await PowershellRunner.ci.InvokeInFolder(folder, command));
    }
    public async Task<string> InvokeLinesFromString(string v, bool writePb)
    {
        return SHJoin.JoinNL(await PowershellRunner.ci.InvokeLinesFromString(v, writePb));
    }
    public async Task<string> InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS a = null)
    {
        return SHJoin.JoinNL(await PowershellRunner.ci.InvokeProcess(exeFileNameWithoutPath, arguments, a));
    }
    public async Task<string> InvokeSingle(string command)
    {
        return SHJoin.JoinNL(await PowershellRunner.ci.InvokeSingle(command));
    }
}
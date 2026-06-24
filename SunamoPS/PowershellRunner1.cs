namespace SunamoPS;

public partial class PowershellRunner : PsOutput, IPowershellRunner<List<string>>
{
    public
        async Task<List<string>>
    InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS? processArgs = null)
    {
        processArgs ??= new InvokeProcessArgsPS();

        if (!exeFileNameWithoutPath.EndsWith(AllExtensions.Exe))
            exeFileNameWithoutPath += AllExtensions.Exe;

        var process = new Process();
        process.StartInfo.FileName = exeFileNameWithoutPath;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        if (processArgs.WorkingDirectory != null)
            process.StartInfo.WorkingDirectory = processArgs.WorkingDirectory;
        process.Start();

        var standardOutput = process.StandardOutput.ReadToEnd();
        await process.WaitForExitAsync();
        var result = SHGetLines.GetLines(standardOutput);
        return result;
    }

    public async Task<List<string>> InvokeInFolder(string folder, string command)
    {
        List<string> commands = new(2);
        commands.Add("cd " + folder);
        commands.Add(command);
        var output = await Invoke(commands);
        return SHGetLines.GetLinesFromLinesWithOneRow(output[0]);
    }

    public Dictionary<string, List<string>> UsedCommandsInFolders { get; set; } = new();
}

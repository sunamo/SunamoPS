namespace SunamoPS;

/// <summary>
/// Partial class extending PowershellRunner with process invocation and folder-based command execution.
/// </summary>
public partial class PowershellRunner : PsOutput, IPowershellRunner<List<string>>
{
    /// <summary>
    /// Invokes an external process and returns its standard output as a list of lines.
    /// If the exe file name does not contain an extension, .exe is appended automatically.
    /// The file name must not be a full path, only the executable name.
    /// </summary>
    /// <param name="exeFileNameWithoutPath">Executable file name without full path.</param>
    /// <param name="arguments">Command-line arguments for the process.</param>
    /// <param name="processArgs">Optional process invocation arguments (e.g., working directory).</param>
    /// <returns>List of output lines from the process.</returns>
    public
#if ASYNC
        async Task<List<string>>
#else
    List<string>
#endif
    InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS? processArgs = null)
    {
        if (processArgs == null)
            processArgs = new InvokeProcessArgsPS();

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
#if ASYNC
        await process.WaitForExitAsync();
#else
        process.WaitForExit();
#endif
        var result = SHGetLines.GetLines(standardOutput);
        return result;
    }

    /// <summary>
    /// Invokes a command in a specified folder by first changing directory.
    /// The cd command output is excluded from results, so index [0] contains the actual command output.
    /// </summary>
    /// <param name="folder">Folder to change to before executing the command.</param>
    /// <param name="command">Command to execute in the folder.</param>
    /// <returns>List of output lines from the command.</returns>
    public async Task<List<string>> InvokeInFolder(string folder, string command)
    {
        List<string> commands = new(2);
        commands.Add("cd " + folder);
        commands.Add(command);
        var output = await Invoke(commands);
        return SHGetLines.GetLinesFromLinesWithOneRow(output[0]);
    }

    /// <summary>
    /// Gets or sets the dictionary of used commands mapped by folder path.
    /// </summary>
    public Dictionary<string, List<string>> UsedCommandsInFolders { get; set; } = new();
}

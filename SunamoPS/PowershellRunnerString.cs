namespace SunamoPS;

/// <summary>
/// PowerShell runner that returns results as single joined strings instead of lists.
/// Delegates all operations to the main PowershellRunner instance.
/// </summary>
public class PowershellRunnerString : IPowershellRunner<string>
{
    private PowershellRunnerString()
    {
    }

    /// <summary>
    /// Singleton instance of PowershellRunnerString.
    /// </summary>
    public static PowershellRunnerString Instance { get; } = new();

    /// <summary>
    /// Invokes a command in a specified folder and returns the output as a single string.
    /// </summary>
    /// <param name="folder">Folder to change to before executing.</param>
    /// <param name="command">Command to execute.</param>
    /// <returns>Command output as a newline-joined string.</returns>
    public async Task<string> InvokeInFolder(string folder, string command)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeInFolder(folder, command));
    }

    /// <summary>
    /// Invokes commands parsed from a multi-line string and returns combined output as a string.
    /// </summary>
    /// <param name="text">Multi-line string of commands.</param>
    /// <param name="isWritingProgressBar">Whether to write progress bar updates.</param>
    /// <returns>Combined output as a newline-joined string.</returns>
    public async Task<string> InvokeLinesFromString(string text, bool isWritingProgressBar)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeLinesFromString(text, isWritingProgressBar));
    }

    /// <summary>
    /// Invokes an external process and returns its output as a single string.
    /// </summary>
    /// <param name="exeFileNameWithoutPath">Executable file name without full path.</param>
    /// <param name="arguments">Command-line arguments for the process.</param>
    /// <param name="processArgs">Optional process invocation arguments.</param>
    /// <returns>Process output as a newline-joined string.</returns>
    public async Task<string> InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS? processArgs = null)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeProcess(exeFileNameWithoutPath, arguments, processArgs));
    }

    /// <summary>
    /// Invokes a single command and returns its output as a string.
    /// </summary>
    /// <param name="command">Command to execute.</param>
    /// <returns>Command output as a newline-joined string.</returns>
    public async Task<string> InvokeSingle(string command)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeSingle(command));
    }
}

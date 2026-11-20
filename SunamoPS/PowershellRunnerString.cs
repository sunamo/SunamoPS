namespace SunamoPS;

/// <summary>
/// Represents the powershell runner string type.
/// </summary>
public class PowershellRunnerString : IPowershellRunner<string>
{
    private PowershellRunnerString()
    {
    }

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static PowershellRunnerString Instance { get; } = new();

    /// <summary>
    /// Invoke in folder.
    /// </summary>
    /// <returns>The invoke in folder result.</returns>
    public async Task<string> InvokeInFolder(string folder, string command)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeInFolder(folder, command));
    }

    /// <summary>
    /// Invoke lines from string.
    /// </summary>
    /// <param name="command)">The command.</param>
    /// <returns>The invoke lines from string result.</returns>
    public async Task<string> InvokeLinesFromString(string text, bool isWritingProgressBar)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeLinesFromString(text, isWritingProgressBar));
    }

    /// <summary>
    /// Invoke process.
    /// </summary>
    /// <param name="isWritingProgressBar)">The is writing progress bar.</param>
    /// <returns>The invoke process result.</returns>
    public async Task<string> InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS? processArgs = null)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeProcess(exeFileNameWithoutPath, arguments, processArgs));
    }

    /// <summary>
    /// Invoke single.
    /// </summary>
    /// <param name="processArgs)">The process args.</param>
    /// <returns>The invoke single result.</returns>
    public async Task<string> InvokeSingle(string command)
    {
        return SHJoin.JoinNL(await PowershellRunner.Instance.InvokeSingle(command));
    }
}

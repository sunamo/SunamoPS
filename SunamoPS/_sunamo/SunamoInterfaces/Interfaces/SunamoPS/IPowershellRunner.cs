namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

/// <summary>
/// Interface for running PowerShell commands and processes.
/// Invoke - runs multiple commands via InvokeWorker.
/// InvokeLinesFromString - runs multiple commands with progress bar.
/// InvokeProcess - starts an external process and returns its output.
/// InvokeSingle - runs a single command.
/// </summary>
/// <typeparam name="T">Return type for command results.</typeparam>
internal interface IPowershellRunner<T>
{
    /// <summary>
    /// Invokes a command in a specified folder by first changing directory.
    /// </summary>
    /// <param name="folder">Folder to change to before executing.</param>
    /// <param name="command">Command to execute.</param>
    /// <returns>Command output.</returns>
    Task<T> InvokeInFolder(string folder, string command);

    /// <summary>
    /// Invokes a single PowerShell command.
    /// </summary>
    /// <param name="command">Command to execute.</param>
    /// <returns>Command output.</returns>
    Task<T> InvokeSingle(string command);

    /// <summary>
    /// Invokes commands parsed from a multi-line string.
    /// </summary>
    /// <param name="text">Multi-line string of commands.</param>
    /// <param name="isWritingProgressBar">Whether to write progress bar updates.</param>
    /// <returns>Command output.</returns>
    Task<T> InvokeLinesFromString(string text, bool isWritingProgressBar);

    /// <summary>
    /// Invokes an external process and returns its output.
    /// </summary>
    /// <param name="exeFileNameWithoutPath">Executable file name without full path.</param>
    /// <param name="arguments">Command-line arguments for the process.</param>
    /// <param name="processArgs">Optional process invocation arguments.</param>
    /// <returns>Process output.</returns>
    Task<T> InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS? processArgs = null);
}

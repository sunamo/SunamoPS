namespace SunamoPS;

/// <summary>
/// Static utility class for calling PowerShell commands and initializing the PS subsystem.
/// </summary>
public class PS
{
    /// <summary>
    /// Executes a PowerShell command and returns the standard output.
    /// </summary>
    /// <param name="command">PowerShell command to execute.</param>
    /// <returns>Standard output of the command.</returns>
    public static string CallPowershellCommand(string command)
    {
        using (var process = new Process())
        {
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.Arguments = "-Command " + command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            return process.StandardOutput.ReadToEnd();
        }
    }

    /// <summary>
    /// Initializes the PowerShell subsystem. This method allows using PowerShell from shared projects
    /// without directly importing SunamoPS. Only needed in the main executable assembly.
    /// </summary>
    public static void Init()
    {
    }
}

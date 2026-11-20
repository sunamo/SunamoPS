namespace SunamoPS;

/// <summary>
/// Represents the PS type.
/// </summary>
public class PS
{
    /// <summary>
    /// Call powershell command.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <returns>The call powershell command result.</returns>
    public static string CallPowershellCommand(string command)
    {
        using var process = new Process();
        process.StartInfo.FileName = "powershell.exe";
        process.StartInfo.Arguments = "-Command " + command;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();
        return process.StandardOutput.ReadToEnd();
    }

    /// <summary>
    /// Init.
    /// </summary>
    public static void Init()
    {
    }
}

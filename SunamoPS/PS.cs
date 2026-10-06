namespace SunamoPS;

public class PS
{
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

    public static void Init()
    {
    }
}

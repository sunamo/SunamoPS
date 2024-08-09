namespace SunamoPS;

public class PowershellHelper : IPowershellHelper
{
    private const string lang = "language:";
    public static PowershellHelper ci = new();

    private PowershellHelper()
    {
    }

    public List<string> ProcessNames()
    {
        var processNames = new List<string>();
        var ps = PowerShell.Create();
        ps.AddCommand("Get-Process");
        var processes = ps.Invoke();
        foreach (var item in processes)
        {
            var process = (Process)item.BaseObject;
            processNames.Add(process.ProcessName);
        }

        return processNames;
    }

    public
#if ASYNC
        async Task
#else
void
#endif
        CmdC(string v, Func<bool, TextBuilderPS> ci)
    {
        var ps = PowershellBuilder.Create(ci);
        ps.CmdC(v);

#if ASYNC
        await
#endif
            PowershellRunner.ci.Invoke(ps.ToList());
    }

    public
#if ASYNC
        async Task<string>
#else
string
#endif
        DetectLanguageForFileGithubLinguist(string windowsPath)
    {
        string command = null;
        string arguments = null;

        // With WSL or WSL 2 not working. In both cases Powershell returns right values but in c# everything empty. Asked on StackOverflow
        var linuxPath = new StringBuilder();
        linuxPath.Append("/mnt/");
        linuxPath.Append(windowsPath[0].ToString().ToLower());
        var parts = SHSplit.SplitMore(windowsPath, AllStrings.bs);
        for (var i = 1; i < parts.Count; i++) linuxPath.Append("/" + parts[i]);

        command = "wsl";
        arguments = " bash -c \"github-linguist '" + linuxPath + "'\"";

        var lines =
#if ASYNC
            await
#endif
                PowershellRunner.ci.InvokeProcess(command + ".exe", arguments);

        var line = lines.First(d => d.Contains(lang)); //CA.ReturnWhichContains(lines, lang).First();
        if (line == null) return null;
        var result = line.Replace(lang, string.Empty).Trim();
        return result;
    }
}
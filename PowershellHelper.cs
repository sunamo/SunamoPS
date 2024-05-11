using SunamoPS._sunamo;


namespace SunamoPS;

public class PowershellHelper : IPowershellHelper
{
    public static PowershellHelper ci = new PowershellHelper();

    private PowershellHelper()
    {

    }

    public List<string> ProcessNames()
    {
        List<string> processNames = new List<string>();
        PowerShell ps = PowerShell.Create();
        ps.AddCommand("Get-Process");
        var processes = ps.Invoke();
        foreach (var item in processes)
        {
            Process process = (Process)item.BaseObject;
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
CmdC(string v, Func<bool, ITextBuilder> ci)
    {
        PowershellBuilder ps = PowershellBuilder.Create(ci);
        ps.CmdC(v);

#if ASYNC
        await
#endif
        PowershellRunner.ci.Invoke(ps.ToList());
    }

    const string lang = "language:";

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
        StringBuilder linuxPath = new StringBuilder();
        linuxPath.Append("/mnt/");
        linuxPath.Append(windowsPath[0].ToString().ToLower());
        var parts = SHSplit.Split(windowsPath, AllStrings.bs);
        for (int i = 1; i < parts.Count; i++)
        {
            linuxPath.Append("/" + parts[i]);
        }

        command = "wsl";
        arguments = " bash -c \"github-linguist '" + linuxPath + "'\"";

        var lines =
#if ASYNC
await
#endif
PowershellRunner.ci.InvokeProcess(command + ".exe", arguments);

        var line = lines.First(d => d.Contains(lang)); //CA.ReturnWhichContains(lines, lang).First();
        if (line == null)
        {
            return null;
        }
        var result = line.ToString().Replace(lang, string.Empty).Trim();
        return result;
    }
}

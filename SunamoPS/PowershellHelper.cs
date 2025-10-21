// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoPS;

public class PowershellHelper : IPowershellHelper
{
    private const string lang = "language:";
    public static PowershellHelper ci = new();
    public static string FindDuplicatedMethodsInPs1File(List<PowershellMethod> methodsNamesContents)
    {
        var grouped = methodsNamesContents.GroupBy(d => d.Name);
        var ordered = grouped.OrderByDescending(d => d.Count());
        StringBuilder sameContent = new();
        StringBuilder differentContent = new();
        StringBuilder noCheckForContent = new();
        foreach (var method in ordered)
        {
            noCheckForContent.AppendLine($"{method.First().Name} on lines {string.Join(",", method.Select(d => d.Line).Order())}");
        }
        return noCheckForContent.ToString();
    }
    public static List<PowershellMethod> ParseMethods(string powerShellCode)
    {
        List<PowershellMethod> methodsNamesContents = new();
        Token[] tokens;
        ParseError[] errors;
        ScriptBlockAst ast = Parser.ParseInput(powerShellCode, out tokens, out errors);
        var functionDefinitions = ast.FindAll(ast => ast is FunctionDefinitionAst, true);
        foreach (FunctionDefinitionAst functionDefinition in functionDefinitions)
        {
            string functionName = functionDefinition.Name;
            string functionArgs = "";
            var args = functionDefinition.Parameters;
            if (args != null && args.Any())
            {
                var argsLine = args.Select(d => d.Name.ToString()).ToList();
                functionArgs = "(" + string.Join(",", argsLine) + ")";
            }
            string functionBody = functionDefinition.Body.Extent.Text;
            methodsNamesContents.Add(new(functionName + functionArgs, functionBody, functionDefinition.Body.Extent.StartLineNumber));
        }
        return methodsNamesContents;
    }
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
        var parts = SHSplit.Split(windowsPath, "\"");
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
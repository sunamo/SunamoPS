namespace SunamoPS;

public class PowershellHelper : IPowershellHelper
{
    private const string languagePrefix = "language:";

    public static PowershellHelper Instance { get; } = new();

    public static string FindDuplicatedMethodsInPs1File(List<PowershellMethod> methods)
    {
        var grouped = methods.GroupBy(method => method.Name);
        var ordered = grouped.OrderByDescending(group => group.Count());
        StringBuilder resultBuilder = new();
        foreach (var methodGroup in ordered)
        {
            resultBuilder.AppendLine($"{methodGroup.First().Name} on lines {string.Join(",", methodGroup.Select(method => method.Line).Order())}");
        }
        return resultBuilder.ToString();
    }

    public static List<PowershellMethod> ParseMethods(string powerShellCode)
    {
        List<PowershellMethod> methods = new();
        Token[] tokens;
        ParseError[] errors;
        ScriptBlockAst scriptBlockAst = Parser.ParseInput(powerShellCode, out tokens, out errors);
        var functionDefinitions = scriptBlockAst.FindAll(node => node is FunctionDefinitionAst, true);
        foreach (FunctionDefinitionAst functionDefinition in functionDefinitions)
        {
            string functionName = functionDefinition.Name;
            string functionArguments = "";
            var parameters = functionDefinition.Parameters;
            if (parameters != null && parameters.Any())
            {
                var parameterNames = parameters.Select(parameter => parameter.Name.ToString()).ToList();
                functionArguments = "(" + string.Join(",", parameterNames) + ")";
            }
            string functionBody = functionDefinition.Body.Extent.Text;
            methods.Add(new(functionName + functionArguments, functionBody, functionDefinition.Body.Extent.StartLineNumber));
        }
        return methods;
    }

    private PowershellHelper()
    {
    }

    public List<string> ProcessNames()
    {
        var processNames = new List<string>();
        var powerShell = PowerShell.Create();
        powerShell.AddCommand("Get-Process");
        var processes = powerShell.Invoke();
        foreach (var item in processes)
        {
            var process = (Process)item.BaseObject;
            processNames.Add(process.ProcessName);
        }
        return processNames;
    }

    public
        async Task
        CmdC(string command, Func<bool, TextBuilderPS> textBuilderFactory)
    {
        var builder = PowershellBuilder.Create(textBuilderFactory);
        builder.CmdC(command);
        await
            PowershellRunner.Instance.Invoke(builder.ToList());
    }

    public
        async Task<string?>
        DetectLanguageForFileGithubLinguist(string windowsPath)
    {
        string command;
        var linuxPath = new StringBuilder();
        linuxPath.Append("/mnt/");
        linuxPath.Append(windowsPath[0].ToString().ToLower());
        var pathParts = SHSplit.Split(windowsPath, "\"");
        for (var i = 1; i < pathParts.Count; i++) linuxPath.Append("/" + pathParts[i]);
        command = "wsl";
        string arguments = " bash -c \"github-linguist '" + linuxPath + "'\"";
        var lines =
            await
                PowershellRunner.Instance.InvokeProcess(command + ".exe", arguments);
        var languageLine = lines.First(line => line.Contains(languagePrefix));
        if (languageLine == null) return null;
        var result = languageLine.Replace(languagePrefix, string.Empty).Trim();
        return result;
    }
}

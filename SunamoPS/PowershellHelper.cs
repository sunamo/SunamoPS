namespace SunamoPS;

/// <summary>
/// Helper class providing PowerShell utility methods for process listing, language detection, and script parsing.
/// </summary>
public class PowershellHelper : IPowershellHelper
{
    private const string languagePrefix = "language:";

    /// <summary>
    /// Singleton instance of PowershellHelper.
    /// </summary>
    public static PowershellHelper Instance { get; } = new();

    /// <summary>
    /// Finds duplicated method names in a parsed PowerShell script.
    /// </summary>
    /// <param name="methods">List of parsed PowerShell methods.</param>
    /// <returns>Formatted string listing method names and their line numbers.</returns>
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

    /// <summary>
    /// Parses PowerShell code into a list of method definitions with their names, bodies, and line numbers.
    /// </summary>
    /// <param name="powerShellCode">PowerShell source code to parse.</param>
    /// <returns>List of parsed PowerShell methods.</returns>
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

    /// <summary>
    /// Gets the names of all currently running processes.
    /// </summary>
    /// <returns>List of process names.</returns>
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

    /// <summary>
    /// Executes a command via cmd /c in a PowerShell session.
    /// </summary>
    /// <param name="command">Command to execute.</param>
    /// <param name="textBuilderFactory">Factory function for creating a TextBuilderPS.</param>
    public
#if ASYNC
        async Task
#else
void
#endif
        CmdC(string command, Func<bool, TextBuilderPS> textBuilderFactory)
    {
        var builder = PowershellBuilder.Create(textBuilderFactory);
        builder.CmdC(command);
#if ASYNC
        await
#endif
            PowershellRunner.Instance.Invoke(builder.ToList());
    }

    /// <summary>
    /// Detects the programming language of a file using GitHub Linguist via WSL.
    /// </summary>
    /// <param name="windowsPath">Windows file path to analyze.</param>
    /// <returns>Detected language name, or null if not found.</returns>
    public
#if ASYNC
        async Task<string?>
#else
string?
#endif
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
#if ASYNC
            await
#endif
                PowershellRunner.Instance.InvokeProcess(command + ".exe", arguments);
        var languageLine = lines.First(line => line.Contains(languagePrefix));
        if (languageLine == null) return null;
        var result = languageLine.Replace(languagePrefix, string.Empty).Trim();
        return result;
    }
}

namespace SunamoPS;

/// <summary>
/// Core PowerShell command runner that executes commands and returns structured output.
/// </summary>
public partial class PowershellRunner : PsOutput, IPowershellRunner<List<string>>
{
    /// <summary>
    /// Singleton instance of PowershellRunner.
    /// </summary>
    public static PowershellRunner Instance { get; } = new();

    private bool saveUsedCommandToDictionary;

    private PowershellRunner()
    {
    }

    /// <summary>
    /// Gets or sets the progress state for tracking command execution.
    /// </summary>
    public ProgressStatePS ProgressState { get; set; } = new();

    /// <summary>
    /// Gets or sets whether executed commands should be saved to a dictionary.
    /// </summary>
    public bool SaveUsedCommandToDictionary
    {
        get
        {
            return saveUsedCommandToDictionary;
        }

        set
        {
            saveUsedCommandToDictionary = value;
        }
    }

    /// <summary>
    /// Invokes a list of PowerShell commands and returns structured output.
    /// For each command, returns at least one entry in the result.
    /// </summary>
    /// <param name="commands">List of PowerShell commands to execute.</param>
    /// <param name="invokeArgs">Optional invocation arguments for caching, progress, and command prepending.</param>
    /// <returns>List of output lists, one per command.</returns>
    public
#if ASYNC
        async Task<List<List<string>>>
#else
    List<List<string>>
#endif
    Invoke(List<string> commands, PsInvokeArgs? invokeArgs = null)
    {
        if (invokeArgs == null)
            invokeArgs = new PsInvokeArgs();
        var isFileExisting = false;
        if (invokeArgs.PathToSaveLoadPsOutput != null && File.Exists(invokeArgs.PathToSaveLoadPsOutput))
        {
            isFileExisting = true;
            var cachedResult = JsonConvert.DeserializeObject<List<string>>(await File.ReadAllTextAsync(invokeArgs.PathToSaveLoadPsOutput));
            var parsedResult = new List<List<string>>(cachedResult!.Count);
            foreach (var item in cachedResult)
                parsedResult.Add(SHGetLines.GetLines(item));
            return parsedResult;
        }

        List<string>? prependCommands = null;
        var isRemovingFirst = false;
        foreach (var item in commands)
        {
            if (item.Trim().StartsWith("cd "))
            {
                if (prependCommands == null)
                    prependCommands = new List<string>();
                isRemovingFirst = true;
                prependCommands.Add(item);
            }

            break;
        }

        if (isRemovingFirst)
            commands.RemoveAt(0);
        if (prependCommands != null)
            invokeArgs.AddBeforeEveryCommand = prependCommands;
        var returnList = new List<List<string>>();
        PowerShell? powerShell = null;

        foreach (var item in commands)
        {
            using (powerShell = PowerShell.Create())
            {
                if (invokeArgs.AddBeforeEveryCommand != null)
                    foreach (var prependCommand in invokeArgs.AddBeforeEveryCommand)
                        powerShell = powerShell.AddScript(prependCommand);
                powerShell = powerShell.AddScript(item).AddCommand("Out-String");
                PSDataCollection<PSObject>? psObjects = null;
                try
                {
#if ASYNC
                    psObjects = await powerShell.InvokeAsync();
#else
                    var asyncResult = powerShell.BeginInvoke();
                    psObjects = powerShell.EndInvoke(asyncResult);
#endif
                }
                catch (Exception exception)
                {
                    throw new Exception(Exceptions.TextOfExceptions(exception));
                }

                PSDataCollection<ErrorRecord> errors = powerShell.Streams.Error;
                if (errors.Count > 0)
                {
                    var stringBuilder = new StringBuilder();
                    foreach (var errorRecord in powerShell.Streams.Error)
                        if (errorRecord != null)
                            ErrorRecordHelper.Text(stringBuilder, errorRecord);
                    returnList.Add(new List<string>([stringBuilder.ToString().ToUnixLineEnding()]));
                }
                else
                {
                    returnList.Add(ProcessPSObjects(psObjects));
                }
            }
        }

        if (invokeArgs.IsImmediatelyWritingToStatus)
            foreach (var item in returnList)
                foreach (var outputLine in item)
                    if (!string.IsNullOrEmpty(outputLine))
                        Console.WriteLine(outputLine);

        if (SaveUsedCommandToDictionary)
            for (var i = 0; i < commands.Count; i++)
            {
                var commandTrimmed = commands[i].Trim();
                if (!commandTrimmed.StartsWith("cd "))
                {
                    DictionaryHelper.AddOrCreate(UsedCommandsInFolders, commandTrimmed, string.Join(Environment.NewLine, returnList[i]));
                    break;
                }
            }

        if (!isFileExisting && invokeArgs.PathToSaveLoadPsOutput != null)
        {
            var sourceList = new List<string>(returnList.Count);
            foreach (var item in returnList)
                sourceList.Add(string.Join(Environment.NewLine, item));
            await File.WriteAllTextAsync(invokeArgs.PathToSaveLoadPsOutput, JsonConvert.SerializeObject(sourceList));
        }

        return returnList;
    }

    /// <summary>
    /// Invokes commands parsed from a multi-line string and returns the combined output as lines.
    /// </summary>
    /// <param name="text">Multi-line string containing commands.</param>
    /// <param name="isWritingProgressBar">Whether to write progress bar updates.</param>
    /// <returns>List of output lines.</returns>
    public
#if ASYNC
        async Task<List<string>>
#else
string
#endif
    InvokeLinesFromString(string text, bool isWritingProgressBar)
    {
        var commandList = SHGetLines.GetLines(text);
        var result =
#if ASYNC
            await
#endif
        Invoke(commandList, new PsInvokeArgs { IsWritingProgressBar = isWritingProgressBar });
        var stringBuilder = new StringBuilder();
        foreach (var item in result)
            stringBuilder.AppendLine(string.Join(Environment.NewLine, item).Trim());
        var combinedOutput = stringBuilder.ToString().Trim();
        return SHGetLines.GetLines(combinedOutput);
    }

    /// <summary>
    /// Invokes a single PowerShell command and returns its output.
    /// </summary>
    /// <param name="command">Command to execute.</param>
    /// <returns>List of output lines from the command.</returns>
    public
#if ASYNC
async Task<List<string>>
#else
    List<string>
#endif
    InvokeSingle(string command)
    {
        return (
#if ASYNC
await
#endif
        Invoke(new List<string>([command])))[0];
    }
}

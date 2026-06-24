namespace SunamoPS;

public partial class PowershellRunner : PsOutput, IPowershellRunner<List<string>>
{
    public static PowershellRunner Instance { get; } = new();

    private bool saveUsedCommandToDictionary;

    private PowershellRunner()
    {
    }

    public ProgressStatePS ProgressState { get; set; } = new();

    public bool SaveUsedCommandToDictionary
    {
        get => saveUsedCommandToDictionary;
        set => saveUsedCommandToDictionary = value;
    }

    public
        async Task<List<List<string>>>
    Invoke(List<string> commands, PsInvokeArgs? invokeArgs = null)
    {
        invokeArgs ??= new PsInvokeArgs();
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
                prependCommands ??= new List<string>();
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
                    psObjects = await powerShell.InvokeAsync();
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

    public
        async Task<List<string>>
    InvokeLinesFromString(string text, bool isWritingProgressBar)
    {
        var commandList = SHGetLines.GetLines(text);
        var result =
            await
        Invoke(commandList, new PsInvokeArgs { IsWritingProgressBar = isWritingProgressBar });
        var stringBuilder = new StringBuilder();
        foreach (var item in result)
            stringBuilder.AppendLine(string.Join(Environment.NewLine, item).Trim());
        var combinedOutput = stringBuilder.ToString().Trim();
        return SHGetLines.GetLines(combinedOutput);
    }

    public
async Task<List<string>>
    InvokeSingle(string command)
    {
        return (
await
        Invoke(new List<string>([command])))[0];
    }
}

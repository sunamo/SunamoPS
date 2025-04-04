namespace SunamoPS;

public partial class PowershellRunner : PsOutput, IPowershellRunner<List<string>>
{
    /// <summary>
    ///     Musím instanci vytvářet tady, ne v ctoru
    /// </summary>
    public static PowershellRunner ci = new();

    private bool saveUsedCommandToDictionary;

    private PowershellRunner()
    {
        // nevím proč to tu je, FS.InvokePs mi EveryLine nenašel. 15-4-23 zakomentováno
        //FS.InvokePs = Invoke;
    }

    public ProgressStatePS clpb { get; set; }

    public bool SaveUsedCommandToDictionary
    {
        get { return saveUsedCommandToDictionary; }
        set
        {
#if DEBUG
            if (value)
            {
            }
#endif


            saveUsedCommandToDictionary = value;
        }
    }


    #region IPowershellRunnerCommands
    /// <summary>
    ///     Tested, working
    ///     For every command return at least one entry in result
    ///     No nevím jestli funguje, dal jsem 4 příkazy a ze všech jsem dostal příkaz z prvního že neexistuje cmd let. možná
    ///     musím bez profilu. profil není v c# podporovaný.
    /// </summary>
    /// <param name="commands"></param>
    public
#if ASYNC
        async Task<List<List<string>>>
#else
List<List<string>>
#endif
        Invoke(List<string> commands, PsInvokeArgs e = null)
    {
        if (e == null) e = new PsInvokeArgs();


        var fileExists = false;
        if (e.pathToSaveLoadPsOutput != null && File.Exists(e.pathToSaveLoadPsOutput))
        {
            // todo zde podmínečně kontrolovat zda DEBUG je přítomen
            fileExists = true;
            var r = JsonConvert.DeserializeObject<List<string>>(await File.ReadAllTextAsync(e.pathToSaveLoadPsOutput));

            var r2 = new List<List<string>>(r.Count);
            foreach (var item in r) r2.Add(SHGetLines.GetLines(item));

            return r2;
        }


        List<string> addBeforeEveryCommand = null;

        var removeFirst = false;
        foreach (var item in commands)
        {
            if (item.Trim().StartsWith("cd "))
            {
                if (addBeforeEveryCommand == null) addBeforeEveryCommand = new List<string>();
                removeFirst = true;
                addBeforeEveryCommand.Add(item);
            }

            break;
        }

        if (removeFirst) commands.RemoveAt(0);

        if (addBeforeEveryCommand != null) e.addBeforeEveryCommand = addBeforeEveryCommand;

        var returnList = new List<List<string>>();
        PowerShell ps = null;
        //  After leaving using is closed pipeline, must watch for complete or

#if DEBUG
        //if (commands[1] == "git status")
        //{

        //}
#endif

        foreach (var item in commands)
        {
#if DEBUG
            //CL.Appeal(item);

            if (item.Contains("git status"))
            {
            }
#endif


            using (ps = PowerShell.Create())
            {
                if (e.addBeforeEveryCommand != null)
                    foreach (var item2 in e.addBeforeEveryCommand)
                        ps = ps.AddScript(item2);

                ps = ps.AddScript(item).AddCommand("Out-String");

                PSDataCollection<PSObject> psObjects = null;

                try
                {
#if ASYNC
                    psObjects = await ps.InvokeAsync();
#else
                    var async_ = ps.BeginInvoke();
                    // Return for SleepWithRandomOutputConsole zero outputs
                    // Pokud se to zasekává, zkontroluj si jestli nenecháváš v app CL.Readline(). S tímto ji powershell nikdy nedokončí
                    psObjects = ps.EndInvoke(async_);
#endif
                }
                catch (Exception ex)
                {
                    throw new Exception(Exceptions.TextOfExceptions(ex));
                    // zde by to mělo vyhodit a podle toho opravit. pak autoservisy
                    //CL.WriteLine(ex);
                    //throw;
                }

                PSDataCollection<ErrorRecord> errors = ps.Streams.Error;
                if (errors.Count > 0)
                {
                    var sb = new StringBuilder();
                    foreach (var item2 in ps.Streams.Error)
                        if (item2 != null)
                            ErrorRecordHelper.Text(sb, item2);
                    returnList.Add(new List<string>([sb.ToString().ToUnixLineEnding()]));
                }
                else
                {
                    returnList.Add(ProcessPSObjects(psObjects));
                }
            }
        }

        //for (int i = 0; i < returnList.Count; i++)
        //{
        //    returnList[i] = CA.Trim(returnList[i]);
        //}

        //result.Wait();
        //var output = result.Result;
        if (e.immediatelyToStatus)
            foreach (var item in returnList)
                foreach (var item2 in item)
                    if (!string.IsNullOrEmpty(item2))
                        Console.WriteLine(item2);
        //ThisApp.Info(item2);
        /*
TOhle je zajímavé
do SaveUsedCommandToDictionary nastavím true, vidím to v debuggeru
        přesto zde je false
        ještě zajímavější je že když jedu na 400+ iteracích, tak tam mám i git commit, git pull atd. má tam být jen git status

        TODO: Proto jsem to dočasně zakomenoval abych se nekoukal na to zda je true
        protože mi to zpomaluje tak t ozakomentuji i tady
         */

        if (SaveUsedCommandToDictionary)
            for (var i = 0; i < commands.Count; i++)
            {
                var commandTrimmed = commands[i].Trim();
                if (!commandTrimmed.StartsWith("cd "))
                {
                    DictionaryHelper.AddOrCreate(UsedCommandsInFolders, commandTrimmed,
                        string.Join(Environment.NewLine, returnList[i]));
                    break;
                }
            }

        if (!fileExists && e.pathToSaveLoadPsOutput != null)
        {
            var ls = new List<string>(returnList.Count);
            foreach (var item in returnList) ls.Add(string.Join(Environment.NewLine, item));

            await File.WriteAllTextAsync(e.pathToSaveLoadPsOutput, JsonConvert.SerializeObject(ls));
        }

        return returnList;
    }
    #endregion



    public
#if ASYNC
        async Task<List<string>>
#else
string
#endif
        InvokeLinesFromString(string v, bool writePb)
    {
        var l = SHGetLines.GetLines(v);


        var result =
#if ASYNC
            await
#endif
                Invoke(l, new PsInvokeArgs { writePb = writePb });

        var sb = new StringBuilder();

        foreach (var item in result) sb.AppendLine(string.Join(Environment.NewLine, item).Trim());

        var result2 = sb.ToString().Trim();

        return SHGetLines.GetLines(result2);
    }

    #region InvokeSingle
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
    #endregion

    /// <summary>
    ///     If in A1 will be full path specified = 'The system cannot find the file specified'
    ///     A1 if dont contains extension, append exe
    /// </summary>
    /// <param name="exeFileNameWithoutPath"></param>
    /// <param name="arguments"></param>
    public
#if ASYNC
        async Task<List<string>>
#else
List<string>
#endif
        InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS a = null)
    {
        if (a == null) a = new InvokeProcessArgsPS();

        // Its not working with .net6 so temporarily disable. Když to nebude fungovat musím vymyslet náhradní řešení.
        //W32.EnableWow64FSRedirection(false);
        if (!exeFileNameWithoutPath.EndsWith(AllExtensions.exe)) exeFileNameWithoutPath += AllExtensions.exe;
        //FS.AddExtensionIfDontHave(exeFileNameWithoutPath, AllExtensions.exe);

        //Create process
        var pProcess = new Process();

        // Must contains only filename, not full path
        pProcess.StartInfo.FileName = exeFileNameWithoutPath;

        //strCommandParameters are parameters to pass to program
        pProcess.StartInfo.Arguments = arguments;

        pProcess.StartInfo.UseShellExecute = false;

        //Set output of program to be written to process output stream
        pProcess.StartInfo.RedirectStandardOutput = true;

        //Optional, recommended do not enter, then old value is not deleted and both paths is combined
        if (a.workingDir != null) pProcess.StartInfo.WorkingDirectory = a.workingDir;

        //Start the process
        pProcess.Start();
        // Its not working with .net6 so temporarily disable. Když to nebude fungovat musím vymyslet náhradní řešení.
        //W32.EnableWow64FSRedirection(true);

        //Get program output
        var strOutput = pProcess.StandardOutput.ReadToEnd();

#if ASYNC
        //Wait for process to finish
        await pProcess.WaitForExitAsync();
#else
        //Wait for process to finish
        pProcess.WaitForExit();
#endif

        var result = SHGetLines.GetLines(strOutput);
        return result;
    }

    /// <summary>
    ///     Je to inteligentní - výstup cd příkazu nedává takže na [0] je výstup prvního příkazu
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<List<string>> InvokeInFolder(string folder, string command)
    {
        List<string> cmds = new(2);
        cmds.Add("cd " + folder);
        cmds.Add(command);

        var output = await Invoke(cmds);

        return SHGetLines.GetLinesFromLinesWithOneRow(output[0]);
    }

    public Dictionary<string, List<string>> UsedCommandsInFolders { get; set; } = new();
}
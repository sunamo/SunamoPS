namespace SunamoPS;

public partial class PowershellRunner : PowershellRunnerBase, IPowershellRunner
{
    public ProgressState clpb { get; set; }
    bool saveUsedCommandToDictionary = false;

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

    /// <summary>
    /// Musím instanci vytvářet tady, ne v ctoru
    /// </summary>
    public static PowershellRunner ci = new PowershellRunner();

    private PowershellRunner()
    {
        // nevím proč to tu je, FS.InvokePs mi EveryLine nenašel. 15-4-23 zakomentováno
        //FS.InvokePs = Invoke;
    }


    /// <summary>
    /// Tested, working
    /// For every command return at least one entry in result
    ///
    /// No nevím jestli funguje, dal jsem 4 příkazy a ze všech jsem dostal příkaz z prvního že neexistuje cmd let. možná musím bez profilu. profil není v c# podporovaný.
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
        if (e == null)
        {
            e = new PsInvokeArgs();
        }


        bool fileExists = false;
        if (e.pathToSaveLoadPsOutput != null && File.Exists(e.pathToSaveLoadPsOutput))
        {
            // todo zde podmínečně kontrolovat zda DEBUG je přítomen
            fileExists = true;
            var r = JsonConvert.DeserializeObject<List<string>>(await TF.ReadAllText(e.pathToSaveLoadPsOutput));

            List<List<string>> r2 = new List<List<string>>(r.Count);
            foreach (var item in r)
            {
                r2.Add(SHGetLines.GetLines(item));
            }

            return r2;
        }


        List<string> addBeforeEveryCommand = null;

        bool removeFirst = false;
        foreach (var item in commands)
        {
            if (item.Trim().StartsWith("cd "))
            {
                if (addBeforeEveryCommand == null)
                {
                    addBeforeEveryCommand = new List<string>();
                }
                removeFirst = true;
                addBeforeEveryCommand.Add(item);
            }
            break;
        }

        if (removeFirst)
        {
            commands.RemoveAt(0);
        }

        if (addBeforeEveryCommand != null)
        {
            e.addBeforeEveryCommand = addBeforeEveryCommand;
        }

        List<List<string>> returnList = new List<List<string>>();
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
                {
                    foreach (var item2 in e.addBeforeEveryCommand)
                    {
                        ps = ps.AddScript(item2);
                    }
                }

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
                    ThrowEx.Custom(ex);
                    // zde by to mělo vyhodit a podle toho opravit. pak autoservisy
                    //CL.WriteLine(ex);
                    //throw;
                }

                if (ps.Streams.Error.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item2 in ps.Streams.Error)
                    {
                        if (item2 != null)
                        {
                            ErrorRecordHelper.Text(sb, item2);
                        }
                    }
                    returnList.Add(CAG.ToList<string>(sb.ToString().ToUnixLineEnding()));
                }
                else
                {
                    returnList.Add(ProcessPSObjects(psObjects));
                }
            }
        }

        for (int i = 0; i < returnList.Count; i++)
        {
            returnList[i] = CA.Trim(returnList[i]);
        }

        //result.Wait();
        //var output = result.Result;
        if (e.immediatelyToStatus)
        {
            foreach (var item in returnList)
            {
                foreach (var item2 in item)
                {
                    if (!string.IsNullOrEmpty(item2))
                    {
                        ThisApp.Info(item2);
                    }
                }
            }
        }

        /*
TOhle je zajímavé
do SaveUsedCommandToDictionary nastavím true, vidím to v debuggeru
        přesto zde je false
        ještě zajímavější je že když jedu na 400+ iteracích, tak tam mám i git commit, git pull atd. má tam být jen git status

        TODO: Proto jsem to dočasně zakomenoval abych se nekoukal na to zda je true
        protože mi to zpomaluje tak t ozakomentuji i tady
         */

        if (SaveUsedCommandToDictionary)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                var commandTrimmed = commands[i].Trim();
                if (!commandTrimmed.StartsWith("cd "))
                {
                    DictionaryHelper.AddOrCreate(UsedCommandsInFolders, commandTrimmed, SHJoin.JoinNL(returnList[i]));
                    break;
                }
            }
        }

        if (!fileExists && e.pathToSaveLoadPsOutput != null)
        {
            List<string> ls = new List<string>(returnList.Count);
            foreach (var item in returnList)
            {
                ls.Add(SHJoin.JoinNL(item));
            }

            await TF.WriteAllText(e.pathToSaveLoadPsOutput, JsonConvert.SerializeObject(ls));
        }

        return returnList;

    }

    public
#if ASYNC
async Task<string>
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

        StringBuilder sb = new StringBuilder();

        foreach (var item in result)
        {
            sb.AppendLine(SHJoin.JoinNL(item).Trim());
        }

        var result2 = sb.ToString().Trim();

        return result2;
    }

    /// <summary>
    /// If in A1 will be full path specified = 'The system cannot find the file specified'
    /// A1 if dont contains extension, append exe
    /// </summary>
    /// <param name="exeFileNameWithoutPath"></param>
    /// <param name="arguments"></param>
    public
#if ASYNC
async Task<List<string>>
#else
List<string>
#endif
InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgs a = null)
    {
        if (a == null)
        {
            a = new InvokeProcessArgs();
        }

        // Its not working with .net6 so temporarily disable. Když to nebude fungovat musím vymyslet náhradní řešení.
        //W32.EnableWow64FSRedirection(false);
        FS.AddExtensionIfDontHave(exeFileNameWithoutPath, AllExtensions.exe);

        //Create process
        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

        // Must contains only filename, not full path
        pProcess.StartInfo.FileName = exeFileNameWithoutPath;

        //strCommandParameters are parameters to pass to program
        pProcess.StartInfo.Arguments = arguments;

        pProcess.StartInfo.UseShellExecute = false;

        //Set output of program to be written to process output stream
        pProcess.StartInfo.RedirectStandardOutput = true;

        //Optional, recommended do not enter, then old value is not deleted and both paths is combined
        if (a.workingDir != null)
        {
            pProcess.StartInfo.WorkingDirectory = a.workingDir;
        }

        //Start the process
        pProcess.Start();
        // Its not working with .net6 so temporarily disable. Když to nebude fungovat musím vymyslet náhradní řešení.
        //W32.EnableWow64FSRedirection(true);

        //Get program output
        string strOutput = pProcess.StandardOutput.ReadToEnd();

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

    public async Task<List<string>> InvokeInFolder(string folder, string command)
    {
        List<string> cmds = new(2);
        cmds.Add("cd " + folder);
        cmds.Add(command);

        return (await Invoke(cmds))[1];
    }

    public Dictionary<string, List<string>> UsedCommandsInFolders { get; set; } = new Dictionary<string, List<string>>();
}

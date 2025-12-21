namespace SunamoPS;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class PowershellRunner : PsOutput, IPowershellRunner<List<string>>
{
    /// <summary>
    ///     If in A1 will be full path specified = 'The system cannot find the file specified'
    ///     A1 if dont contains extension, append exe
    /// </summary>
    /// <param name = "exeFileNameWithoutPath"></param>
    /// <param name = "arguments"></param>
    public 
#if ASYNC
        async Task<List<string>>
#else
    List<string> 
#endif
    InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS a = null)
    {
        if (a == null)
            a = new InvokeProcessArgsPS();
        // Its not working with .net6 so temporarily disable. Když to nebude fungovat musím vymyslet náhradní řešení.
        //W32.EnableWow64FSRedirection(false);
        if (!exeFileNameWithoutPath.EndsWith(AllExtensions.exe))
            exeFileNameWithoutPath += AllExtensions.exe;
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
        if (a.workingDir != null)
            pProcess.StartInfo.WorkingDirectory = a.workingDir;
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
    /// <param name = "folder"></param>
    /// <param name = "command"></param>
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
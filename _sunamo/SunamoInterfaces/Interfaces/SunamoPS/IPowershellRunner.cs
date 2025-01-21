namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

//internal interface IPowershellRunner
//{
//    ProgressState clpb { get; set; }
//#if ASYNC
//    Task<List<string>> InvokeProcess(string exeFileNameWithoutPath, string arguments);
//    Task<List<List<string>>> Invoke(IList<string> commands);
//    Task<List<List<string>>> Invoke(IList<string> commands, PsInvokeArgs e);
//    Task<List<string>> Invoke(string commands);
//    Task<List<List<string>>> InvokeAsync(IList<string> commands, PsInvokeArgs e = null);
//    Task<string> InvokeLinesFromString(string v, bool writePb);
//    Task<List<string>> InvokeSingle(string command);
//#else
//List<string> InvokeProcess(string exeFileNameWithoutPath, string arguments);
//List<List<string>> Invoke(IList<string> commands);
//    List<List<string>> Invoke(IList<string> commands, PsInvokeArgs e);
//    List<string> Invoke(string commands);
//    Task<List<List<string>>> InvokeAsync(IList<string> commands, PsInvokeArgs e = null);
//    string InvokeLinesFromString(string v, bool writePb);
//     List<string> InvokeSingle(string command);
//#endif
//    Dictionary<string, List<string>> UsedCommandsInFolders { get; set; }
//    //List<string> ProcessPSObjects(ICollection<PSObject> pso);
//}
/// <summary>
///     Invoke - more commands, just run InvokeWorker
///     InvokeLinesFromString - more commands, with progress bar. Simply call InvokeWorker
///     InvokeProcess - spustí proces ze kterého vrátí output
///     InvokeSingle - just run InvokeWorker
/// </summary>
internal interface IPowershellRunner<T>
{
    Task<T> InvokeInFolder(string folder, string command);
    Task<T> InvokeSingle(string command);
    //List<List<string>> Invoke(IList<string> commands, PsInvokeArgs e);
    //List<string> Invoke(string commands);
    // zakomentoval jsem protože všechny 4 invoke pouze volají InvokeWorker
    Task<T> InvokeLinesFromString(string v, bool writePb);
    Task<T> InvokeProcess(string exeFileNameWithoutPath, string arguments, InvokeProcessArgsPS a = null);


}
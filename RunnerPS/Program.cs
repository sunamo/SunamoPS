namespace RunnerPS;

using SunamoDebugIO;
using SunamoPlatformUwpInterop.AppData;
using SunamoPS;

internal class Program : ProgramShared
{
    const string appName = "RunnerPS";

    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async Task MainAsync()
    {
        AppData.ci.CreateAppFoldersIfDontExists(new SunamoPlatformUwpInterop.Args.CreateAppFoldersIfDontExistsArgs { AppName = appName });
        await ProgramShared.CreatePathToFiles(AppData.ci.GetFileString);

        PowershellRunnerTests t = new PowershellRunnerTests();
        await t.InvokeInFolderTest();

        //await a();

        //Console.WriteLine("Finished");
        Console.ReadLine();
    }

    private static async Task a()
    {
        var methods = PowershellHelper.ParseMethods(await File.ReadAllTextAsync(@"e:\vs\Scripts_Projects\PowershellScripts\_PowerShell\Microsoft.PowerShell_profile.ps1"));

        Output = PowershellHelper.FindDuplicatedMethodsInPs1File(methods);
        OutputOpen();
    }
}
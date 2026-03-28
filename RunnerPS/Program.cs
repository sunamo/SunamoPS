namespace RunnerPS;

using SunamoDebugIO;
using SunamoPlatformUwpInterop.AppData;
using SunamoPS;
using SunamoPS.Tests;

/// <summary>
/// Entry point for the RunnerPS console application.
/// </summary>
internal class Program : ProgramShared
{
    const string appName = "RunnerPS";

    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async Task MainAsync()
    {
        AppData.Instance.CreateAppFoldersIfDontExists(new SunamoPlatformUwpInterop.Args.CreateAppFoldersIfDontExistsArgs { AppName = appName });
        await ProgramShared.CreatePathToFiles(AppData.Instance.GetFileString);

        PowershellRunnerTests t = new PowershellRunnerTests();
        await t.InvokeInFolderTest();

        Console.ReadLine();
    }
}

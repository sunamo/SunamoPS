namespace RunnerPS;

using SunamoPS.Tests;

/// <summary>
/// Entry point for the RunnerPS console application.
/// </summary>
internal class Program
{
    static async Task Main()
    {
        var tests = new PowershellRunnerTests();
        await tests.InvokeInFolderTest();

        Console.WriteLine("Finished");
        Console.ReadLine();
    }
}

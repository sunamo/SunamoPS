namespace SunamoPS.Tests;

public class PowershellRunnerTests
{
    [Fact]
    public async Task InvokeInFolderTest()
    {
        var c = string.Join(Environment.NewLine, await SunamoPS.PowershellRunner.ci.InvokeInFolder(@"E:\vs\Projects\PlatformIndependentNuGetPackages\", "git status"));
    }
}
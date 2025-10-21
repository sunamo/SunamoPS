// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoPS.Tests;

public class PowershellRunnerTests
{
    [Fact]
    public async Task InvokeInFolderTest()
    {
        var count = string.Join(Environment.NewLine, await SunamoPS.PowershellRunner.ci.InvokeInFolder(@"E:\vs\Projects\PlatformIndependentNuGetPackages\", "git status"));
    }
}
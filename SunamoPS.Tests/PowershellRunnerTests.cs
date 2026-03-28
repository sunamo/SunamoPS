namespace SunamoPS.Tests;

/// <summary>
/// Tests for PowershellRunner functionality.
/// </summary>
public class PowershellRunnerTests
{
    /// <summary>
    /// Tests that InvokeInFolder correctly executes git status in a specified folder.
    /// </summary>
    [Fact]
    public async Task InvokeInFolderTest()
    {
        var gitStatusOutput = await PowershellRunner.Instance.InvokeInFolder(
            Path.GetTempPath(),
            "Get-ChildItem");

        Assert.NotNull(gitStatusOutput);
        Assert.True(gitStatusOutput.Count > 0, "InvokeInFolder should return at least one line of output");
    }

    /// <summary>
    /// Tests that InvokeSingle correctly executes a single PowerShell command.
    /// </summary>
    [Fact]
    public async Task InvokeSingleTest()
    {
        var output = await PowershellRunner.Instance.InvokeSingle("Write-Output 'Hello'");

        Assert.NotNull(output);
        Assert.Contains(output, line => line.Contains("Hello"));
    }

    /// <summary>
    /// Tests that InvokeProcess correctly runs an external process.
    /// </summary>
    [Fact]
    public async Task InvokeProcessTest()
    {
        var output = await PowershellRunner.Instance.InvokeProcess("cmd", "/c echo TestOutput");

        Assert.NotNull(output);
        Assert.True(output.Count > 0, "InvokeProcess should return output from the process");
    }

    /// <summary>
    /// Tests that PowershellParser correctly parses command parts.
    /// </summary>
    [Fact]
    public void ParseToPartsTest()
    {
        var parts = PowershellParser.Instance.ParseToParts("Get-Process -Name \"My Process\"", "|");

        Assert.NotNull(parts);
        Assert.Equal(3, parts.Count);
        Assert.Equal("Get-Process", parts[0]);
        Assert.Equal("-Name", parts[1]);
        Assert.Equal("\"My Process\"", parts[2]);
    }

    /// <summary>
    /// Tests that PowershellHelper can retrieve running process names.
    /// </summary>
    [Fact]
    public void ProcessNamesTest()
    {
        var processNames = PowershellHelper.Instance.ProcessNames();

        Assert.NotNull(processNames);
        Assert.True(processNames.Count > 0, "There should be at least one running process");
    }
}

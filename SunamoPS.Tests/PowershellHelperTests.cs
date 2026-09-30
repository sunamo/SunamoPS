namespace SunamoPS.Tests;

/// <summary>
/// Tests for PowershellHelper (moved from sunamo.Tests.wpf win.Tests).
/// </summary>
public class PowershellHelperTests
{
    /// <summary>
    /// Verifies GitHub Linguist language detection on files from the local test data folder.
    /// </summary>
    [Fact(Skip = "Requires WSL with github-linguist and local test data in D:/_Test/sunamo")]
    public async Task DetectLanguageForFileGithubLinguistTest()
    {
        string file = @"D:\_Test\sunamo\win\Helpers\Powershell\PowershellHelper\cs";
        var expected = "C#";

        var actual = await PowershellHelper.Instance.DetectLanguageForFileGithubLinguist(file);
        Assert.Equal(expected, actual);

        file = @"D:\_Test\sunamo\win\Helpers\Powershell\PowershellHelper\plain";
        expected = "C#";

        actual = await PowershellHelper.Instance.DetectLanguageForFileGithubLinguist(file);
        Assert.Equal(expected, actual);
    }
}

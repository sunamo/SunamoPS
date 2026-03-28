namespace SunamoPS;

/// <summary>
/// Parses PowerShell command strings into individual parts, respecting quoted sections.
/// </summary>
public class PowershellParser : IPowershellParser
{
    /// <summary>
    /// Singleton instance of PowershellParser.
    /// </summary>
    public static PowershellParser Instance { get; } = new();

    private PowershellParser()
    {
    }

    /// <summary>
    /// Parses a command string into individual parts. Spaces inside quoted sections are preserved.
    /// </summary>
    /// <param name="text">Command string to parse.</param>
    /// <param name="charWhichIsNotContained">Temporary placeholder character that must not appear in the input.</param>
    /// <returns>List of parsed command parts.</returns>
    public List<string> ParseToParts(string text, string charWhichIsNotContained)
    {
        if (text.Contains(charWhichIsNotContained)) throw new Exception(text + " contains " + charWhichIsNotContained);

        var stringBuilder = new StringBuilder(text);
        var quotedMatches = Regex.Matches(text, "\"([^\"]*)\"").Select(match => match.Value);
        foreach (var item in quotedMatches) stringBuilder = stringBuilder.Replace(item, item.Replace(" ", charWhichIsNotContained));

        var parts = SHSplit.Split(stringBuilder.ToString(), " ");
        for (var i = 0; i < parts.Count; i++) parts[i] = parts[i].Replace(charWhichIsNotContained, "");

        return parts;
    }
}

namespace SunamoPS;

public class PowershellParser : IPowershellParser
{
    public static PowershellParser Instance { get; } = new();

    private PowershellParser()
    {
    }

    public List<string> ParseToParts(string text, string charWhichIsNotContained)
    {
        if (text.Contains(charWhichIsNotContained)) throw new Exception(text + " contains " + charWhichIsNotContained);

        var stringBuilder = new StringBuilder(text);
        var quotedMatches = Regex.Matches(text, "\"([^\"]*)\"").Cast<Match>().Select(match => match.Value);
        foreach (var item in quotedMatches) stringBuilder = stringBuilder.Replace(item, item.Replace(" ", charWhichIsNotContained));

        var parts = SHSplit.Split(stringBuilder.ToString(), " ");
        for (var i = 0; i < parts.Count; i++) parts[i] = parts[i].Replace(charWhichIsNotContained, "");

        return parts;
    }
}

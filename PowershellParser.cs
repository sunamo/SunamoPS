namespace SunamoPS;

public class PowershellParser : IPowershellParser
{
    public static PowershellParser ci = new();
    private Type type = typeof(PowershellParser);

    private PowershellParser()
    {
    }

    public List<string> ParseToParts(string d, string charWhichIsNotContained)
    {
        if (d.Contains(charWhichIsNotContained)) throw new Exception(d + " contains " + charWhichIsNotContained);

        var sb = new StringBuilder(d);
        var b = Regex.Matches(d, "\"([^\"]*)\"").Select(d => d.Value); //SH.ValuesBetweenQuotes(d, true);
        foreach (var item in b) sb = sb.Replace(item, item.Replace(AllStrings.space, charWhichIsNotContained));

        var p = SHSplit.SplitMore(sb.ToString(), AllStrings.space);
        for (var i = 0; i < p.Count; i++) p[i] = p[i].Replace(charWhichIsNotContained, AllStrings.space);

        return p;
    }
}
namespace SunamoPS;





public class PowershellParser : IPowershellParser
{
    Type type = typeof(PowershellParser);

    public static PowershellParser ci = new PowershellParser();

    private PowershellParser()
    {

    }

    public List<string> ParseToParts(string d, string charWhichIsNotContained)
    {
        if (d.Contains(charWhichIsNotContained))
        {
            throw new Exception(d + " contains " + charWhichIsNotContained);
        }

        StringBuilder sb = new StringBuilder(d);
        var b = Regex.Matches(d, "\"([^\"]*)\"").Select(d => d.Value); //SH.ValuesBetweenQuotes(d, true);
        foreach (var item in b)
        {
            sb = sb.Replace(item, item.Replace(AllStrings.space, charWhichIsNotContained));
        }

        var p = SHSplit.Split(sb.ToString(), AllStrings.space);
        for (int i = 0; i < p.Count; i++)
        {
            p[i] = p[i].Replace(charWhichIsNotContained, AllStrings.space);
        }

        return p;
    }
}

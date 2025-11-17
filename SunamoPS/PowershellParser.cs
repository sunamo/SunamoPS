// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

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

        var stringBuilder = new StringBuilder(d);
        var builder = Regex.Matches(d, "\"([^\"]*)\"").Select(d => d.Value); //SH.ValuesBetweenQuotes(d, true);
        foreach (var item in builder) stringBuilder = stringBuilder.Replace(item, item.Replace(" ", charWhichIsNotContained));

        var parameter = SHSplit.Split(stringBuilder.ToString(), " ");
        for (var i = 0; i < parameter.Count; i++) parameter[i] = parameter[i].Replace(charWhichIsNotContained, "");

        return parameter;
    }
}
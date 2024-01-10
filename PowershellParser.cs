using SunamoExceptions.OnlyInSE;
using SunamoInterfaces.Interfaces.SunamoPS;
using SunamoString;
using SunamoStringSplit;
using SunamoValues;

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
            ThrowEx.Custom(d + " contains " + charWhichIsNotContained);
        }

        StringBuilder sb = new StringBuilder(d);
        var b = SH.ValuesBetweenQuotes(d, true);
        foreach (var item in b)
        {
            sb = sb.Replace(item, item.Replace(AllStringsSE.space, charWhichIsNotContained));
        }

        var p = SHSplit.Split(sb.ToString(), AllStringsSE.space);
        for (int i = 0; i < p.Count; i++)
        {
            p[i] = p[i].Replace(charWhichIsNotContained, AllStringsSE.space);
        }

        return p;
    }
}

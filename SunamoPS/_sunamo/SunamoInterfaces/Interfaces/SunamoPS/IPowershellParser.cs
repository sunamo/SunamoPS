namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

internal interface IPowershellParser
{
    List<string> ParseToParts(string text, string charWhichIsNotContained);
}

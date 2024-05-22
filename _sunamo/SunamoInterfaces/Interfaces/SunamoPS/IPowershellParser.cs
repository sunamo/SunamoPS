namespace SunamoPS;


internal interface IPowershellParser
{
    List<string> ParseToParts(string d, string charWhichIsNotContained);
}
namespace SunamoPS;


public interface IPowershellParser
{
    List<string> ParseToParts(string d, string charWhichIsNotContained);
}
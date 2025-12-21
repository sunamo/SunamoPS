namespace SunamoPS._sunamo.SunamoExtensions;

internal static class ToUnixLineEndingExtensions
{
}

internal static class StringExtensions
{
    internal static string ToUnixLineEnding(this string s)
    {
        return s.ReplaceLineEndings("\n");
    }
}
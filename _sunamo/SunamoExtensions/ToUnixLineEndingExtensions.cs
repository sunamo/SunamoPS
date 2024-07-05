namespace SunamoPS._sunamo.SunamoExtensions;

internal static class ToUnixLineEndingExtensions
{
    internal static IList<string> ToUnixLineEnding(this IList<string> t)
    {
        for (int i = 0; i < t.Count; i++)
        {
            t[i] = t[i].ToUnixLineEnding();
        }
        return t;
    }
}
internal static class StringExtensions
{
    internal static string ToUnixLineEnding(this string s)
    {
        return s.ReplaceLineEndings("\n");
    }
}

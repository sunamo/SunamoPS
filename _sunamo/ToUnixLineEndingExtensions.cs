
namespace SunamoPS;

internal static partial class ToUnixLineEndingExtensions
{
    public static IList<string> ToUnixLineEnding(this IList<string> t)
    {
        for (int i = 0; i < t.Count; i++)
        {
            t[i] = t[i].ToUnixLineEnding();
        }
        return t;
    }
}
internal static partial class StringExtensions
{
    public static string ToUnixLineEnding(this string s)
    {
        return s.ReplaceLineEndings("\n");
    }
}
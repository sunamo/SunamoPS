namespace SunamoPS._sunamo.SunamoStringGetLines;

/// <summary>
/// Splits text into lines.
/// </summary>
internal static class SHGetLines
{
    /// <summary>
    /// Splits the text into lines, handling CRLF, LFCR, CR and LF.
    /// </summary>
    internal static List<string> GetLines(string text)
    {
        return text.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None).ToList();
    }

    /// <summary>
    /// Splits the only item of the list into lines; a list with more items is returned unchanged.
    /// </summary>
    internal static List<string> GetLinesFromLinesWithOneRow(List<string> list)
    {
        return list.Count == 1 ? GetLines(list[0]) : list;
    }
}

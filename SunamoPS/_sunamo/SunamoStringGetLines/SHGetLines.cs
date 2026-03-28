namespace SunamoPS._sunamo.SunamoStringGetLines;

/// <summary>
/// Helper for splitting text into individual lines.
/// </summary>
internal class SHGetLines
{
    /// <summary>
    /// Splits text into lines, handling all newline formats.
    /// </summary>
    /// <param name="text">Text to split into lines.</param>
    /// <returns>List of lines.</returns>
    internal static List<string> GetLines(string text)
    {
        var parts = text.Split(new[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(parts);
        return parts;
    }

    private static void SplitByUnixNewline(List<string> list)
    {
        SplitBy(list, "\r");
        SplitBy(list, "\n");
    }

    private static void SplitBy(List<string> list, string delimiter)
    {
        for (var i = list.Count - 1; i >= 0; i--)
        {
            if (delimiter == "\r")
            {
                var carriageReturnNewline = list[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                var newlineCarriageReturn = list[i].Split(new[] { "\n\r" }, StringSplitOptions.None);

                if (carriageReturnNewline.Length > 1)
                    ThrowEx.Custom("cannot contain any \\r\\n, pass already split by this pattern");
                else if (newlineCarriageReturn.Length > 1)
                    ThrowEx.Custom("cannot contain any \\n\\r, pass already split by this pattern");
            }

            var splitResult = list[i].Split(new[] { delimiter }, StringSplitOptions.None);

            if (splitResult.Length > 1) InsertOnIndex(list, splitResult.ToList(), i);
        }
    }

    private static void InsertOnIndex(List<string> list, List<string> insertList, int index)
    {
        insertList.Reverse();

        list.RemoveAt(index);

        foreach (var item in insertList) list.Insert(index, item);
    }

    /// <summary>
    /// Gets lines from a list that may contain a single multi-line entry.
    /// </summary>
    /// <param name="list">List that may contain a single string with embedded newlines.</param>
    /// <returns>List of individual lines.</returns>
    internal static List<string> GetLinesFromLinesWithOneRow(List<string> list)
    {
        if (list.Count == 1) return GetLines(list[0]);
        return list;
    }
}

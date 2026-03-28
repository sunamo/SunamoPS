namespace SunamoPS._sunamo.SunamoStringSplit;

/// <summary>
/// String splitting helper methods.
/// </summary>
internal class SHSplit
{
    /// <summary>
    /// Splits a string by the specified delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="delimiters">Delimiters to split by.</param>
    /// <returns>List of non-empty parts.</returns>
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}

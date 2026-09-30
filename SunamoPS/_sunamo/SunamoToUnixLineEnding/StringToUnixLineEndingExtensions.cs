namespace SunamoPS._sunamo.SunamoToUnixLineEnding;

/// <summary>
/// Internal copy of the string line-ending normalization extension (converts \r\n and \r to \n).
/// </summary>
internal static class StringToUnixLineEndingExtensions
{
    /// <summary>
    /// Converts all Windows and old Mac line endings in the text to Unix line endings.
    /// </summary>
    /// <param name="text">Text to normalize.</param>
    /// <returns>Text with \n line endings only.</returns>
    internal static string ToUnixLineEnding(this string text)
    {
        return text.Replace("\r\n", "\n").Replace("\r", "\n");
    }
}

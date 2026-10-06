namespace SunamoPS._sunamo;

internal class SHJoin
{
    /// <summary>
    /// Joins a list of strings with newline separators.
    /// </summary>
    /// <param name="list">List of strings to join.</param>
    /// <returns>Joined string.</returns>
    internal static string JoinNL(List<string> list) => string.Join('\n', list);
}

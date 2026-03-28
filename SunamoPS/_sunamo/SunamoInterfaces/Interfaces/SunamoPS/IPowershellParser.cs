namespace SunamoPS._sunamo.SunamoInterfaces.Interfaces.SunamoPS;

/// <summary>
/// Interface for parsing PowerShell command strings.
/// </summary>
internal interface IPowershellParser
{
    /// <summary>
    /// Parses a command string into individual parts, respecting quoted sections.
    /// </summary>
    /// <param name="text">Command string to parse.</param>
    /// <param name="charWhichIsNotContained">Temporary placeholder character for spaces inside quotes.</param>
    /// <returns>List of parsed command parts.</returns>
    List<string> ParseToParts(string text, string charWhichIsNotContained);
}

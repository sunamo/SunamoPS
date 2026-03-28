namespace SunamoPS;

/// <summary>
/// Helper for formatting PowerShell ErrorRecord objects into readable text.
/// </summary>
public class ErrorRecordHelper
{
    /// <summary>
    /// Appends the error details and exception messages from an ErrorRecord to the StringBuilder.
    /// </summary>
    /// <param name="stringBuilder">StringBuilder to append error text to.</param>
    /// <param name="errorRecord">ErrorRecord to extract messages from.</param>
    public static void Text(StringBuilder stringBuilder, ErrorRecord errorRecord)
    {
        if (errorRecord == null) return;

        if (errorRecord.ErrorDetails != null) stringBuilder.AppendLine(errorRecord.ErrorDetails.Message);

        stringBuilder.AppendLine(Exceptions.TextOfExceptions(errorRecord.Exception));
    }
}

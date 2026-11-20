namespace SunamoPS;

/// <summary>
/// Represents the error record helper type.
/// </summary>
public class ErrorRecordHelper
{
    /// <summary>
    /// Text.
    /// </summary>
    /// <param name="stringBuilder">The string builder.</param>
    /// <param name="errorRecord">The error record.</param>
    public static void Text(StringBuilder stringBuilder, ErrorRecord errorRecord)
    {
        if (errorRecord == null) return;

        if (errorRecord.ErrorDetails != null) stringBuilder.AppendLine(errorRecord.ErrorDetails.Message);

        stringBuilder.AppendLine(Exceptions.TextOfExceptions(errorRecord.Exception));
    }
}

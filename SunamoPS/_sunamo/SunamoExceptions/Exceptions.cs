namespace SunamoPS._sunamo.SunamoExceptions;

/// <summary>
/// Builds exception messages.
/// </summary>
internal static class Exceptions
{
    /// <summary>
    /// Returns the message of the exception and its inner exceptions, one per line.
    /// </summary>
    internal static string TextOfExceptions(Exception exception)
    {
        if (exception == null) return string.Empty;
        var text = new StringBuilder("Exception:");
        text.AppendLine(exception.Message);
        while (exception.InnerException != null)
        {
            exception = exception.InnerException;
            text.AppendLine(exception.Message);
        }
        return text.ToString();
    }
}

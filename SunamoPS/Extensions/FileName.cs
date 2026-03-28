namespace SunamoPS.Extensions;

/// <summary>
/// Extension methods for Exception objects.
/// </summary>
public static class ExceptionsExtensions
{
    /// <summary>
    /// Gets the exception message including all inner exception messages.
    /// </summary>
    /// <param name="exception">Exception to extract messages from.</param>
    /// <returns>Combined message string from the exception and all inner exceptions.</returns>
    public static string GetAllMessages(this Exception exception)
    {
        if (exception == null)
        {
            return "";
        }

        string message = exception.Message;

        if (exception.InnerException != null)
        {
            message += Environment.NewLine + "Inner Exception: " + exception.InnerException.GetAllMessages();
        }

        return message;
    }
}

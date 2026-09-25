namespace SunamoPS.Extensions;

public static class ExceptionsExtensions
{
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

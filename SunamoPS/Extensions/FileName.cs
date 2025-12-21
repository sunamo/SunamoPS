namespace SunamoPS.Extensions;

public static class ExceptionsExtensions
{
    public static string GetAllMessages(this Exception ex)
    {
        if (ex == null)
        {
            return "";
        }

        string message = ex.Message;

        if (ex.InnerException != null)
        {
            message += Environment.NewLine + "Inner Exception: " + ex.InnerException.GetAllMessages();
        }

        return message;
    }
}
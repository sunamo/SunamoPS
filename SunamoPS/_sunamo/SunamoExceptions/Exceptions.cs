namespace SunamoPS._sunamo.SunamoExceptions;

/// <summary>
/// Utility class for exception message formatting and stack trace inspection.
/// </summary>
internal sealed partial class Exceptions
{
    /// <summary>
    /// Prepends a context label if not empty.
    /// </summary>
    /// <param name="before">Context label to prepend.</param>
    /// <returns>Formatted prefix string.</returns>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Builds a complete error message from an exception and its inner exceptions.
    /// </summary>
    /// <param name="exception">Exception to extract messages from.</param>
    /// <param name="isIncludingInner">Whether to include inner exception messages.</param>
    /// <returns>Formatted exception text.</returns>
    internal static string TextOfExceptions(Exception exception, bool isIncludingInner = true)
    {
        if (exception == null) return string.Empty;
        StringBuilder stringBuilder = new();
        stringBuilder.Append("Exception:");
        stringBuilder.AppendLine(exception.Message);
        if (isIncludingInner)
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                stringBuilder.AppendLine(exception.Message);
            }
        var result = stringBuilder.ToString();
        return result;
    }

    /// <summary>
    /// Inspects the stack trace to determine the type and method where the exception occurred.
    /// </summary>
    /// <param name="isFillAlsoFirstTwo">Whether to fill type and method name from the first non-ThrowEx frame.</param>
    /// <returns>Tuple of (type name, method name, stack trace text).</returns>
    internal static Tuple<string, string, string> PlaceOfException(bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        string typeName = string.Empty;
        string methodName = string.Empty;
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (isFillAlsoFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out typeName, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(typeName, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Extracts type name and method name from a stack trace line.
    /// </summary>
    /// <param name="stackTraceLine">Single line from a stack trace.</param>
    /// <param name="typeName">Extracted type name.</param>
    /// <param name="methodName">Extracted method name.</param>
    internal static void TypeAndMethodName(string stackTraceLine, out string typeName, out string methodName)
    {
        var afterAt = stackTraceLine.Split("at ")[1].Trim();
        var beforeParenthesis = afterAt.Split('(')[0];
        var segments = beforeParenthesis.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = segments[^1];
        segments.RemoveAt(segments.Count - 1);
        typeName = string.Join(".", segments);
    }

    /// <summary>
    /// Gets the name of the calling method at the specified stack depth.
    /// </summary>
    /// <param name="depth">Stack frame depth to inspect.</param>
    /// <returns>Method name at the specified depth.</returns>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }

    /// <summary>
    /// Returns a formatted "not allowed" message.
    /// </summary>
    /// <param name="before">Context label.</param>
    /// <param name="what">What is not allowed.</param>
    /// <returns>Formatted message or null.</returns>
    internal static string? IsNotAllowed(string before, string what)
    {
        return CheckBefore(before) + what + " is not allowed.";
    }

    /// <summary>
    /// Returns a formatted custom error message.
    /// </summary>
    /// <param name="before">Context label.</param>
    /// <param name="message">Custom error message.</param>
    /// <returns>Formatted message or null.</returns>
    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }
}

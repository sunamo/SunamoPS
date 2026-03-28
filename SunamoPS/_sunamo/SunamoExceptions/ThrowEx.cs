namespace SunamoPS._sunamo.SunamoExceptions;

using Debugger = System.Diagnostics.Debugger;

/// <summary>
/// Helper for throwing formatted exceptions with context information.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws a custom exception with the specified message.
    /// </summary>
    /// <param name="message">Primary error message.</param>
    /// <param name="isReallyThrowing">Whether to actually throw or just return true.</param>
    /// <param name="secondMessage">Optional additional message.</param>
    /// <returns>True if exception was triggered, false otherwise.</returns>
    internal static bool Custom(string message, bool isReallyThrowing = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? exceptionText = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(exceptionText, isReallyThrowing);
    }

    /// <summary>
    /// Throws an exception indicating that an operation is not allowed.
    /// </summary>
    /// <param name="what">Description of what is not allowed.</param>
    /// <returns>True if exception was triggered.</returns>
    internal static bool IsNotAllowed(string what)
    {
        return ThrowIsNotNull(Exceptions.IsNotAllowed(FullNameOfExecutedCode(), what));
    }

    /// <summary>
    /// Gets the full name (type.method) of the currently executing code.
    /// </summary>
    /// <returns>Full name string in format "Namespace.Type.Method".</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    private static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type typeInstance)
        {
            typeFullName = typeInstance.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type resolvedType = type.GetType();
            typeFullName = resolvedType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws an exception if the provided message is not null.
    /// </summary>
    /// <param name="exceptionMessage">Exception message to throw, or null to skip.</param>
    /// <param name="isReallyThrowing">Whether to actually throw or just return true.</param>
    /// <returns>True if message was not null, false otherwise.</returns>
    internal static bool ThrowIsNotNull(string? exceptionMessage, bool isReallyThrowing = true)
    {
        if (exceptionMessage != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exceptionMessage);
            }
            return true;
        }
        return false;
    }
}

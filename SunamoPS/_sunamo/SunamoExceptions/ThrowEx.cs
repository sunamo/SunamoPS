namespace SunamoPS._sunamo.SunamoExceptions;

/// <summary>
/// Throws exceptions whose message starts with the place they were thrown from.
/// </summary>
internal static class ThrowEx
{
    /// <summary>
    /// Throws an exception that the given thing is not allowed.
    /// </summary>
    internal static bool IsNotAllowed(string what)
    {
        var method = new System.Diagnostics.StackFrame(1).GetMethod();
        var place = method == null ? string.Empty : method.DeclaringType?.FullName + "." + method.Name + ": ";
        throw new Exception(place + what + " is not allowed.");
    }
}

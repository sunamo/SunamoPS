namespace SunamoPS;

/// <summary>
/// Base class for processing PowerShell output and error records.
/// </summary>
public class PsOutput
{
    /// <summary>
    /// Invokes a PowerShell instance asynchronously and returns output or error messages.
    /// </summary>
    /// <param name="powerShell">PowerShell instance to invoke.</param>
    /// <returns>List of output or error strings.</returns>
    public static async Task<List<string>> InvokeAsync(PowerShell powerShell)
    {
        var output = await powerShell.InvokeAsync();
        List<string> result;
        if (powerShell.HadErrors)
        {
            result = ProcessErrorRecords(powerShell.Streams.Error);
        }
        else
        {
            result = ProcessPSObjects(output);
        }
        return result;
    }

    /// <summary>
    /// Converts a collection of ErrorRecords into a list of formatted error strings.
    /// </summary>
    /// <param name="errors">Collection of PowerShell error records.</param>
    /// <returns>List of formatted error strings.</returns>
    public static List<string> ProcessErrorRecords(PSDataCollection<ErrorRecord> errors)
    {
        List<string> result = new List<string>();
        StringBuilder stringBuilder = new();
        foreach (var item in errors)
        {
            AddErrorRecord(stringBuilder, item);
            result.Add(stringBuilder.ToString());
        }
        return result;
    }

    private static void AddErrorRecord(StringBuilder stringBuilder, ErrorRecord errorRecord)
    {
        stringBuilder.Clear();
        if (errorRecord == null) return;
        if (errorRecord.ErrorDetails != null) stringBuilder.AppendLine(errorRecord.ErrorDetails.Message);
        stringBuilder.AppendLine(errorRecord.Exception.GetAllMessages());
    }

    /// <summary>
    /// Converts a collection of PSObjects into a list of strings with Unix line endings.
    /// </summary>
    /// <param name="psObjects">Collection of PowerShell objects.</param>
    /// <returns>List of string representations.</returns>
    public static List<string> ProcessPSObjects(ICollection<PSObject> psObjects)
    {
        var output = new List<string>();
        foreach (var item in psObjects)
            if (item != null)
                output.Add(item.ToString().ToUnixLineEnding());
        return output;
    }
}

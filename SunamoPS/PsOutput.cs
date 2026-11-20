namespace SunamoPS;

/// <summary>
/// Represents the ps output type.
/// </summary>
public class PsOutput
{
    /// <summary>
    /// Invoke async.
    /// </summary>
    /// <param name="powerShell">The power shell.</param>
    /// <returns>The invoke async result.</returns>
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
    /// Process error records.
    /// </summary>
    /// <param name="errors">The errors.</param>
    /// <returns>The process error records result.</returns>
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
    /// Process PS objects.
    /// </summary>
    /// <param name="psObjects">The ps objects.</param>
    /// <returns>The process PS objects result.</returns>
    public static List<string> ProcessPSObjects(ICollection<PSObject> psObjects)
    {
        var output = new List<string>();
        foreach (var item in psObjects)
            if (item != null)
                output.Add(item.ToString().ToUnixLineEnding());
        return output;
    }
}

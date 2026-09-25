namespace SunamoPS;

public class PsOutput
{
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

    public static List<string> ProcessPSObjects(ICollection<PSObject> psObjects)
    {
        var output = new List<string>();
        foreach (var item in psObjects)
            if (item != null)
                output.Add(item.ToString().ToUnixLineEnding());
        return output;
    }
}

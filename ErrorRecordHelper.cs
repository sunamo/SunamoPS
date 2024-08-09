namespace SunamoPS;

public class ErrorRecordHelper
{
    // ErrorRecord + Exc here
    // Zjistit proč mi to např u @"C:\repos\_\Bobril_Projects\_tutorials\BobrilYt\" nezobrazuje nikdy nic ač ps konzole vypisuje něco

    public static void Text(StringBuilder sb, ErrorRecord e)
    {
        if (e == null) return; // string.Empty;

        if (e.ErrorDetails != null) sb.AppendLine(e.ErrorDetails.Message);

        sb.AppendLine(Exceptions.TextOfExceptions(e.Exception));
    }
}
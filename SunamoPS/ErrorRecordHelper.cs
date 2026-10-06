namespace SunamoPS;

public class ErrorRecordHelper
{
    public static void Text(StringBuilder stringBuilder, ErrorRecord errorRecord)
    {
        if (errorRecord == null) return;

        if (errorRecord.ErrorDetails != null) stringBuilder.AppendLine(errorRecord.ErrorDetails.Message);

        stringBuilder.AppendLine(Exceptions.TextOfExceptions(errorRecord.Exception));
    }
}

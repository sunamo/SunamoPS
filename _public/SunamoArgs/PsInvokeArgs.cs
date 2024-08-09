namespace SunamoPS._public.SunamoArgs;

public class PsInvokeArgs
{
    public static readonly PsInvokeArgs Def = new();
    public List<string> addBeforeEveryCommand = null;


    public bool immediatelyToStatus = false;


    public string pathToSaveLoadPsOutput = null;
    public bool writePb = false;
}
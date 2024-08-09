namespace SunamoPS;

public class PowershellRunnerBase
{
    public static List<string> ProcessPSObjects(ICollection<PSObject> pso)
    {
        var output = new List<string>();

        foreach (var item in pso)
            if (item != null)
                output.Add(item.ToString().ToUnixLineEnding());

        return output;
    }
}
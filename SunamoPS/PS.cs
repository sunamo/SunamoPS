namespace SunamoPS;

public class PS
{
    public static string CallPowershellCommand(string command)
    {
        using (var process = new Process())
        {
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.Arguments = "-Command " + command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            // Spusťte proces
            process.Start();
            // Zpracujte výstup
            return process.StandardOutput.ReadToEnd();
        }
    }

    /// <summary>
    ///     Tato metoda je zde proto abych moho využívat powershell i ze shared a jiných projektů z něj odvozených aniž bych
    ///     musel importovat SunamoPS
    ///     To stačí jen v assembly hlavního exe
    /// </summary>
    public static void Init()
    {
        //Builder = win.Helpers.Powershell.PowershellBuilder.GetInstance();
        //Helper = win.Helpers.Powershell.PowershellHelper.ci;
        //Parser = win.Helpers.Powershell.PowershellParser.ci;
        //Runner = win.Helpers.Powershell.PowershellRunner.ci;

        //PowershellBuilder.p = Builder;
        //PowershellHelper.p = Helper;
        //PowershellParser.p = Parser;
        //PowershellRunner.p = Runner;

        /*
         * Opravdu nechápu proč když jsem to všem udělal jako .p a .ci
         * to najednou chci u jednoho .ci.p a .GetInstance()
         * Vždycky musím mít na paměti že nemůžu nic měnit co už jsem jednou zapsal. Jedna změna vyvolá dalších X změn a potom nedělám nic jiného. Zamyslet se nad všemi důsledky.
         */
        //#region ukládá data jež jsou pro každý algoritmus jiné. Zde nemůžu použít ci, to by byla vláknová sebevražda. 
        //SunamoShared.win.Powershell.PowershellBuilder.typeToActivate = typeof(SunamoPS.PowershellBuilder);
        //#endregion


        //#region Neukládají žádné data permanentně, můžou být jako ci.
        //// 'PowershellHelper' does not contain a definition for 'p'
        //SunamoShared.win.Powershell.PowershellHelper.p = SunamoPS.PowershellHelper.ci;
        //SunamoShared.win.Powershell.PowershellParser.p = SunamoPS.PowershellParser.ci;
        //SunamoShared.win.Powershell.PowershellRunner.p = SunamoPS.PowershellRunner.ci;
        //#endregion
    }
}
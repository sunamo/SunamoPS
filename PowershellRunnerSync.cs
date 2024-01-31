namespace SunamoPS;

/// <summary>
/// Všechny metody zde volají this.Invoke nebo InvokeAsync v PowershellRunnerAsync.cs
/// Takže nic odsud nemazat, každá metoda má zde svůj smysl.
/// </summary>
public partial class PowershellRunner
{
    #region InvokeSingle
    public
#if ASYNC
async Task<List<string>>
#else
  List<string>
#endif
InvokeSingle(string command)
    {
        return (
#if ASYNC
await
#endif
Invoke(CA.ToListString(command)))[0];
    }
    #endregion




    //        public
    //#if ASYNC
    //    async Task<List<string>>
    //#else
    //    List<string>  
    //#endif
    // Invoke(string commands)
    //        {
    //            return
    //#if ASYNC
    //    await
    //#endif
    // InvokeAsync(CA.ToListString(commands), new PsInvokeArgs { immediatelyToStatus = false })[0];
    //        }



    public
#if ASYNC
async Task<List<List<string>>>
#else
  List<List<string>>
#endif
Invoke(List<string> commands)
    {
        return
#if ASYNC
await
#endif
Invoke(commands, new PsInvokeArgs { immediatelyToStatus = false });
    }
}

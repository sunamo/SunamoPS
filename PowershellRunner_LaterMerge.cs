namespace SunamoPS;

/*
 * CHybělo mi tu pár metod dle IPowershellRunner
 * Tohle jsem editoval, fakt netuším proč zase pracuji se starým kódem
 *
 *
 */

//using System;
//using sunamo.Essential;
//using System.Collections.Generic;
//using System.Linq;
//using System.Management.Automation;
//using System.Text;
//using System.Threading.Tasks;

//namespace win.Helpers.Powershell
//{
//    public partial class PowershellRunner : PowershellRunnerBase, IPowershellRunner
//    {
//        public ProgressState clpb { get; set; }
//        bool saveUsedCommandToDictionary = false;
//        public bool SaveUsedCommandToDictionary
//        {
//            get { return saveUsedCommandToDictionary; }
//            set
//            {

//#if DEBUG
//                if (value)
//                {

//                }
//#endif


//                saveUsedCommandToDictionary = value;
//            }
//        }

//        /// <summary>
//        /// Musím instanci vytvářet tady, ne v ctoru
//        /// </summary>
//        public static PowershellRunner ci = new PowershellRunner();

//        private PowershellRunner()
//        {
//            // nevím proč to tu je, FS.InvokePs mi EveryLine nenašel. 15-4-23 zakomentováno
//            //FS.InvokePs = Invoke;
//        }

//        public
//#if ASYNC
//    async Task<string>
//#else
//    string
//#endif
// InvokeLinesFromString(string v, bool writePb)
//        {
//            var l = SHGetLines.GetLines(v);



//            var result =
//#if ASYNC
//    await
//#endif
// InvokeWorker(l, new PsInvokeArgs { writePb = writePb });

//            StringBuilder sb = new StringBuilder();

//            foreach (var item in result)
//            {
//                sb.AppendLine(string.Join(Environment.NewLine, item).Trim());
//            }

//            var result2 = sb.ToString().Trim();

//            return result2;
//        }

//        /// <summary>
//        /// If in A1 will be full path specified = 'The system cannot find the file specified'
//        /// A1 if dont contains extension, append exe
//        /// </summary>
//        /// <param name="exeFileNameWithoutPath"></param>
//        /// <param name="arguments"></param>
//        public
//#if ASYNC
//    async Task<List<string>>
//#else
// List<string>
//#endif
// InvokeProcess(string exeFileNameWithoutPath, string arguments)
//        {
//            // Its not working with .net6 so temporarily disable. Když to nebude fungovat musím vymyslet náhradní řešení.
//            //W32.EnableWow64FSRedirection(false);
//            FS.AddExtensionIfDontHave(exeFileNameWithoutPath, AllExtensions.exe);

//            //Create process
//            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

//            // Must contains only filename, not full path
//            pProcess.StartInfo.FileName = exeFileNameWithoutPath;

//            //strCommandParameters are parameters to pass to program
//            pProcess.StartInfo.Arguments = arguments;

//            pProcess.StartInfo.UseShellExecute = false;

//            //Set output of program to be written to process output stream
//            pProcess.StartInfo.RedirectStandardOutput = true;

//            //Optional, recommended do not enter, then old value is not deleted and both paths is combined
//            //pProcess.StartInfo.WorkingDirectory = ;

//            //Start the process
//            pProcess.Start();
//            // Its not working with .net6 so temporarily disable. Když to nebude fungovat musím vymyslet náhradní řešení.
//            //W32.EnableWow64FSRedirection(true);

//            //Get program output
//            string strOutput = pProcess.StandardOutput.ReadToEnd();

//#if ASYNC
//            //Wait for process to finish
//            await pProcess.WaitForExitAsync();
//#else
//            //Wait for process to finish
//            pProcess.WaitForExit();
//#endif




//            var result = SHGetLines.GetLines(strOutput);
//            return result;
//        }

//        PowershellBuilder builder = new PowershellBuilder();

//        public Dictionary<string, List<string>> UsedCommandsInFolders { get; set; } = new Dictionary<string, List<string>>();
//    }
//}

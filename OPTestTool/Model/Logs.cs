using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptTestTools.Model
{
    public static class Logs
    {
        //public delegate void ToLogsEventHandler(string log);

        //public static event ToLogsEventHandler ToLogs;

        //public static void ToLog(this int log,params int[] logs)
        //{
        //    log.ToLogBase(logs);
        //}

        //public static void ToLog(this String log, params String[] logs)
        //{
        //    log.ToLogBase(logs);
        //}

        //public static void ToLog(this Double log,params double[] logs)
        //{
        //    log.ToLogBase(logs);
        //}

        //private static void ToLogBase<T>(this T log,params T[] logs)
        //{
        //    List<string> logList = new List<string>();
        //    logList.Add(log.ToString());
        //    foreach (var item in logs)
        //    {
        //        logList.Add(item.ToString());
        //    }
        //    //Frm.ToLog(string.Join(",", logs));
        //    ToLogs(string.Join(",", logList));

        //}

        //public static void ToLog<T>(this T log)
        //{
        //    ToLogs(log.ToString());
        //}

        //public static bool ToLogBool<T>(this T log)
        //{
        //    string bl = log.ToString();
        //    ToLogs(bl);
        //    return bl.ToInt32().ToBool();
        //}

        //public static void ToLog<T>(this T log, object cont)
        //{
        //    var cl = cont as Control;
        //    ToLogs(cl.Text + ":" + log);
        //}
    }
}
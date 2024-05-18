using System.Text;

namespace OPTestTool
{
    public enum LogType
    {
        Log = 1,
        Warning,
        Error,
        Exception
    }

    public static class Logger
    {
        public static event Action<LogType, string> LogMessageReceived;

        private static readonly ThreadLocal<StringBuilder> stringBuilder = new ThreadLocal<StringBuilder>();

        public static void Log(string msg)
        {
            if (LogMessageReceived != null)
            {
                LogMessageReceived(LogType.Log, (msg != null) ? msg.ToString() : "[null]");
                return;
            }
            if (stringBuilder.Value == null)
            {
                stringBuilder.Value = new StringBuilder();
            }
            stringBuilder.Value.Clear();
            stringBuilder.Value.AppendFormat("{0:HH:mm:ss} ", DateTime.Now);
            stringBuilder.Value.Append((msg != null) ? msg.ToString() : "[null]");
            string text = stringBuilder.ToString();
            System.Diagnostics.Debug.WriteLine("[logr] " + text);
        }

        public static void LogWarning(object msg)
        {
            if (LogMessageReceived != null)
            {
                LogMessageReceived(LogType.Warning, (msg != null) ? msg.ToString() : "[null]");
                return;
            }
            if (stringBuilder.Value == null)
            {
                stringBuilder.Value = new StringBuilder();
            }
            stringBuilder.Value.Clear();
            stringBuilder.Value.AppendFormat("{0:HH:mm:ss} ", DateTime.Now);
            stringBuilder.Value.Append((msg != null) ? msg.ToString() : "[null]");
            string text = stringBuilder.ToString();
            System.Diagnostics.Debug.WriteLine("[Warn] " + text);
        }

        public static void LogError(string msg)
        {
            if (LogMessageReceived != null)
            {
                LogMessageReceived(LogType.Error, (msg != null) ? msg.ToString() : "[null]");
                return;
            }
            if (stringBuilder.Value == null)
            {
                stringBuilder.Value = new StringBuilder();
            }
            stringBuilder.Value.Clear();
            stringBuilder.Value.AppendFormat("{0:HH:mm:ss} ", DateTime.Now);
            stringBuilder.Value.Append((msg != null) ? msg.ToString() : "[null]");
            string text = stringBuilder.ToString();
            System.Diagnostics.Debug.WriteLine("[Eror] " + text);
        }

        public static void LogException(Exception exception)
        {
            if (LogMessageReceived != null)
            {
                LogMessageReceived(LogType.Exception, exception.Message + "\n" + exception.StackTrace);
                return;
            }
            if (stringBuilder.Value == null)
            {
                stringBuilder.Value = new StringBuilder();
            }
            stringBuilder.Value.Clear();
            stringBuilder.Value.AppendFormat("{0:HH:mm:ss} ", DateTime.Now);
            stringBuilder.Value.Append(exception.Message + "\n" + exception.StackTrace);
            string text = stringBuilder.ToString();
            System.Diagnostics.Debug.WriteLine("[Exce] " + text);
        }

        public static void ToLog(this string log)
        {
            Log(log);
        }
    }
}
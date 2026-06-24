using Bluegrams.Application;
using System.Text;

namespace OPTestTool
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string rootPath = Path.GetDirectoryName(Application.ExecutablePath);
            System.Environment.CurrentDirectory = rootPath;

            PortableSettingsProvider.ApplyProvider(OPTestTool.Properties.Settings.Default);
            PortableSettingsProvider.ApplyProvider(WordDictTool.Properties.Settings.Default);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
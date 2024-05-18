using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPTestTool.Extension
{
    public static class ProcessExtension
    {
        public static void ShellStart(this Process process, string fileName)
        {
            Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
        }
    }
}
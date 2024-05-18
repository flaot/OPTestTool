using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.StaticData
{
    public class MemoryAsm
    {
        public static int ReadIntType = 0;

        public static int ReadStringType = 0;
        public static int ReadStringLen = 0;

        public static int ReadDataLen = 0;

        public static int WriteIntType = 0;
        public static int WriteIntV = 0;

        public static float WriteFloatV = 0;

        public static double WriteDoubleV = 0;

        public static int WriteStringType = 0;
        public static string WriteStringV = "";

        public static string WriteDataData = "";

        public static int VirtualAllocExAddr = 0;
        public static int VirtualAllocExSize = 1024;
        public static int VirtualAllocExType = 0;

        public static int VirtualFreeEx = 0;

        public static int VirtualProtectExAddr = 0;
        public static int VirtualProtectExSize = 0;
        public static int VirtualProtectExType = 0;
        public static int VirtualProtectExOldProtect = 0;

        public static string GetModuleBaseAddrModule = "";
    }
}

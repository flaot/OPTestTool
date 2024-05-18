using OPTestTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.Model
{
    public class FrmBindWindowHelper
    {
        private OpSoft mk = null;

        public FrmBindWindowHelper(OpSoft _mk)
        {
            mk = _mk;
        }

        public int BindWindow(int hwnd)
        {
            try
            {
                if (mk.BindWindow(hwnd, StaticData.BindWindow.Display, StaticData.BindWindow.Mouse, StaticData.BindWindow.Keypad, StaticData.BindWindow.Mode) == 0)
                {
                    return mk.GetLastError();
                }
                return 1;
            }
            catch (Exception ex)
            {
                ex.ToString().ToLog();
            }
            return -1;
        }

        /// <summary>
        /// 解绑窗口
        /// </summary>
        /// <returns></returns>
        public int UnBindWindow()
        {
            if (mk.UnBindWindow() == 0)
            {
                return mk.GetLastError();
            }
            return 1;
        }
    }
}
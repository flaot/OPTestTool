using OPTestTool.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.Model
{
    public class FrmSettingHelper
    {
        /// <summary>
        /// 获取窗口句柄，来自TreeViewText
        /// </summary>
        /// <param name="data"></param>
        /// <returns>返回窗口句柄</returns>
        public static int GetHwndWhereParsTreeViewText(string data)
        {
            if (data.Length > 0 & data.Contains("|"))
            {
                return data.Split('|')[0].ToInt32();
            }
            return 0;
        }
    }
}
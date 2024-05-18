using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.Entity
{
    public class RegSetting
    {
        public string Reg { get; set; } = "";
        public string RegNote { get; set; } = "";
        public string Ip { get; set; } = "";
        public bool UserIp { get; set; } = false;
        public bool RememberPwd { get; set; } = false;
        public bool HideReg { get; set; } = false;
        public bool Free { get; set; } = true;
        /// <summary>
        /// 注册码
        /// </summary>
        public bool MachineCode { get; set; } = false;
        public int Tm { get; set; } = 0;
        /// <summary>
        /// 机器码
        /// </summary>
        public string Machine { get; set; } = "";
        public string FreeMachine { get; set; } = "";
    }
}

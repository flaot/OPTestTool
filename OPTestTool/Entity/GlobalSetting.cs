using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.Entity
{
    public class GlobalSetting
    {
        //综合设置区
        public bool TimeInfo { get; set; } = false;

        public bool AutoRoll { get; set; } = false;
        public bool ClearHide { get; set; } = false;
        public string SetPath { get; set; } = "";

        //绑定参数区
        public bool BindMoveWindow { get; set; } = false;

        public bool BindSuccessTips { get; set; } = false;
        public string PlugObj { get; set; } = "op";

        //测试图色区
        public int EnableGetColorByCapture { get; set; } = 0;

        public bool EnablePicCacheOpenCache { get; set; } = true;
        public bool CustomCodeIf { get; set; } = false;
        public string CustomCodeIfContent { get; set; } = "";
        public bool CustomCodeElse { get; set; } = false;
        public string CustomCodeElseContent { get; set; } = "";
        public bool AutoGetLanPoint { get; set; } = false;

        //测试鼠标区
        public bool TestMouseAfterMoveOperation { get; set; } = false;

        public bool TestMouseBeforeOperationActive { get; set; } = false;

        //测试键盘区
        public bool TestKeyboardBeforeOperationActive { get; set; } = false;

        //文本输入区
        public bool ContentWriteBeforeOperationActive { get; set; } = false;

        //内存汇编区

        //测试脚本区
        public string TestScriptScriptPath { get; set; } = "";

        //其它测试区
    }
}
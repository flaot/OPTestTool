using ScriptTestTools.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using OPTestTool.Extension;

namespace ScriptTestTools.Model
{
    public class GlobalSettingService
    {
        private string SettingPath = "";

        public GlobalSettingService(string path)
        {
            if (!File.Exists(path))
                File.WriteAllText(path, "");

            SettingPath = path;
        }

        /// <summary>
        /// 读取Setting配置文件
        /// </summary>
        /// <returns></returns>
        public GlobalSetting ReadSetting()
        {
            return JsonConvert
                .DeserializeObject<GlobalSetting>(SettingPath.ReadJsonFile()) ?? new GlobalSetting();
        }

        /// <summary>
        /// 写入Setting配置文件
        /// </summary>
        /// <param name="globalSetting"></param>
        public void WriteSetting(GlobalSetting globalSetting)
        {
            SettingPath.WriteJsonFile(JsonConvert.SerializeObject(globalSetting));
        }
    }
}
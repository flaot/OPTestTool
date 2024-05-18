using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OPTestTool.Extension;
using ScriptTestTools.Entity;

namespace ScriptTestTools.Model
{
    internal class SettingService
    {
        private string SettingPath = "";

        public SettingService(string path)
        {
            if (!File.Exists(path))
            {
                using (var file = File.Create(path))
                {
                    file.Close();
                }
            }
        }

        /// <summary>
        /// 读取Setting配置文件
        /// </summary>
        /// <returns></returns>
        public T ReadSetting<T>()
        {
            return JsonConvert
                .DeserializeObject<T>(SettingPath.ReadJsonFile());
        }

        /// <summary>
        /// 写入Setting配置文件
        /// </summary>
        /// <param name="globalSetting"></param>
        public void WriteSetting<T>(T globalSetting)
        {
            SettingPath.WriteJsonFile(JsonConvert.SerializeObject(globalSetting));
        }
    }
}
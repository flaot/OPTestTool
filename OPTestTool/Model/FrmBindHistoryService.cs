using Newtonsoft.Json;
using OPTestTool.Extension;
using ScriptTestTools.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.Model
{
    public class FrmBindHistoryService
    {
        private string pathHistory = "";

        public FrmBindHistoryService(string path)
        {
            pathHistory = path;
        }

        /// <summary>
        /// 读取BindHistory配置文件
        /// </summary>
        /// <returns></returns>
        public List<FrmBindHistory> ReadSetting()
        {
            return JsonConvert
                .DeserializeObject<List<FrmBindHistory>>(pathHistory.ReadJsonFile()) ?? new List<FrmBindHistory>();
        }

        /// <summary>
        /// 写入BindHistory配置文件
        /// </summary>
        /// <param name="history"></param>
        public void WriteSetting(List<FrmBindHistory> history)
        {
            pathHistory.WriteJsonFile(JsonConvert.SerializeObject(history));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPTestTool.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 过滤引号
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string FilteQuotes(this String data)
        {
            if (data.Length > 0)
            {
                if (data.Contains('"'))
                {
                    return data.Replace('"', ' ').Trim();
                }
            }
            return data;
        }
    }
}
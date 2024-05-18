using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPTestTool.Extension
{
    /// <summary>
    /// 转换扩展
    /// </summary>
    public static class ConvertExtension
    {
        /// <summary>
        /// 将指定的值转换为 32 位无符号整数。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>value，舍入为最接近的 32 位无符号整数。 如果 value 为两个整数中间的数字，则返回二者中的偶数；即 4.5 转换为 4，而 5.5 转换为 6。</returns>
        public static int ToInt32<T>(this T data)
        {
            return Convert.ToInt32(data);
        }

        /// <summary>
        /// 将指定的值转换为双精度浮点数。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>与 value 等效的双精度浮点数，如果 value 为 null，则为零。</returns>
        public static double ToDouble<T>(this T data)
        {
            //Convert.ToDateTime
            return Convert.ToDouble(data);
        }

        /// <summary>
        /// 将指定的值转换为 DateTime 值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>value 的值的日期和时间等效项，如果 MinValue 为 value，则为 null 的日期和时间等效项。</returns>
        public static DateTime ToDateTime<T>(this T data)
        {
            return Convert.ToDateTime(data);
        }

        /// <summary>
        /// 0=False,非0=True
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ToBool<T>(this T data)
        {
            return Convert.ToBoolean(data);
        }

        /// <summary>
        /// 将指定的值转换为十进制数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static decimal ToDecimal<T>(this T data)
        {
            return Convert.ToDecimal(data);
        }

        /// <summary>
        /// 将指定的值转换为 64 位有符号整数。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>一个与 value 中数字等效的 64 位带符号整数，如果 value 为 null，则为 0（零）</returns>
        public static long ToInt64<T>(this T data)
        {
            return Convert.ToInt64(data);
        }

        /// <summary>
        /// 将指定的值转换为 16 位有符号整数。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>一个与 value 中数字等效的 16 位带符号整数，如果 value 为 null，则为 0（零）。</returns>
        public static short ToInt16<T>(this T data)
        {
            return Convert.ToInt16(data);
        }

        /// <summary>
        /// 将指定的值转换为单精度浮点数。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>与 value 中数字等效的单精度浮点数，如果 value 为 null，则为 0（零）。</returns>
        public static float ToSingle<T>(this T data)
        {
            return Convert.ToSingle(data);
        }
    }
}
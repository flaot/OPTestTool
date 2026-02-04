using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WordDictTool
{
    public class GrayHelper
    {
        public const int WORD_BKCOLOR = 0;
        public const int WORD_COLOR = 1;
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Color_t
        {
            public byte a;
            public byte r;
            public byte g;
            public byte b;
            public void str2color(string s)
            {
                byte[] bytes = Hex2bin(s.ToUpper());
                r = bytes[0];
                g = bytes[1];
                b = bytes[2];
            }
            public byte toGray()
            {
                return (byte)((r * 299 + g * 587 + b * 114 + 500) / 1000);
            }
        }
        private struct Color_Df
        {
            public Color_t color;
            public Color_t df;
        }
        public static Image GrayImage(Image orgImage, string colorStr)
        {
            if (str2colordfs(colorStr, out var colors) == false)
                return Bgr2binary(orgImage, colors);
            else
                return Bgr2binarybk(orgImage, colors);
        }
        public static byte[] Hex2bin(string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                char c1 = hexString[i];
                char c2 = hexString[i + 1];
                byte b1 = (byte)((c1 <= '9' ? c1 - '0' : c1 - 'A' + 10) & 0xF);
                byte b2 = (byte)((c2 <= '9' ? c2 - '0' : c2 - 'A' + 10) & 0xF);
                bytes[i / 2] = (byte)(((b1 << 4) & 0xF0) + b2);
            }
            return bytes;
        }
        private static bool str2colordfs(string colorStr, out List<Color_Df> colors)
        {
            colors = new List<Color_Df>();
            bool ret = false;
            if (string.IsNullOrEmpty(colorStr))
            {   //default
                return true;
            }
            if (colorStr[0] == '@')
            { //bk color info
                ret = true;
            }
            if (ret)
                colorStr = colorStr.Substring(1);
            string[] vstr = colorStr.Split('|', StringSplitOptions.RemoveEmptyEntries);
            foreach (var it in vstr)
            {
                string[] vstr2 = it.Split('-', StringSplitOptions.RemoveEmptyEntries);
                Color_Df cr = new Color_Df();
                cr.color.str2color(vstr2[0]);
                if (vstr2.Length > 1)
                    cr.df.str2color(vstr2[1]);
                colors.Add(cr);
            }
            return ret;
        }

        private static Image Bgr2binary(Image orgImage, List<Color_Df> colors)
        {
            Bitmap bitmap = new Bitmap(orgImage);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                int bytesPerPixel = Image.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
                int height = bitmap.Height;
                int width = bitmap.Width;
                int stride = bitmapData.Stride;
                var scan = (byte*)bitmapData.Scan0;
                int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;
                byte[] rgbValues = new byte[bytes];
                Marshal.Copy(new IntPtr(scan), rgbValues, 0, bytes);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int i = y * width * bytesPerPixel + x * bytesPerPixel;
                        fixed (byte* pData = &rgbValues[i])
                        {
                            byte a = rgbValues[i]; // Alpha (透明度)
                            byte r = rgbValues[i + 1]; // Red (红色)
                            byte g = rgbValues[i + 2]; // Green (绿色)
                            byte b = rgbValues[i + 3]; // Blue (蓝色)
                            *(int*)pData = (r * 299 + g * 587 + b * 114 + 500) / 1000;
                        }
                    }
                }
                for (int i = 0; i < height; ++i)
                {
                    int rowIndex = i * width * bytesPerPixel;
                    fixed (byte* pbinSrc = &rgbValues[i * width])
                    {
                        byte* pbin = pbinSrc;
                        Color_t* psrc = (Color_t*)&scan[rowIndex];
                        for (int j = 0; j < width; ++j)
                        {
                            byte g1 = psrc->toGray();
                            *pbin = WORD_BKCOLOR;
                            for (int cIndex = 0; cIndex < colors.Count; cIndex++)
                            {
                                var it = colors[cIndex];
                                //对每个颜色描述
                                if (Math.Abs(g1 - it.color.toGray()) <= it.df.toGray())
                                {
                                    *pbin = WORD_COLOR;
                                    break;
                                }
                            }
                            ++pbin;
                            ++psrc;
                        }
                    }
                }
                Marshal.Copy(rgbValues, 0, new IntPtr(scan), bytes);
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }
        private static Image Bgr2binarybk(Image orgImage, List<Color_Df> colors)
        {
            Bitmap bitmap = new Bitmap(orgImage);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                int bytesPerPixel = Image.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
                int height = bitmap.Height;
                int width = bitmap.Width;
                int stride = bitmapData.Stride;
                var scan = (byte*)bitmapData.Scan0;
                int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;
                byte[] rgbValues = new byte[bytes];
                Marshal.Copy(new IntPtr(scan), rgbValues, 0, bytes);
                if (colors.Count == 0)
                {
                    //fromImage4
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            int i = y * width * bytesPerPixel + x * bytesPerPixel;
                            fixed (byte* pData = &rgbValues[i])
                            {
                                byte a = rgbValues[i]; // Alpha (透明度)
                                byte r = rgbValues[i + 1]; // Red (红色)
                                byte g = rgbValues[i + 2]; // Green (绿色)
                                byte b = rgbValues[i + 3]; // Blue (蓝色)
                                *(int*)pData = (r * 299 + g * 587 + b * 114 + 500) / 1000;
                            }
                        }
                    }
                    //TODO
                }
                else
                {
                    for (int cIndex = 0; cIndex < colors.Count; cIndex++)
                    {
                        var bk = colors[cIndex];
                        for (int x = 0; x < bytes; x++)
                        {
                            Color_t* c = (Color_t*)&scan[x * 4];
                            if (!(Math.Abs((*c).b - bk.color.b) <= bk.df.b &&
                               Math.Abs((*c).g - bk.color.g) <= bk.df.g &&
                               Math.Abs((*c).r - bk.color.r) <= bk.df.r))
                            {
                                fixed (byte* pbin = &rgbValues[x])
                                {
                                    *(int*)pbin = WORD_COLOR;
                                }
                            }
                        }
                    }
                }
                Marshal.Copy(rgbValues, 0, new IntPtr(scan), bytes);
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }
    }
}

using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WordDictTool
{
    /// <summary>
    /// 单通道图像
    /// </summary>
    public class GrayImageBin
    {
        public const byte WORD_BKCOLOR = 0;
        public const byte WORD_COLOR = 1;

        public int width;
        public int height;
        public byte[] pixels;
        private GrayImageBin() { }
        public static Bitmap GrayImage(Image orgImage, string colorStr)
        {
            GrayImageBin binary = new GrayImageBin();
            binary.Create(orgImage.Width, orgImage.Height);
            if (Str2colordfs(colorStr, out var colors) == false)
                binary.FromImageFK(orgImage, colors);
            else
                binary.FromImageBK(orgImage, colors);
            return binary.GetImage();
        }
        public void Create(int w, int h)
        {
            width = w;
            height = h;
            pixels = new byte[w * h];
        }
        private static bool Str2colordfs(string colorStr, out List<Color_Df> colors)
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
                cr.color.Str2color(vstr2[0]);
                if (vstr2.Length > 1)
                    cr.df.Str2color(vstr2[1]);
                colors.Add(cr);
            }
            return ret;
        }
        private void FromImageFK(Image img4, List<Color_Df> colors)
        {
            Bitmap bitmap = new Bitmap(img4);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            unsafe
            {
                int bytesPerPixel = Image.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
                var pImage = (byte*)bitmapData.Scan0;
                for (int i = 0; i < pixels.Length; ++i)
                {
                    var imIndex = i * bytesPerPixel;
                    Color_t* color = (Color_t*)&pImage[imIndex];
                    pixels[i] = color->ToGray();
                }
                for (int i = 0; i < height; ++i)
                {
                    int rowIndex = i * width * bytesPerPixel;
                    Color_t* psrc = (Color_t*)&pImage[rowIndex];
                    for (int j = 0; j < width; ++j)
                    {
                        byte g1 = psrc->ToGray();
                        int binIndex = i * width + j;
                        pixels[binIndex] = WORD_BKCOLOR;
                        for (int cIndex = 0; cIndex < colors.Count; cIndex++)
                        {
                            var it = colors[cIndex];
                            //对每个颜色描述
                            if (Math.Abs(g1 - it.color.ToGray()) <= it.df.ToGray())
                            {
                                pixels[binIndex] = WORD_COLOR;
                                break;
                            }
                        }
                        ++psrc;
                    }
                }
            }
            bitmap.UnlockBits(bitmapData);
        }
        private void FromImageBK(Image img4, List<Color_Df> colors)
        {
            Bitmap bitmap = new Bitmap(img4);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            unsafe
            {
                int bytesPerPixel = Image.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
                var pImage = (byte*)bitmapData.Scan0;
                if (colors.Count == 0)
                {
                    byte[] grayTemp = new byte[pixels.Length];
                    for (int i = 0; i < pixels.Length; ++i)
                    {
                        var imIndex = i * bytesPerPixel;
                        Color_t* color = (Color_t*)&pImage[imIndex];
                        grayTemp[i] = color->ToGray();
                    }
                    int bkcolor = GetBKColor(grayTemp);
                    for (int i = 0; i < width * height; ++i)
                    {
                        pixels[i] = (Math.Abs(grayTemp[i] - bkcolor) < 20 ? WORD_BKCOLOR : WORD_COLOR);
                    }
                }
                else
                {
                    for (int cIndex = 0; cIndex < colors.Count; cIndex++)
                    {
                        var bk = colors[cIndex];
                        for (int x = 0; x < width * height; x++)
                        {
                            Color_t* c = (Color_t*)&pImage[x * 4];
                            if (!(Math.Abs((*c).b - bk.color.b) <= bk.df.b &&
                               Math.Abs((*c).g - bk.color.g) <= bk.df.g &&
                               Math.Abs((*c).r - bk.color.r) <= bk.df.r))
                            {
                                pixels[x] = WORD_COLOR;
                            }
                            else
                            {
                                pixels[x] = WORD_BKCOLOR;
                            }
                        }
                    }
                }
            }
            bitmap.UnlockBits(bitmapData);
        }
        private static int GetBKColor(byte[] bin)
        {
            int[] y = new int[256];
            for (int i = 0; i < bin.Length; ++i)
                y[bin[i]]++;
            // scan max
            int m = 0;
            for (int i = 1; i < 256; ++i)
            {
                if (y[i] > y[m]) m = i;
            }
            return m;
        }
        private Bitmap GetImage()
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                int bytesPerPixel = Image.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
                var pImage = (byte*)bitmapData.Scan0;
                int byteCount = Math.Abs(bitmapData.Stride) * bitmap.Height;
                for (int i = 0; i < pixels.Length; ++i)
                {
                    var imIndex = i * 4;
                    byte v = pixels[i] == 1 ? (byte)0xff : (byte)0;
                    pImage[imIndex] = pImage[imIndex + 1] = pImage[imIndex + 2] = v;
                    pImage[imIndex + 3] = 0xff; //Aphal
                }
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Color_t
        {
            public byte b;
            public byte g;
            public byte r;
            public byte a;
            public void Str2color(string s)
            {
                byte[] bytes = WordData.Hex2bin(s.ToUpper());
                r = bytes[0];
                g = bytes[1];
                b = bytes[2];
            }
            public byte ToGray()
            {
                return (byte)((r * 299 + g * 587 + b * 114 + 500) / 1000);
            }
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Color_Df
        {
            public Color_t color;
            public Color_t df;
        }
    }
}

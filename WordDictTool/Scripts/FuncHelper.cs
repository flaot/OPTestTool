namespace WordDictTool
{
    public static class FuncHelper
    {
        public static string ColorToHex(Color color)
        {
            return string.Format("{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }
        public static Color HexToColor(string hex)
        {
            return Color.FromArgb(0xFF, Convert.ToInt32(hex.Substring(0, 2), 16),
                                   Convert.ToInt32(hex.Substring(2, 2), 16),
                                   Convert.ToInt32(hex.Substring(4, 2), 16));
        }
        /// <summary>
        /// 将图像等比例缩放（不使用线性插值）
        /// </summary>
        /// <param name="originalImage">原始图像</param>
        /// <param name="scaleFactor">缩放比例</param>
        /// <param name="line">辅助线大小</param>
        /// <returns>缩放后的图像</returns>
        public static Bitmap ResizeImageProportionally(Image originalImage, int scaleFactor)
        {
            if (originalImage == null)
                throw new ArgumentNullException(nameof(originalImage));

            if (scaleFactor <= 0)
                throw new ArgumentOutOfRangeException(nameof(scaleFactor), "缩放比例必须大于0");

            // 计算新尺寸
            int newWidth = originalImage.Width * scaleFactor;
            int newHeight = originalImage.Height * scaleFactor;

            // 创建新位图
            Bitmap resizedImage = new Bitmap(newWidth, newHeight);

            // 获取原始图像数据
            Bitmap originalBitmap = new Bitmap(originalImage);

            // 手动进行像素映射（最近邻插值）
            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    // 计算在原始图像中的对应位置
                    int sourceX = x / scaleFactor;
                    int sourceY = y / scaleFactor;

                    // 边界检查
                    if (sourceX >= 0 && sourceX < originalBitmap.Width &&
                        sourceY >= 0 && sourceY < originalBitmap.Height)
                    {
                        // 直接复制像素值
                        Color pixelColor = originalBitmap.GetPixel(sourceX, sourceY);
                        resizedImage.SetPixel(x, y, pixelColor);
                    }
                }
            }

            return resizedImage;
        }
    }
}

using System;
using System.Drawing;
using System.IO;

public partial class OpSoft
{
    public Image GetScreenDataBmp(int x, int y, int x2, int y2)
    {
        this.GetScreenDataBmp(0, 0, x2, y2, out var pBmp, out var bmpSize);
        if (pBmp == IntPtr.Zero)
            return null;
        var tbuf = new byte[bmpSize];
        unsafe
        {
            byte* memBytePtr = (byte*)pBmp.ToPointer();
            using (UnmanagedMemoryStream ms = new UnmanagedMemoryStream(memBytePtr, (long)bmpSize, (long)bmpSize, FileAccess.Read))
            {
                ms.Read(tbuf, 0, tbuf.Length);
            }
        }
        Image iamge = null;
        try
        {
            using (MemoryStream ms = new MemoryStream(tbuf))
            {
                iamge = Image.FromStream(ms);
            }
        }
        catch (Exception)
        {
            iamge = null;
        }
        return iamge;
    }
}

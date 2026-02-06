using System.Text;

namespace WordDictTool
{
    /// <summary> 字特征结构 </summary>
    public class WordData
    {
        private string _name;
        private int _w;
        private int _h;
        private byte[] _data;
        private int _bitCnt;
        private string _wordCode;

        public string name => _name;
        public int w => _w;
        public int h => _h;
        public byte[] data => _data;
        public int bitCnt => _bitCnt;
        public string wordCode => _wordCode;

        public void SetChar(string ch)
        {
            _name = ch;
            _wordCode = UnPause();
        }
        public void Pause(string wordCode)
        {
            _wordCode = wordCode;
            var vstr = wordCode.Split('$');
            if (vstr.Length < 3)
                return;

            _name = vstr[0];
            string info = vstr[1];
            string dataStr = vstr[2];
            vstr = info.Split(',');
            if (vstr.Length < 3)
                return;

            int.TryParse(vstr[0], out _h);
            int.TryParse(vstr[1], out _w);
            int.TryParse(vstr[2], out _bitCnt);
            _data = Hex2bin(dataStr);
        }
        private string UnPause()
        {
            string str = $"{name}${h},{w},{bitCnt}$";
            StringBuilder sb = new StringBuilder(str, str.Length + data.Length * 2);
            for (int i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("X2"));
            return sb.ToString();
        }
        public override string ToString()
        {
            return _wordCode;
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
    }
}

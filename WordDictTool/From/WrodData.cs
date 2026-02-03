using System.Text;

namespace WordDictTool
{
    public class WrodData
    {
        public string name;
        public readonly int w;
        public readonly int h;
        public readonly byte[] data;
        public readonly int bitCnt;
        public readonly string wordCode;
        public bool addToDict;
        public WrodData(string wordCode, bool addToDict)
        {
            this.wordCode = wordCode;
            this.addToDict = addToDict;
            var vstr = wordCode.Split('|');
            if (vstr.Length < 3)
                return;

            string name = vstr[0];
            string info = vstr[1];
            string dataStr = vstr[2];
            vstr = info.Split(',');
            if (vstr.Length < 3)
                return;

            int.TryParse(vstr[0], out h);
            int.TryParse(vstr[1], out w);
            int.TryParse(vstr[2], out bitCnt);
            data = Hex2bin(dataStr);
        }
        private static byte[] Hex2bin(string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                char c1 = hexString[i];
                char c2 = hexString[i + 1];
                byte b1 = (byte)(char.IsDigit(c1) ? c1 - '0' : char.ToUpper(c1) - 'A' + 10);
                byte b2 = (byte)(char.IsDigit(c2) ? c2 - '0' : char.ToUpper(c2) - 'A' + 10);
                bytes[i / 2] = (byte)(b1 << 4 + b2);
            }
            return bytes;
        }
        public override string ToString()
        {
            return wordCode;
        }
    }
}


namespace WordDictTool
{
    public class WordData
    {
        public string name;
        public readonly int w;
        public readonly int h;
        public readonly byte[] data;
        public readonly int bitCnt;
        public readonly string wordCode;
        public bool addToDict;
        public WordData(string wordCode, bool addToDict)
        {
            this.wordCode = wordCode;
            this.addToDict = addToDict;
            var vstr = wordCode.Split('$');
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
            data = GrayHelper.Hex2bin(dataStr);
        }
        public override string ToString()
        {
            return wordCode;
        }
    }
}

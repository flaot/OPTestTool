namespace WordDictTool
{
    /// <summary> 字库 </summary>
    public class WordDict
    {
        public readonly List<WordData> wrods = new List<WordData>();
        private readonly Dictionary<int, WordData> _hashWord = new Dictionary<int, WordData>();
        public event Action<WordDict> OnChange;

        private bool _firtEvent = true;

        public void Clear()
        {
            if (wrods.Count <= 0)
                return;
            wrods.Clear();
            _hashWord.Clear();
            FireChangeEvent();
        }
        public void Add(string wordCode)
        {
            var hash = wordCode.GetHashCode();
            if (_hashWord.TryGetValue(hash, out var _))
                return;
            WordData data = new WordData();
            data.Pause(wordCode);
            _hashWord.Add(data.wordCode.GetHashCode(), data);
            wrods.Add(data);
            FireChangeEvent();
        }
        public void Remove(string wordCode)
        {
            var hash = wordCode.GetHashCode();
            if (!_hashWord.TryGetValue(hash, out var wordData))
                return;
            wrods.Remove(wordData);
            _hashWord.Remove(hash);
            FireChangeEvent();
        }
        public WordData Find(string wordCode)
        {
            _hashWord.TryGetValue(wordCode.GetHashCode(), out var wordData);
            return wordData;
        }
        public void Replace(string oldCode, string newCode)
        {
            var oldHash = oldCode.GetHashCode();
            var newHash = newCode.GetHashCode();
            //不存在旧数据
            if (!_hashWord.TryGetValue(oldHash, out var wordData))
            {
                //不存在新数据
                if (!_hashWord.ContainsKey(newHash))
                {
                    Add(newCode);
                }
                return;
            }
            _hashWord.Remove(oldHash);
            //存在新数据
            if (_hashWord.ContainsKey(newHash))
            {
                wrods.Remove(wordData);
                FireChangeEvent();
                return;
            }
            //替换数据
            _hashWord.Add(newCode.GetHashCode(), wordData);
            wordData.Pause(newCode);
            FireChangeEvent();
        }
        public void ReplaceChar(string oldCode, string newChar)
        {
            var oldHash = oldCode.GetHashCode();
            //不存在旧数据
            if (!_hashWord.TryGetValue(oldHash, out var wordData))
            { 
                WordData wordData1 = new WordData();
                wordData1.Pause(oldCode);
                wordData1.SetChar(newChar);
                var newHashTemp = wordData1.wordCode.GetHashCode();
                if (!_hashWord.ContainsKey(newHashTemp))
                { 
                    wrods.Add(wordData1);
                    _hashWord.Add(newHashTemp, wordData1);
                    FireChangeEvent();
                }
                return;
            }
            if (string.Equals(wordData.name, newChar))
                return;
            _hashWord.Remove(oldHash);
            wordData.SetChar(newChar);
            //存在新数据
            var newCode = wordData.ToString();
            var newHash = newCode.GetHashCode();
            if (_hashWord.TryGetValue(newHash, out var _))
            {
                wrods.Remove(wordData);
                FireChangeEvent();
                return;
            }
            _hashWord.Add(newHash, wordData);
            FireChangeEvent();
        }
        public void SortByChar()
        {
            wrods.Sort((l, r) => l.name.CompareTo(r.name));
            FireChangeEvent();
        }
        public void SortByBitCnt()
        {
            wrods.Sort((l, r) => l.bitCnt.CompareTo(r.bitCnt));
            FireChangeEvent();
        }

        public void PauseEvent()
        {
            _firtEvent = false;
        }
        public void ResumeEvent()
        {
            _firtEvent = true;
        }

        private void FireChangeEvent()
        {
            if (_firtEvent)
                OnChange?.Invoke(this);
        }
    }
}

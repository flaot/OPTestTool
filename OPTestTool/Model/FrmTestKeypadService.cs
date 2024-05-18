using ScriptTestTools.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.Model
{
    public class FrmTestKeypadService
    {
        private List<Keypad> keypad = new List<Keypad>();
        public FrmTestKeypadService()
        {
            InitData();
        }

        private void InitData()
        {
            keypad.Add(new Keypad("1", 49));
            keypad.Add(new Keypad("2", 50));
            keypad.Add(new Keypad("3", 51));
            keypad.Add(new Keypad("4", 52));
            keypad.Add(new Keypad("5", 53));
            keypad.Add(new Keypad("6", 54));
            keypad.Add(new Keypad("7", 55));
            keypad.Add(new Keypad("8", 56));
            keypad.Add(new Keypad("9", 57));
            keypad.Add(new Keypad("0", 48));

            keypad.Add(new Keypad("-", 189));
            keypad.Add(new Keypad("=", 187));
            keypad.Add(new Keypad("back", 8));
            keypad.Add(new Keypad("a", 65));
            keypad.Add(new Keypad("b", 66));
            keypad.Add(new Keypad("c", 67));
            keypad.Add(new Keypad("d", 68));
            keypad.Add(new Keypad("e", 69));
            keypad.Add(new Keypad("f", 70));
            keypad.Add(new Keypad("g", 71));

            keypad.Add(new Keypad("h", 72));
            keypad.Add(new Keypad("i", 73));
            keypad.Add(new Keypad("j", 74));
            keypad.Add(new Keypad("k", 75));
            keypad.Add(new Keypad("l", 76));
            keypad.Add(new Keypad("m", 77));
            keypad.Add(new Keypad("n", 78));
            keypad.Add(new Keypad("o", 79));
            keypad.Add(new Keypad("p", 80));
            keypad.Add(new Keypad("q", 81));

            keypad.Add(new Keypad("r", 82));
            keypad.Add(new Keypad("s", 83));
            keypad.Add(new Keypad("t", 84));
            keypad.Add(new Keypad("u", 85));
            keypad.Add(new Keypad("v", 86));
            keypad.Add(new Keypad("w", 87));
            keypad.Add(new Keypad("x", 88));
            keypad.Add(new Keypad("y", 89));
            keypad.Add(new Keypad("z", 90));
            keypad.Add(new Keypad("ctrl", 17));

            keypad.Add(new Keypad("alt", 18));
            keypad.Add(new Keypad("shift", 16));
            keypad.Add(new Keypad("win", 91));
            keypad.Add(new Keypad("space", 32));
            keypad.Add(new Keypad("cap", 20));
            keypad.Add(new Keypad("tab", 9));
            keypad.Add(new Keypad("~", 192));
            keypad.Add(new Keypad("esc", 27));
            keypad.Add(new Keypad("enter", 13));
            keypad.Add(new Keypad("up", 38));

            keypad.Add(new Keypad("down", 40));
            keypad.Add(new Keypad("left", 37));
            keypad.Add(new Keypad("right", 39));
            keypad.Add(new Keypad("option", 93));
            keypad.Add(new Keypad("print", 44));
            keypad.Add(new Keypad("delete", 46));
            keypad.Add(new Keypad("home", 36));
            keypad.Add(new Keypad("end", 35));
            keypad.Add(new Keypad("pgup", 33));
            keypad.Add(new Keypad("pgdn", 34));

            keypad.Add(new Keypad("f1", 112));
            keypad.Add(new Keypad("f2", 113));
            keypad.Add(new Keypad("f3", 114));
            keypad.Add(new Keypad("f4", 115));
            keypad.Add(new Keypad("f5", 116));
            keypad.Add(new Keypad("f6", 117));
            keypad.Add(new Keypad("f7", 118));
            keypad.Add(new Keypad("f8", 119));
            keypad.Add(new Keypad("f9", 120));
            keypad.Add(new Keypad("f10", 121));

            keypad.Add(new Keypad("f11", 122));
            keypad.Add(new Keypad("f12", 123));
            keypad.Add(new Keypad("[", 219));
            keypad.Add(new Keypad("]", 221));
            keypad.Add(new Keypad("\\", 220));
            keypad.Add(new Keypad(";", 186));
            keypad.Add(new Keypad("'", 222));
            keypad.Add(new Keypad(",", 188));
            keypad.Add(new Keypad(".", 190));
            keypad.Add(new Keypad("/", 191));
        }

        /// <summary>
        /// 返回自定义虚拟键盘的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetKeypadValue(int key)
        {
            string val = "";
            try
            {
                val = keypad.Where(w => w.VirtualKeyCode == key)
                 .Select(s => s.VirtualValueCode).FirstOrDefault().ToString();
            }
            catch
            {
                val = "-1";
            }

            return val;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.Entity
{
    public class Keypad
    {
        public int VirtualKeyCode { get; set; }
        public string VirtualValueCode { get; set; }

        public Keypad(string va,int ky)
        {
            VirtualValueCode = va;
            VirtualKeyCode = ky;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimDef
{
    public class Patch : Def
    {
        public List<string> mods = new List<string>();
        public string patchType = "-unset-";
        public string xpath = "-unset-";
        public string value = "-unset-";
        public string success = "-unset-";
        public string container = "-unset-";
    }
}

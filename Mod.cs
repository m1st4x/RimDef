using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimDef
{
    public class Mod
    {
        public string name;
        public string packageId;
        public string version;
        public string dir;
        public string defPath;
        public string patchPath;

        public Mod(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
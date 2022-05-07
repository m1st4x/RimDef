using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimDef
{
    class Mod
    {
        public string name;
        public string packageId;
        public string dir;
        public string defPath;

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
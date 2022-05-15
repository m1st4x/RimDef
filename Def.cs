using System.Collections.Generic;

namespace RimDef
{
    public class Def
    {
        public string modName;
        public string defType;
        public string defName;
        public string label;
        public string description;
        public string texture;
        public string xml;
        public string file;
        public bool disabled;

        public List<string[]> details = new List<string[]>();
    }
}
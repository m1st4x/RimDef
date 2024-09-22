using System.Collections.Generic;

namespace RimDef
{
    public class Def
    {
        public Mod mod;

        public string defType;
        public string defName;
        public string label;
        public string description;
        public string texture;
        public string xml;
        public string file;
        public bool enabled;

        public List<string[]> details = new List<string[]>();
    }
}
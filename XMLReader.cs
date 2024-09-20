using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace RimDef
{
    class XMLReader
    {
        public string modDir;

        public List<string> defTypes;

        public List<Def> loadAllPatches(Mod mod)
        {
            List<Def> defs = new List<Def>();
            try
            {
                string path = mod.dir + "/" + mod.version + "/Patches";
                string[] files = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    Console.WriteLine("reading " + file);
                    var doc = new XmlDocument();
                    doc.Load(file);
                    foreach (XmlNode node in doc.DocumentElement.SelectNodes("/Patch/Operation"))
                    {
                        foreach (XmlNode child in node.ChildNodes)
                        {
                            //TODO new subclass Patch
                            Def def = new Def();
                            def.defType = "patch";
                            def.defName = "unset";
                            if (child.Attributes["Class"] != null)
                            {
                                def.defName = child.Attributes["Class"].Value;
                            }

                            string xpath = "unset";
                            string value = "unset";

                            string key = child.Name;

                            if (key == "xpath") xpath = child.InnerText;

                            if (key == "value")
                            {
                                value = child.InnerText;
                                foreach (XmlNode patchNode in child.SelectNodes("li"))
                                {
                                    //Console.WriteLine($"{patchNode.InnerText} {value}");
                                }
                            }

                            def.xml = System.Xml.Linq.XDocument.Parse(child.OuterXml).ToString(); ;
                            def.mod = mod;
                            def.file = file;
                            def.label = xpath;
                            def.description = value;
                            def.disabled = false;                            
                            defs.Add(def);

                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            return defs;
        }

        public List<Def> loadAllDefs(Mod mod)
        {
            Console.WriteLine("loading mod: " + mod.name);

            defTypes = new List<string>();

            List<Def> defs = new List<Def>();

            // NOTE: The contents of a Def folder don't follow a clear naming convention, 
            // but the folder names are generally the same in every mod.
            // see https://rimworldwiki.com/wiki/Modding_Tutorials/Mod_folder_structure
            try
            {
                string path = mod.dir + "/" + mod.version;
                string[] files = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    Console.WriteLine("reading " + file);
                    defs.AddRange(readXML(mod, file));
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            return defs;
        }

        public List<string> readModConfig()
        {
            List<string> activeMods = new List<string>();
            string path = Environment.ExpandEnvironmentVariables("%USERPROFILE%/Appdata/LocalLow/Ludeon Studios/RimWorld by Ludeon Studios/Config/ModsConfig.xml");
            var doc = new XmlDocument();
            doc.Load(path);
            foreach (XmlNode node in doc.DocumentElement.SelectNodes("/ModsConfigData/activeMods/li"))
                activeMods.Add(node.InnerText);
            return activeMods;
        }

        public string readPackageId(string file)
        {
            string packageId = "-undefined-";
            var doc = new XmlDocument();
            doc.Load(file);
            XmlNode node = doc.DocumentElement.SelectSingleNode("/ModMetaData/packageId");
            if (node != null)
                packageId = node.InnerText.ToLower();
            return packageId;
        }

        public string readModName(string file)
        {
            string modName = "-undefined-";
            var doc = new XmlDocument();
            doc.Load(file);
            XmlNode node = doc.DocumentElement.SelectSingleNode("/ModMetaData/name");
            if (node != null)
                modName = node.InnerText.ToLower();
            return modName;
        }

        private List<Def> readXML(Mod mod, string file)
        {
            List<Def> xmlDefs = new List<Def>();
            string[] orientations = { "_north", "_south", "_west", "_east" };

            var doc = new XmlDocument();
            try
            {
                doc.Load(file);
                foreach (XmlNode node in doc.DocumentElement.SelectNodes("/Defs"))
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        string type = child.Name;
                        int idx = type.IndexOf("Def");
                        if (idx > 0)
                        {
                            if (!defTypes.Contains(type))
                            {
                                defTypes.Add(type);
                            }

                            string defType = type.Substring(0, idx).ToLower();

                            string defName = "";
                            string label = "";
                            string description = "";
                            string texture = "";

                            for (int i = 0; i < child.ChildNodes.Count; i++)
                            {
                                string name = child.ChildNodes[i].Name;
                                if (name == "defName")
                                {
                                    defName = child.ChildNodes[i].InnerText;
                                }
                                if (name == "label")
                                {
                                    label = child.ChildNodes[i].InnerText;
                                }
                                if (name == "description")
                                {
                                    description = child.ChildNodes[i].InnerText;
                                }

                                if (name == "reportString") // Jobs
                                {
                                    description = child.ChildNodes[i].InnerText;
                                }
                                if (name == "baseDesc") // Backstories
                                {
                                    description = child.ChildNodes[i].InnerText;
                                }
                                if (name == "degreeDatas")  // Traits
                                {
                                    XmlNode traitNode = child.SelectSingleNode("degreeDatas/li/description");
                                    if (traitNode != null)
                                    {
                                        description = traitNode.InnerText;
                                    }
                                }
                                if (name == "stages")  // Thoughts
                                {
                                    XmlNodeList thoughtDefs = child.SelectNodes("stages/li/description");
                                    foreach (XmlNode thoughtNode in thoughtDefs)
                                    {
                                        description += thoughtNode.InnerText + "\n\n";
                                    }
                                }
                            }
                            if (description == "") description = "No additional information available";


                            // Texture
                            XmlNode texNode = child.SelectSingleNode("graphicData/texPath");
                            if (texNode != null)
                            {
                                // core textures
                                // https://ludeon.com/forums/index.php?topic=2325

                                string texPath = mod.dir + @"/Textures/" + texNode.InnerText;
                                if (Directory.Exists(texPath))
                                {
                                    string[] files = Directory.GetFiles(texPath, "*.*", SearchOption.AllDirectories);
                                    texture = files[0];
                                }
                                else
                                {
                                    texture = mod.dir + @"/Textures/" + texNode.InnerText + ".png";
                                    if (!File.Exists(texture))
                                    {
                                        //Console.WriteLine(texture + " does not exist");
                                        foreach (string ori in orientations)
                                        {
                                            string textureOri = mod.dir + @"/Textures/" + texNode.InnerText + ori + ".png";
                                            if (File.Exists(textureOri))
                                            {
                                                texture = textureOri;
                                                //Console.WriteLine(texture + " added");
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            Def def = new Def();

                            if (defType == "thing")
                            {
                                ThingDef thing = new ThingDef();

                                // <statBases>
                                XmlNode statsNode = child.SelectSingleNode("statBases");
                                if (statsNode != null)
                                {
                                    foreach (XmlNode stat in statsNode.ChildNodes)
                                    {
                                        thing.details.Add(new string[] { stat.Name, stat.InnerText });
                                    }
                                }
                                def = thing;
                            }

                            if (defType == "recipe")
                            {
                                RecipeDef recipe = new RecipeDef();

                                // <products>
                                string products = "";
                                XmlNodeList productNodes = child.SelectNodes("products");
                                foreach (XmlNode n in productNodes)
                                {
                                    foreach (XmlNode p in n.ChildNodes)
                                    {
                                        products += p.InnerXml + "x " + p.Name;
                                    }
                                }

                                // <researchPrerequisite>
                                string research = "-";
                                XmlNode researchNode = child.SelectSingleNode("researchPrerequisite");
                                if (researchNode != null)
                                {
                                    research = researchNode.InnerText;
                                }
                                recipe.research = research;

                                // <skillRequirements>
                                string skill = "-";
                                XmlNode skillNode = child.SelectSingleNode("skillRequirements");
                                if (skillNode != null)
                                {
                                    skill = skillNode.InnerText;
                                }
                                recipe.skill = skill;

                                // <workAmount>
                                string work = "-";
                                XmlNode workNode = child.SelectSingleNode("workAmount");
                                if (workNode != null)
                                {
                                    work = workNode.InnerText;
                                }
                                recipe.work = work;

                                // <ingredients>
                                foreach (XmlNode n in child.SelectNodes("ingredients/li"))
                                {
                                    string ingredients = "";
                                    foreach (XmlNode xml in n.SelectNodes("filter/thingDefs/li"))
                                    {
                                        ingredients += xml.InnerText + ", ";
                                    }
                                    foreach (XmlNode xml in n.SelectNodes("filter/categories/li"))
                                    {
                                        ingredients += xml.InnerText + ", ";
                                    }
                                    ingredients = ingredients.Substring(0, ingredients.Length - 2);

                                    string amount = n.LastChild.InnerText;

                                    recipe.addIngredients(new string[] { amount, ingredients, products });
                                }
                                def = recipe;
                            }

                            def.mod = mod;
                            def.defType = type;
                            def.defName = defName;
                            def.label = label;
                            def.description = description;
                            def.texture = texture;
                            def.file = file;
                            def.disabled = false;

                            // XML view
                            string xmlOut = System.Xml.Linq.XDocument.Parse(child.OuterXml).ToString();
                            def.xml = xmlOut;

                            if (defName != "")
                            {
                                xmlDefs.Add(def);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            try
            {
                // read defs disabled by comment <!-- -->
                foreach (XmlNode comment in doc.SelectNodes("//comment()"))
                {
                    Console.WriteLine("\ncomment: " + comment.InnerText + "\n");
                    string xml = comment.InnerText;
                    if (!xml.StartsWith("<")) xml = "<" + xml + ">";
                    XmlReader nodeReader = XmlReader.Create(new StringReader(xml));
                    XmlNode newNode = doc.ReadNode(nodeReader);

                    XmlNode defName = newNode.SelectSingleNode("defName");
                    if (defName != null)
                    {
                        Def disabledDef = new Def();
                        disabledDef.mod = mod;
                        disabledDef.defType = newNode.Name;
                        disabledDef.defName = defName.InnerText;
                        disabledDef.label = "(Disabled) ";
                        XmlNode labelNode = newNode.SelectSingleNode("label");
                        if (labelNode != null) disabledDef.label += labelNode.InnerText;
                        disabledDef.file = file;
                        disabledDef.disabled = true;
                        xmlDefs.Add(disabledDef);
                        Console.WriteLine("Disabled definition added.");
                    }
                }
            }
            catch (Exception e) { Console.WriteLine("XMLReader: " + e.StackTrace); }

            return xmlDefs;
        }

        public void disableNode(Def def)
        {
            try
            {
                // backup file
                string backup = def.file + ".ori";
                if (!File.Exists(backup))
                    File.Copy(def.file, backup);
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            try
            {
                // comment out xml
                var doc = new XmlDocument();
                doc.Load(def.file);
                XmlNode node = doc.DocumentElement.SelectSingleNode("ThingDef[defName='" + def.defName + "']");
                if (node != null)
                {
                    XmlComment comment = doc.CreateComment(node.OuterXml);
                    XmlNode parent = node.ParentNode;
                    parent.ReplaceChild(comment, node);
                    doc.Save(def.file);
                    def.disabled = true;
                    Console.WriteLine("'" + def.defName + "' disabled.");
                }
                else
                {
                    Console.WriteLine("'" + def.defName + "' not found.");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        public void enableNode(Def def)
        {
            var doc = new XmlDocument();
            doc.Load(def.file);
            foreach (XmlNode comment in doc.SelectNodes("//comment()"))
            {
                string xml = comment.InnerText;
                if (!xml.StartsWith("<")) xml = "<" + xml + ">";
                XmlReader nodeReader = XmlReader.Create(new StringReader(xml));
                XmlNode newNode = doc.ReadNode(nodeReader);
                Console.WriteLine("enable: " + newNode.OuterXml);

                XmlNode defName = newNode.SelectSingleNode("defName");
                if (defName != null && defName.InnerText == def.defName)
                {
                    XmlNode parent = comment.ParentNode;
                    parent.ReplaceChild(newNode, comment);
                    doc.Save(def.file);
                    def.disabled = false;
                    Console.WriteLine("'" + def.defName + "' enabled.");
                    break;
                }
            }
        }

    }
}

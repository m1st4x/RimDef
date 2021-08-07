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

        public List<Def> loadAllDefs(string mod, string dir)
        {
            Console.WriteLine("loading mod: " + mod);

            defTypes = new List<string>();

            List<Def> defs = new List<Def>();

            // NOTE: The contents of a Def folder don't follow a clear naming convention, 
            // but the folder names are generally the same in every mod.
            // see https://rimworldwiki.com/wiki/Modding_Tutorials/Mod_folder_structure
            try
            {
                string[] files = Directory.GetFiles(dir, "*.xml", SearchOption.AllDirectories);
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

        private List<Def> readXML(string mod, string file)
        {
            List<Def> xmlDefs = new List<Def>();
            try
            {
                var doc = new XmlDocument();
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
                            }

                            // Texture
                            XmlNode texNode = child.SelectSingleNode("graphicData/texPath");
                            if (texNode != null)
                            {
                                // core textures
                                // https://ludeon.com/forums/index.php?topic=2325

                                string texPath = modDir + @"/" + mod + @"/Textures/" + texNode.InnerText;
                                if (Directory.Exists(texPath))
                                {
                                    string[] files = Directory.GetFiles(texPath, "*.*", SearchOption.AllDirectories);
                                    texture = files[0];
                                }
                                else
                                {
                                    texture = modDir + @"/" + mod + @"/Textures/" + texNode.InnerText + ".png";
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

                            def.modName = mod;
                            def.defType = type;
                            def.defName = defName;
                            def.label = label;
                            def.description = description;
                            def.texture = texture;

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

            return xmlDefs;
        }
    }
}

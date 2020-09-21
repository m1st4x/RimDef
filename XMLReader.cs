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

        public List<Def> loadAllDefs(string mod)
        {
            Console.WriteLine("loading mod " + mod);

            defTypes = new List<string>();

            List<Def> defs = new List<Def>();

            List<string> directories = new List<string>();

            // consider version subdirs
            string dir1 = modDir + @"/" + mod + @"/Defs/";
            string dir2 = modDir + @"/" + mod + @"/1.1/Defs/";
            string dir3 = modDir + @"/" + mod + @"/v1.1/Defs/";
            string dir4 = modDir + @"/" + mod + @"/1.2/Defs/";
            string[] dirs = { dir1, dir2, dir3, dir4 };
            foreach (string dir in dirs)
            {
                if (Directory.Exists(dir))
                {
                    foreach (string path in Directory.GetDirectories(dir))
                    {
                        directories.Add(path);
                    }
                    break;
                }
            }
            //Console.WriteLine(directories.Count + " subdirs found");

            foreach (string dir in directories)
            {
                // NOTE: The contents of a Def folder don't follow a clear naming convention, but the folder names are generally the same in every mod.
                // see https://rimworldwiki.com/wiki/Modding_Tutorials/Mod_folder_structure
                try
                {
                    string[] files = Directory.GetFiles(dir, "*.xml", SearchOption.AllDirectories);
                    foreach (string filename in files)
                    {
                        Console.WriteLine("reading " + filename);
                        defs.AddRange(readXML(filename, mod));
                    }
                }
                catch (Exception) { }
            }

            return defs;
        }

        private List<Def> readXML(string filename, string mod)
        {
            List<Def> xmlDefs = new List<Def>();
            try
            {
                var doc = new XmlDocument();
                doc.Load(filename);
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
                                if (name == "graphicData")
                                {
                                    XmlNodeList graphicData = child.ChildNodes[i].ChildNodes;
                                    for (int j = 0; j < graphicData.Count; j++)
                                    {
                                        if (graphicData[j].Name == "texPath")
                                        {
                                            string texPath = modDir + @"/" + mod + @"/Textures/" + graphicData[j].InnerText;
                                            if (Directory.Exists(texPath))
                                            {
                                                string[] files = Directory.GetFiles(texPath, "*.*", SearchOption.AllDirectories);
                                                texture = files[0];
                                            }
                                            else
                                            {
                                                texture = modDir + @"/" + mod + @"/Textures/" + graphicData[j].InnerText + ".png";
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
                                        Console.WriteLine(stat.Name + ": " + stat.InnerText);
                                        thing.details.Add(new string[] { stat.Name, stat.InnerText });
                                    }
                                }
                                def = thing;
                            }

                            if (defType == "recipe")
                            {
                                RecipeDef recipe = new RecipeDef();
                                Console.WriteLine("Recipe: " + label);

                                // <products>
                                string products = "";
                                XmlNodeList productNodes = child.SelectNodes("products");
                                foreach (XmlNode n in productNodes)
                                {
                                    foreach (XmlNode p in n.ChildNodes)
                                    {
                                        Console.WriteLine(p.Name + " # " + p.InnerXml);
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

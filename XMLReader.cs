using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace RimDef
{
    class XMLReader
    {
        public string appdataPath = "%USERPROFILE%\\Appdata\\LocalLow\\Ludeon Studios\\RimWorld by Ludeon Studios\\Config\\ModsConfig.xml";

        public List<string> defTypes;

        public List<Def> loadAllPatches(Mod mod)
        {
            List<Def> patches = new List<Def>();
            if (Directory.Exists(mod.patchPath))
            {
                foreach (string file in Directory.GetFiles(mod.patchPath, "*.xml", SearchOption.AllDirectories))
                {
                    try
                    {
                        Console.WriteLine("reading " + file);
                        var doc = new XmlDocument();
                        doc.Load(file);

                        // <Operation>
                        string opClass = "unset";
                        XmlNodeList opNodes = doc.DocumentElement.SelectNodes("/Patch/Operation");
                        foreach (XmlNode opNode in opNodes)
                        {
                            if (opNode.Attributes != null && opNode.Attributes.Count > 0)
                            {
                                opClass = opNode.Attributes[0].Value;
                                Console.WriteLine("opClass=" + opClass);

                                ////////////////////////////////////////////////////////////////////
                                // <Operation Class="PatchOperationFindMod">
                                //
                                if (opClass == "PatchOperationFindMod")
                                {
                                    // mods
                                    List<string> patchMods = new List<string>();
                                    XmlNodeList nodeList = doc.DocumentElement.SelectNodes("/Patch/Operation/mods/li");
                                    foreach (XmlNode li in nodeList)
                                    {
                                        patchMods = new List<string>();
                                        foreach (XmlNode node in nodeList)
                                        {
                                            patchMods.Add(node.InnerText);
                                            Console.WriteLine($"mods={node.InnerText}");
                                        }
                                    }

                                    // match
                                    XmlNode match = doc.DocumentElement.SelectSingleNode("/Patch/Operation/match");
                                    if (match != null)
                                    {
                                        string matchClass = "unset";
                                        if (match.Attributes != null && match.Attributes.Count > 0)
                                        {
                                            matchClass = match.Attributes["Class"].Value;
                                            Console.WriteLine("Class=" + matchClass);
                                        }

                                        if (matchClass == "PatchOperationAdd")
                                        {
                                            //TODO
                                        }

                                        if (matchClass == "PatchOperationSequence")
                                        {
                                            // sequence
                                            foreach (XmlNode li in match.SelectNodes("operations/li"))
                                            {
                                                // type
                                                string liClass = "unset";
                                                if (li.Attributes != null && li.Attributes.Count > 0)
                                                {
                                                    liClass = li.Attributes["Class"].Value;
                                                    Console.WriteLine("liClass=" + liClass);
                                                }

                                                string xpath = "unset";
                                                string value = "unset";
                                                foreach (XmlNode child in li.ChildNodes)
                                                {
                                                    if (child.Name == "xpath") xpath = child.InnerText;
                                                    if (child.Name == "value") value = child.InnerText;
                                                }
                                                Console.WriteLine("xpath=" + xpath);
                                                Console.WriteLine("value=" + value);

                                                // name
                                                string name = "unset";
                                                try
                                                {
                                                    int start = xpath.IndexOf("\"");
                                                    int end = xpath.LastIndexOf("\"");
                                                    name = xpath.Substring(start + 1, end - start - 1);
                                                    Console.WriteLine("target=" + name);
                                                }
                                                catch (Exception ex) { Console.WriteLine(ex.Message); }

                                                Patch patch = new Patch();
                                                patch.defType = matchClass;
                                                patch.defName = name;
                                                patch.mod = mod;
                                                patch.mods = patchMods;
                                                patch.file = file;
                                                patch.enabled = true;
                                                patch.xml = System.Xml.Linq.XDocument.Parse(match.OuterXml).ToString();

                                                patch.patchType = liClass;
                                                patch.xpath = xpath;
                                                patch.value = value;

                                                patches.Add(patch);

                                                Console.WriteLine("-----------------------");
                                            }
                                        }
                                    }
                                }

                                ////////////////////////////////////////////////////////////////////
                                // <Operation Class="PatchOperationAdd">
                                //
                                if (opClass == "PatchOperationAdd")
                                {
                                    string xpath = "unset";
                                    string value = "unset";
                                    foreach (XmlNode child in opNode.ChildNodes)
                                    {
                                        if (child.Name == "xpath") xpath = child.InnerText;
                                        if (child.Name == "value") value = child.InnerText;
                                    }
                                    Console.WriteLine("xpath=" + xpath);
                                    Console.WriteLine("value=" + value);

                                    Patch patch = new Patch();
                                    patch.defType = "Patch";
                                    patch.defName = "PatchOperationAdd";

                                    patch.mod = mod;
                                    patch.file = file;
                                    patch.enabled = true;
                                    patch.xml = System.Xml.Linq.XDocument.Parse(opNode.OuterXml).ToString();

                                    patch.patchType = opClass;
                                    patch.xpath = xpath;
                                    patch.value = value;

                                    patches.Add(patch);

                                    Console.WriteLine("-----------------------");
                                }

                                ////////////////////////////////////////////////////////////////////
                                // <Operation Class="PatchOperationConditional">
                                //
                                if (opClass == "PatchOperationConditional")
                                {
                                    Patch patch = new Patch();
                                    patch.defType = "Patch";
                                    patch.defName = "PatchOperationConditional";
                                    patch.patchType = opClass;

                                    foreach (XmlNode child in opNode.ChildNodes)
                                    {
                                        if (child.Name == "xpath") patch.xpath = child.InnerText;
                                        if (child.Name == "value") patch.value = child.InnerText;

                                        //TODO
                                        if (child.Name == "match")
                                        {
                                            if (child.Attributes != null && child.Attributes.Count > 0)
                                            {
                                                string matchClass = child.Attributes[0].Value;
                                            }
                                        }
                                        if (child.Name == "nomatch")
                                        {
                                            if (child.Attributes != null && child.Attributes.Count > 0)
                                            {
                                                string nomatchClass = child.Attributes[0].Value;
                                            }
                                        }
                                    }

                                    patch.mod = mod;
                                    patch.file = file;
                                    patch.enabled = true;
                                    patch.xml = System.Xml.Linq.XDocument.Parse(opNode.OuterXml).ToString();
                                    patches.Add(patch);

                                    Console.WriteLine("-----------------------");
                                }
                            }
                        }
                    }
                    catch (Exception e) { Console.WriteLine(e); }
                }
            }

            return patches;
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
                string path = mod.dir + "\\" + mod.version;
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
            try
            {
                string path = Environment.ExpandEnvironmentVariables(appdataPath);
                var doc = new XmlDocument();
                doc.Load(path);
                foreach (XmlNode node in doc.DocumentElement.SelectNodes("/ModsConfigData/activeMods/li"))
                    activeMods.Add(node.InnerText);
            }
            catch (Exception e) { Console.WriteLine(e); }

            return activeMods;
        }

        public string readPackageId(string file)
        {
            string packageId = "-undef-";
            try
            {
                var doc = new XmlDocument();
                doc.Load(file);
                XmlNode node = doc.DocumentElement.SelectSingleNode("/ModMetaData/packageId");
                if (node != null)
                    packageId = node.InnerText.ToLower();
            }
            catch (Exception e) { Console.WriteLine(e); }

            return packageId;
        }

        public string readModName(string file)
        {
            string modName = "-undef-";
            try
            {
                var doc = new XmlDocument();
                doc.Load(file);
                XmlNode node = doc.DocumentElement.SelectSingleNode("/ModMetaData/name");
                if (node != null)
                    modName = node.InnerText.ToLower();
            }
            catch (Exception e) { Console.WriteLine(e); }

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


                            // Textures
                            XmlNode texNode = child.SelectSingleNode("graphicData/texPath");
                            if (texNode != null)
                            {
                                // core textures
                                // https://ludeon.com/forums/index.php?topic=2325

                                string texPath = mod.dir + @"\\Textures\\" + texNode.InnerText;
                                if (Directory.Exists(texPath))
                                {
                                    string[] files = Directory.GetFiles(texPath, "*.*", SearchOption.AllDirectories);
                                    texture = files[0];
                                }
                                else
                                {
                                    texture = mod.dir + @"\\Textures\\" + texNode.InnerText + ".png";
                                    if (!File.Exists(texture))
                                    {
                                        //Console.WriteLine(texture + " does not exist");
                                        foreach (string ori in orientations)
                                        {
                                            string textureOri = mod.dir + @"\\Textures\\" + texNode.InnerText + ori + ".png";
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
                            def.enabled = true;

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
                        disabledDef.enabled = false;
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
                    def.enabled = false;
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
                    def.enabled = true;
                    Console.WriteLine("'" + def.defName + "' enabled.");
                    break;
                }
            }
        }

    }
}

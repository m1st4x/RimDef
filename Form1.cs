using SimpleSearch;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RimDef
{
    public partial class Form1 : Form
    {
        XMLReader xmlReader = new XMLReader();

        List<Def> defs = new List<Def>();
        List<Def> defsView = new List<Def>();

        private ListView lwDetails = new ListView();

        public Form1()
        {
            InitializeComponent();

            txtModDir.Text = @"C:\Games\RimWorld";

            lwRecipe.Columns.Add("amount", 50);
            lwRecipe.Columns.Add("ingredient", 200);
            lwRecipe.Columns.Add("products", 100);

            // 
            // lwDetail
            // 
            this.lwDetails.GridLines = true;
            this.lwDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lwDetails.HideSelection = false;
            this.lwDetails.Location = new System.Drawing.Point(440, 440);
            this.lwDetails.Name = "lwDetail";
            //this.lwDetails.Size = new System.Drawing.Size(360, 60);
            this.lwDetails.TabIndex = 4;
            this.lwDetails.UseCompatibleStateImageBehavior = false;
            this.lwDetails.View = System.Windows.Forms.View.Details;
            this.lwDetails.Visible = false;

            this.lwDetails.Columns.Add("key", 150);
            this.lwDetails.Columns.Add("value", 150);

            this.Controls.Add(this.lwDetails);
        }

        private void loadModList(string rimDir)
        {
            defs.Clear();
            lbMods.Items.Clear();
            lbDefTypes.DataSource = null;
            lwDefs.Items.Clear();
            xmlView.Clear();
            gbDesc.Visible = false;
            gbRecipe.Visible = false;
            pictureBox1.Visible = false;

            List<Mod> modVersions;
            string[] versionNames = { "1.0", "1.1", "1.1-1.2", "1.2", "1.3" };

            try
            {
                List<string> activeMods = xmlReader.readModConfig();

                if (rimDir.Contains("294100")) // steam version
                {
                    //TODO core defs (since rw 1.1)

                    xmlReader.modDir = rimDir;

                    foreach (string dir in Directory.GetDirectories(rimDir))
                    {
                        //Dictionary<string, string> defdirsTmp = new Dictionary<string, string>();
                        //Tuple<string, string> latest = null;

                        if (cbOnlyActiveMods.Checked)
                        {
                            string packageId = xmlReader.readPackageId(dir + @"/About/About.xml");
                            if (!activeMods.Contains(packageId)) continue;
                        }
                        string modName = xmlReader.readModName(dir + @"/About/About.xml");

                        Mod mod = new Mod(modName);
                        mod.defPath = dir + @"/Defs/";
                        lbMods.Items.Add(mod);

                        /* TODO
                        foreach (string ver in versionNames)
                        {
                            string defPath = dir + "/" + ver + @"/Defs/";
                            if (Directory.Exists(defPath))
                            {
                                latest = new Tuple<string, string>(modName + "*" + ver, defPath);
                                defdirsTmp.Add(latest.Item1, latest.Item2);
                            }
                        }
                        */
                    }
                }
                else // non-steam version
                {
                    // Core defs directory (since rw 1.1)
                    Mod core = new Mod("Core");
                    core.dir = rimDir + @"/Data/Core/";
                    core.defPath = rimDir + @"/Data/Core/Defs/";
                    lbMods.Items.Add(core);

                    string modDir = rimDir + @"/Mods/";
                    xmlReader.modDir = modDir;

                    foreach (string dir in Directory.GetDirectories(modDir))
                    {
                        modVersions = new List<Mod>();
                        Mod latest = null;

                        string packageId = xmlReader.readPackageId(dir + @"/About/About.xml");
                        if (cbOnlyActiveMods.Checked)
                        {
                            if (!activeMods.Contains(packageId)) continue;
                        }

                        string modName = xmlReader.readModName(dir + @"/About/About.xml");

                        string path = dir + @"/Defs/";
                        if (Directory.Exists(path))
                        {
                            Mod mod = new Mod(modName);
                            mod.packageId = packageId;
                            mod.dir = dir;
                            mod.defPath = path;
                            modVersions.Add(mod);
                            latest = mod;
                        }

                        foreach (string ver in versionNames)
                        {
                            path = dir + "/" + ver + @"/Defs/";
                            if (Directory.Exists(path))
                            {
                                Mod mod = new Mod(modName + "*" + ver);
                                mod.packageId = packageId;
                                mod.version = ver;
                                mod.dir = dir;
                                mod.defPath = path;
                                modVersions.Add(mod);
                                latest = mod;
                            }
                        }

                        if (cbLatestVersion.Checked && latest != null)
                        {
                            lbMods.Items.Add(latest);
                        }
                        else
                        {
                            foreach (Mod m in modVersions)
                            {
                                lbMods.Items.Add(m);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Error loading modlist: " + ex); }
        }

        private void lbMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            // reading all defs from selected mod
            Mod mod = (Mod)lbMods.SelectedItem;
            defs = xmlReader.loadAllDefs(mod);
            defsView = defs;

            lwDefs.Items.Clear();
            lwDefs.Columns.Clear();
            lbDefTypes.DataSource = null;
            xmlView.Clear();
            gbDesc.Visible = false;
            gbRecipe.Visible = false;
            pictureBox1.Visible = false;
            lwDetails.Items.Clear();
            lwRecipe.Items.Clear();

            lwDefs.Columns.Add("Type", 100);
            lwDefs.Columns.Add("Name", 120);
            lwDefs.Columns.Add("Label", 150);

            //xmlReader.defTypes.Sort();
            lbDefTypes.DataSource = xmlReader.defTypes;
        }

        // Filter defs from loaded mod ListBox
        private void lbDefTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbDefTypes.SelectedIndices.Count > 0)
            {
                string selectedType = xmlReader.defTypes[lbDefTypes.SelectedIndices[0]];
                lwDefs.Items.Clear();

                defsView = new List<Def>();
                foreach (Def def in defs)
                {
                    if (def.defType == selectedType)
                    {
                        defsView.Add(def);
                        string[] items = { def.defType, def.defName, def.label };
                        var listViewItem = new ListViewItem(items);
                        lwDefs.Items.Add(listViewItem);
                    }
                }
            }
        }

        private void lwDefs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lwDefs.SelectedIndices.Count > 0)
            {
                Def def = defsView[lwDefs.SelectedIndices[0]];

                gbRecipe.Visible = false;
                gbDesc.Visible = false;
                pictureBox1.Visible = false;
                lwDetails.Visible = false;
                cbDisable.Visible = false;

                lblPath.Text = def.file.Substring(def.file.IndexOf("/1."));
                xmlView.Text = def.xml;

                if (def.defType.ToLower() == "recipedef")
                {
                    RecipeDef recipe = (RecipeDef)def;

                    lwRecipe.Items.Clear();

                    foreach (string[] li in recipe.ingredients)
                    {
                        lwRecipe.Items.Add(new ListViewItem(li));
                    }

                    lwDetails.Items.Clear();
                    lwDetails.Items.Add(new ListViewItem(new string[] { "Work amount", recipe.work }));
                    lwDetails.Items.Add(new ListViewItem(new string[] { "Skill requirements", recipe.skill }));
                    lwDetails.Items.Add(new ListViewItem(new string[] { "Research prerequisite", recipe.research }));

                    lwDetails.Size = new System.Drawing.Size(360, 60);
                    lwDetails.Visible = true;
                    gbRecipe.Visible = true;
                }

                if (def.defType.ToLower() == "thingdef")
                {
                    // Details
                    lwDetails.Items.Clear();
                    foreach (string[] row in def.details)
                    {
                        lwDetails.Items.Add(new ListViewItem(row));
                    }
                    if (lwDetails.Items.Count > 0)
                    {
                        lwDetails.Size = new System.Drawing.Size(360, 110);
                        lwDetails.Visible = true;
                    }

                    // Texture
                    Console.WriteLine("texture path = " + def.texture);
                    Bitmap image = new Bitmap(RimDef.Properties.Resources.nopic);
                    if (File.Exists(def.texture))
                    {
                        try
                        {
                            image = new Bitmap(def.texture);
                        }
                        catch (Exception ex) { Console.WriteLine(ex); }
                    }
                    pictureBox1.Image = (Image)image;
                    pictureBox1.Visible = true;
                    pictureBox1.Refresh();

                    cbDisable.Visible = true;
                    cbDisable.Checked = def.disabled;
                }

                // Description
                if (def.description != "")
                {
                    thingDesc.Text = def.description;
                    gbDesc.Visible = true;
                }
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtModDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadModList(txtModDir.Text);
        }

        private SearchCore SearchCore { get; set; }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var searchers = new List<Searcher> { new DefSearcher(defs) };
            SearchCore = new SearchCore(searchers);

            lwDefs.Items.Clear();
            lwDefs.Columns.Clear();
            lwDefs.Columns.Add("Mod", 150);
            lwDefs.Columns.Add("Type", 150);
            lwDefs.Columns.Add("Name", 150);
            lwDefs.Columns.Add("Label", 150);

            string searchText = txtSearch.Text;
            Console.WriteLine(searchText);

            var model = new SearchResponse();
            var s = new System.Diagnostics.Stopwatch();
            s.Start();
            model.Results = SearchCore.Search(searchText);
            s.Stop();
            model.TimeTaken = s.Elapsed;

            defsView.Clear();

            foreach (SearchResult result in model.Results)
            {
                Def def = result.Definition;
                string[] items = { def.mod.name, def.defType, def.defName, def.label };
                var listViewItem = new ListViewItem(items);
                lwDefs.Items.Add(listViewItem);
                defsView.Add(def);
            }
        }

        private void cbDisable_CheckedChanged(object sender, EventArgs e)
        {
            Def def = defsView[lwDefs.SelectedIndices[0]];
            if (cbDisable.Checked)
            {
                Console.WriteLine(def.file);
                xmlReader.disableNode(def);
            }
            else
                xmlReader.enableNode(def);
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

    }
}

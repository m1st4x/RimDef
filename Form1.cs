using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SimpleSearch;

namespace RimDef
{
    public partial class Form1 : Form
    {
        XMLReader xmlReader = new XMLReader();

        List<Def> defs = new List<Def>();
        List<Def> defsView = new List<Def>();

        Dictionary<string, string> defdirs;

        private ListView lwDetails = new ListView();

        public Form1()
        {
            InitializeComponent();

            txtModDir.Text = @"C:\Games\RimWorld Royalty";

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
            lbDefTypes.Items.Clear();
            lwDefs.Items.Clear();
            xmlView.Clear();
            gbDesc.Visible = false;
            gbRecipe.Visible = false;
            pictureBox1.Visible = false;

            defdirs = new Dictionary<string, string>();
            string modDir = rimDir + "/Mods";
            xmlReader.modDir = modDir;

            // Core defs directory (since rw 1.1)
            defdirs.Add("Core", rimDir + "/Data/Core/Defs");
            lbMods.Items.Add("Core");

            try
            {
                List<string> activeMods = xmlReader.readModConfig();

                foreach (string dir in Directory.GetDirectories(modDir))
                {
                    if (cbOnlyActiveMods.Checked)
                    {
                        string packageId = xmlReader.readPackageId(dir + @"/About/About.xml");
                        if (!activeMods.Contains(packageId)) continue;
                    }

                    string[] split = dir.Split('\\');
                    string name = split[split.Length - 1];

                    Dictionary<string, string> defdirsTmp = new Dictionary<string, string>();
                    Tuple<string, string> latest = null;

                    string path = dir + @"/Defs/";
                    if (Directory.Exists(path))
                    {
                        latest = new Tuple<string, string>(name, path);
                        defdirsTmp.Add(name, path);
                    }

                    string[] versions = { "1.0", "1.1", "1.1-1.2", "1.2" };
                    foreach (string ver in versions)
                    {
                        path = dir + "/" + ver + @"/Defs/";
                        if (Directory.Exists(path))
                        {
                            latest = new Tuple<string, string>(name + "*" + ver, path);
                            defdirsTmp.Add(latest.Item1, latest.Item2);
                        }
                    }

                    if (cbLatestVersion.Checked && latest != null)
                    {
                        defdirs.Add(latest.Item1, latest.Item2);
                        lbMods.Items.Add(latest.Item1);
                    }
                    else
                    {
                        foreach (KeyValuePair<string, string> entry in defdirsTmp)
                        {
                            defdirs.Add(entry.Key, entry.Value);
                            lbMods.Items.Add(entry.Key);
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Error loading modlist: " + ex); }
        }

        private void lbMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mod = defdirs.ElementAt(lbMods.SelectedIndex).Key;
            string moddir = mod.Split('*')[0]; // remove version string
            string path = defdirs.ElementAt(lbMods.SelectedIndex).Value;

            // reading all defs from selected mod
            defs = xmlReader.loadAllDefs(moddir, path);
            defsView = defs;

            lwDefs.Items.Clear();
            lwDefs.Columns.Clear();
            lbDefTypes.Items.Clear();
            xmlView.Clear();
            gbDesc.Visible = false;
            gbRecipe.Visible = false;
            pictureBox1.Visible = false;
            lwDetails.Items.Clear();
            lwRecipe.Items.Clear();

            lwDefs.Columns.Add("Type", 100);
            lwDefs.Columns.Add("Name", 120);
            lwDefs.Columns.Add("Label", 150);

            foreach (Def def in defs)
            {
                var listViewItem = new ListViewItem(new string[] { def.defType, def.defName, def.label });
                listViewItem.ToolTipText = "tooltip test";
                lwDefs.Items.Add(listViewItem);
            }
            
            foreach (string item in xmlReader.defTypes)
            {
                lbDefTypes.Items.Add(item);
            }
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

                xmlView.Text = def.xml;

                if (def.defType.ToLower() == "recipedef")
                {
                    RecipeDef recipe = (RecipeDef) def;

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
                string[] items = { def.modName, def.defType, def.defName, def.label };
                var listViewItem = new ListViewItem(items);
                lwDefs.Items.Add(listViewItem);
                defsView.Add(def);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

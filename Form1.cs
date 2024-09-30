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

        private List<Def> patches = new List<Def>();
        private List<string> patchesView = new List<string>();

        private ListView lwDetails = new ListView();

        string[] versions = { "1.0", "1.1", "1.2", "1.3", "1.4", "1.5" };
        string[] vanilla = { "Core", "Royalty", "Ideology", "Anomaly", "Biotech" };

        public Form1()
        {
            InitializeComponent();

            txtModDir.Text = @"C:\Users\Ralf\Desktop\Rimworld";
            //txtModDir.Text = @"C:\Games\Rimworld";

            cbVersion.DataSource = versions;
            cbVersion.SelectedIndex = versions.Length - 1;

            lwRecipe.Columns.Add("amount", 50);
            lwRecipe.Columns.Add("ingredient", 200);
            lwRecipe.Columns.Add("products", 100);

            lwDetails.GridLines = true;
            lwDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            lwDetails.HideSelection = false;
            lwDetails.Location = new System.Drawing.Point(20, 440);
            lwDetails.Name = "lwDetail";
            //this.lwDetails.Size = new System.Drawing.Size(360, 60);
            lwDetails.Scrollable = true;
            lwDetails.TabIndex = 4;
            lwDetails.UseCompatibleStateImageBehavior = false;
            lwDetails.View = System.Windows.Forms.View.Details;
            lwDetails.Visible = false;

            lwDetails.Columns.Add("key", 150);
            lwDetails.Columns.Add("value", 150);

            Controls.Add(this.lwDetails);
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

        private void loadModList(string rimDir)
        {
            defs.Clear();
            lbMods.Items.Clear();
            lbDefTypes.DataSource = null;
            lbPatches.DataSource = null;
            lwDefs.Items.Clear();
            xmlView.Clear();
            gbDesc.Visible = false;
            gbRecipe.Visible = false;
            pictureBox1.Visible = false;

            List<string> activeMods = xmlReader.readModConfig();

            //TODO steam mods in different directory?
            //if (rimDir.Contains("294100")) // steam id

            // vanilla content
            foreach (string content in vanilla)
            {
                Mod dlc = new Mod(content);
                dlc.dir = rimDir + @"\Data\" + content + @"\";
                dlc.defPath = rimDir + @"\Data\" + content + @"\Defs\";
                lbMods.Items.Add(dlc);
            }

            // mod content
            string modDir = rimDir + @"\Mods\";
            foreach (string dir in Directory.GetDirectories(modDir))
            {
                try
                {
                    string packageId = xmlReader.readPackageId(dir + @"\About\About.xml");
                    if (cbOnlyActiveMods.Checked)
                    {
                        if (!activeMods.Contains(packageId)) continue;
                    }

                    string modName = xmlReader.readModName(dir + @"\About\About.xml");
                    Mod mod = new Mod(modName);
                    mod.packageId = packageId;
                    mod.dir = dir;

                    // Defs dir
                    string ver = versions[cbVersion.SelectedIndex];
                    string path = dir + @"\" + ver + @"\Defs\";
                    if (Directory.Exists(path))
                    {
                        mod.name = ver + " " + modName;
                        mod.version = ver;
                        mod.defPath = path;
                    }
                    else
                    {
                        path = dir + @"\Defs\";
                        if (Directory.Exists(path))
                        {
                            mod.defPath = path;
                        }
                    }

                    // Patch dir
                    path = dir + @"\" + ver + @"\Patches\";
                    if (Directory.Exists(path))
                    {
                        mod.patchPath = path;
                    }
                    else
                    {
                        path = dir + @"\Patches\";
                        if (Directory.Exists(path))
                        {
                            mod.patchPath = path;
                        }
                    }

                    lbMods.Items.Add(mod);
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
        }

        private void lbMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            // read def types
            Mod mod = (Mod)lbMods.SelectedItem;
            defs = defsView = xmlReader.loadAllDefs(mod);
            //xmlReader.defTypes.Sort();
            lbDefTypes.DataSource = null;
            lbDefTypes.DataSource = xmlReader.defTypes;

            // read patches
            patches = xmlReader.loadAllPatches(mod);
            patchesView = new List<string>();
            foreach (Def def in patches) {
                patchesView.Add(def.defName);
            }
            lbPatches.DataSource = null;
            lbPatches.DataSource = patchesView;
            if (lbPatches.Items.Count > 0)
                lbPatches.SetSelected(0, false);

            // reset form
            lwDefs.Items.Clear();
            lwDefs.Columns.Clear();
            lwDefs.Columns.Add("Name", 150);
            lwDefs.Columns.Add("Label", 300);
            lwDetails.Items.Clear();
            lwRecipe.Items.Clear();
            xmlView.Clear();
            gbDesc.Visible = false;
            gbRecipe.Visible = false;
            pictureBox1.Visible = false;
        }

        private void lbPatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPatches.SelectedIndices.Count > 0)
            {
                string selectedName = patchesView[lbPatches.SelectedIndices[0]];
                foreach (Patch patch in patches)
                {
                    if (patch.defName == selectedName)
                    {
                        xmlView.Text = patch.xml;
                        //lblPath.Text = patch.file;
                        lblXPath.Text = "XPath: " + patch.xpath;
                        lblXmlPath.Text = patch.file;
                    }
                }
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
                        string[] items = { def.defName, def.label };
                        var listViewItem = new ListViewItem(items);
                        lwDefs.Items.Add(listViewItem);
                    }
                }
                xmlView.Clear();
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

                btnDisable.Enabled = def.enabled;
                btnEnable.Enabled = !btnDisable.Enabled;

                // trim path
                string path = def.file;
                int i = path.IndexOf(@"\1.");
                if (i > -1) path = def.file.Substring(i);
                lblXmlPath.Text = path;

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

                    // Textures
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

            var search = new SearchResponse();
            var time = new System.Diagnostics.Stopwatch();
            time.Start();
            search.Results = SearchCore.Search(searchText);
            time.Stop();
            search.TimeTaken = time.Elapsed;

            defsView.Clear();

            foreach (SearchResult result in search.Results)
            {
                Def def = result.Definition;
                string[] items = { def.mod.name, def.defType, def.defName, def.label };
                var listViewItem = new ListViewItem(items);
                lwDefs.Items.Add(listViewItem);
                defsView.Add(def);
            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            if (lwDefs.SelectedIndices.Count > 0)
            {
                Def def = defsView[lwDefs.SelectedIndices[0]];
                if (!def.enabled)
                {
                    var confirm = MessageBox.Show("Are you sure to disable this item ??", "Confirm!", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        xmlReader.disableNode(def);
                        def.enabled = false;
                        btnDisable.Enabled = false;
                        btnEnable.Enabled = !btnDisable.Enabled;
                    }
                }
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            if (lwDefs.SelectedIndices.Count > 0)
            {
                Def def = defsView[lwDefs.SelectedIndices[0]];
                if (def.enabled)
                {
                    var confirm = MessageBox.Show("Are you sure to enable this item ??", "Confirm!", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        xmlReader.enableNode(def);
                        def.enabled = true;
                        btnDisable.Enabled = true;
                        btnEnable.Enabled = !btnDisable.Enabled;
                    }
                }
            }
        }

    }
}

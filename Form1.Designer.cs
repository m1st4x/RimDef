namespace RimDef
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbMods = new System.Windows.Forms.ListBox();
            this.lbDefTypes = new System.Windows.Forms.ListBox();
            this.lwDefs = new System.Windows.Forms.ListView();
            this.lwRecipe = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnFolder = new System.Windows.Forms.Button();
            this.txtModDir = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.thingDesc = new System.Windows.Forms.TextBox();
            this.xmlView = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gbDesc = new System.Windows.Forms.GroupBox();
            this.gbRecipe = new System.Windows.Forms.GroupBox();
            this.cbOnlyActiveMods = new System.Windows.Forms.CheckBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbPatches = new System.Windows.Forms.ListBox();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.btnDisable = new System.Windows.Forms.Button();
            this.btnEnable = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbDesc.SuspendLayout();
            this.gbRecipe.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMods
            // 
            this.lbMods.FormattingEnabled = true;
            this.lbMods.ItemHeight = 16;
            this.lbMods.Location = new System.Drawing.Point(25, 123);
            this.lbMods.Margin = new System.Windows.Forms.Padding(4);
            this.lbMods.Name = "lbMods";
            this.lbMods.Size = new System.Drawing.Size(241, 228);
            this.lbMods.TabIndex = 0;
            this.lbMods.SelectedIndexChanged += new System.EventHandler(this.lbMods_SelectedIndexChanged);
            // 
            // lbDefTypes
            // 
            this.lbDefTypes.FormattingEnabled = true;
            this.lbDefTypes.ItemHeight = 16;
            this.lbDefTypes.Location = new System.Drawing.Point(295, 123);
            this.lbDefTypes.Margin = new System.Windows.Forms.Padding(4);
            this.lbDefTypes.Name = "lbDefTypes";
            this.lbDefTypes.Size = new System.Drawing.Size(161, 228);
            this.lbDefTypes.TabIndex = 1;
            this.lbDefTypes.SelectedIndexChanged += new System.EventHandler(this.lbDefTypes_SelectedIndexChanged);
            // 
            // lwDefs
            // 
            this.lwDefs.FullRowSelect = true;
            this.lwDefs.GridLines = true;
            this.lwDefs.HideSelection = false;
            this.lwDefs.Location = new System.Drawing.Point(755, 123);
            this.lwDefs.Margin = new System.Windows.Forms.Padding(4);
            this.lwDefs.Name = "lwDefs";
            this.lwDefs.Size = new System.Drawing.Size(530, 228);
            this.lwDefs.TabIndex = 2;
            this.lwDefs.UseCompatibleStateImageBehavior = false;
            this.lwDefs.View = System.Windows.Forms.View.Details;
            this.lwDefs.SelectedIndexChanged += new System.EventHandler(this.lwDefs_SelectedIndexChanged);
            // 
            // lwRecipe
            // 
            this.lwRecipe.GridLines = true;
            this.lwRecipe.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lwRecipe.HideSelection = false;
            this.lwRecipe.Location = new System.Drawing.Point(0, 23);
            this.lwRecipe.Margin = new System.Windows.Forms.Padding(4);
            this.lwRecipe.Name = "lwRecipe";
            this.lwRecipe.Size = new System.Drawing.Size(479, 98);
            this.lwRecipe.TabIndex = 3;
            this.lwRecipe.UseCompatibleStateImageBehavior = false;
            this.lwRecipe.View = System.Windows.Forms.View.Details;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(372, 399);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(348, 37);
            this.btnFolder.Margin = new System.Windows.Forms.Padding(4);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(33, 28);
            this.btnFolder.TabIndex = 5;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // txtModDir
            // 
            this.txtModDir.Location = new System.Drawing.Point(24, 39);
            this.txtModDir.Margin = new System.Windows.Forms.Padding(4);
            this.txtModDir.Name = "txtModDir";
            this.txtModDir.Size = new System.Drawing.Size(315, 22);
            this.txtModDir.TabIndex = 6;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(389, 37);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(67, 28);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(1052, 89);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(159, 22);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1220, 87);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(67, 28);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // thingDesc
            // 
            this.thingDesc.Location = new System.Drawing.Point(0, 20);
            this.thingDesc.Margin = new System.Windows.Forms.Padding(4);
            this.thingDesc.Multiline = true;
            this.thingDesc.Name = "thingDesc";
            this.thingDesc.ReadOnly = true;
            this.thingDesc.Size = new System.Drawing.Size(317, 124);
            this.thingDesc.TabIndex = 10;
            // 
            // xmlView
            // 
            this.xmlView.Location = new System.Drawing.Point(755, 399);
            this.xmlView.Margin = new System.Windows.Forms.Padding(4);
            this.xmlView.Multiline = true;
            this.xmlView.Name = "xmlView";
            this.xmlView.ReadOnly = true;
            this.xmlView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.xmlView.Size = new System.Drawing.Size(650, 367);
            this.xmlView.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 103);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Mods";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Defs";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Rimworld directory";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(752, 379);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "XML";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(752, 103);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Content";
            // 
            // gbDesc
            // 
            this.gbDesc.Controls.Add(this.thingDesc);
            this.gbDesc.Location = new System.Drawing.Point(25, 379);
            this.gbDesc.Margin = new System.Windows.Forms.Padding(4);
            this.gbDesc.Name = "gbDesc";
            this.gbDesc.Padding = new System.Windows.Forms.Padding(4);
            this.gbDesc.Size = new System.Drawing.Size(317, 143);
            this.gbDesc.TabIndex = 19;
            this.gbDesc.TabStop = false;
            this.gbDesc.Text = "Description";
            this.gbDesc.Visible = false;
            // 
            // gbRecipe
            // 
            this.gbRecipe.Controls.Add(this.lwRecipe);
            this.gbRecipe.Location = new System.Drawing.Point(25, 643);
            this.gbRecipe.Margin = new System.Windows.Forms.Padding(4);
            this.gbRecipe.Name = "gbRecipe";
            this.gbRecipe.Padding = new System.Windows.Forms.Padding(4);
            this.gbRecipe.Size = new System.Drawing.Size(480, 123);
            this.gbRecipe.TabIndex = 20;
            this.gbRecipe.TabStop = false;
            this.gbRecipe.Text = "Ingredients";
            this.gbRecipe.Visible = false;
            // 
            // cbOnlyActiveMods
            // 
            this.cbOnlyActiveMods.AutoSize = true;
            this.cbOnlyActiveMods.Location = new System.Drawing.Point(88, 70);
            this.cbOnlyActiveMods.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbOnlyActiveMods.Name = "cbOnlyActiveMods";
            this.cbOnlyActiveMods.Size = new System.Drawing.Size(93, 20);
            this.cbOnlyActiveMods.TabIndex = 21;
            this.cbOnlyActiveMods.Text = "only active";
            this.cbOnlyActiveMods.UseVisualStyleBackColor = true;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(632, 18);
            this.lblPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(0, 16);
            this.lblPath.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(479, 103);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 26;
            this.label6.Text = "Patches";
            // 
            // lbPatches
            // 
            this.lbPatches.FormattingEnabled = true;
            this.lbPatches.ItemHeight = 16;
            this.lbPatches.Location = new System.Drawing.Point(483, 123);
            this.lbPatches.Margin = new System.Windows.Forms.Padding(4);
            this.lbPatches.Name = "lbPatches";
            this.lbPatches.Size = new System.Drawing.Size(215, 228);
            this.lbPatches.TabIndex = 25;
            this.lbPatches.SelectedIndexChanged += new System.EventHandler(this.lbPatches_SelectedIndexChanged);
            // 
            // cbVersion
            // 
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Location = new System.Drawing.Point(23, 68);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(59, 24);
            this.cbVersion.TabIndex = 27;
            // 
            // btnDisable
            // 
            this.btnDisable.Enabled = false;
            this.btnDisable.Location = new System.Drawing.Point(1210, 358);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(75, 23);
            this.btnDisable.TabIndex = 28;
            this.btnDisable.Text = "Disable";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // btnEnable
            // 
            this.btnEnable.Enabled = false;
            this.btnEnable.Location = new System.Drawing.Point(1136, 358);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(75, 23);
            this.btnEnable.TabIndex = 29;
            this.btnEnable.Text = "Enable";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 897);
            this.Controls.Add(this.btnEnable);
            this.Controls.Add(this.btnDisable);
            this.Controls.Add(this.cbVersion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbPatches);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.cbOnlyActiveMods);
            this.Controls.Add(this.gbRecipe);
            this.Controls.Add(this.gbDesc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.xmlView);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtModDir);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lwDefs);
            this.Controls.Add(this.lbDefTypes);
            this.Controls.Add(this.lbMods);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "RimDef";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbDesc.ResumeLayout(false);
            this.gbDesc.PerformLayout();
            this.gbRecipe.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbMods;
        private System.Windows.Forms.ListBox lbDefTypes;
        private System.Windows.Forms.ListView lwDefs;
        private System.Windows.Forms.ListView lwRecipe;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox txtModDir;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox thingDesc;
        private System.Windows.Forms.TextBox xmlView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbDesc;
        private System.Windows.Forms.GroupBox gbRecipe;
        private System.Windows.Forms.CheckBox cbOnlyActiveMods;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lbPatches;
        private System.Windows.Forms.ComboBox cbVersion;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.Button btnEnable;
    }
}


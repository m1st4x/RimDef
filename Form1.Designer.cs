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
            this.lwDetail = new System.Windows.Forms.ListView();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbDesc.SuspendLayout();
            this.gbRecipe.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMods
            // 
            this.lbMods.FormattingEnabled = true;
            this.lbMods.Location = new System.Drawing.Point(17, 83);
            this.lbMods.Name = "lbMods";
            this.lbMods.Size = new System.Drawing.Size(150, 186);
            this.lbMods.TabIndex = 0;
            this.lbMods.SelectedIndexChanged += new System.EventHandler(this.lbMods_SelectedIndexChanged);
            // 
            // lbDefTypes
            // 
            this.lbDefTypes.FormattingEnabled = true;
            this.lbDefTypes.Location = new System.Drawing.Point(186, 82);
            this.lbDefTypes.Name = "lbDefTypes";
            this.lbDefTypes.Size = new System.Drawing.Size(120, 186);
            this.lbDefTypes.TabIndex = 1;
            this.lbDefTypes.SelectedIndexChanged += new System.EventHandler(this.lbDefTypes_SelectedIndexChanged);
            // 
            // lwDefs
            // 
            this.lwDefs.FullRowSelect = true;
            this.lwDefs.Location = new System.Drawing.Point(17, 306);
            this.lwDefs.Name = "lwDefs";
            this.lwDefs.Size = new System.Drawing.Size(400, 250);
            this.lwDefs.TabIndex = 2;
            this.lwDefs.UseCompatibleStateImageBehavior = false;
            this.lwDefs.View = System.Windows.Forms.View.Details;
            this.lwDefs.SelectedIndexChanged += new System.EventHandler(this.lwDefs_SelectedIndexChanged);
            // 
            // lwDetail
            // 
            this.lwDetail.Location = new System.Drawing.Point(6, 19);
            this.lwDetail.Name = "lwDetail";
            this.lwDetail.Size = new System.Drawing.Size(362, 69);
            this.lwDetail.TabIndex = 3;
            this.lwDetail.UseCompatibleStateImageBehavior = false;
            this.lwDetail.View = System.Windows.Forms.View.Details;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(696, 351);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(261, 30);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(25, 23);
            this.btnFolder.TabIndex = 5;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // txtModDir
            // 
            this.txtModDir.Location = new System.Drawing.Point(18, 32);
            this.txtModDir.Name = "txtModDir";
            this.txtModDir.Size = new System.Drawing.Size(237, 20);
            this.txtModDir.TabIndex = 6;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(292, 30);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(50, 23);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(17, 562);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(120, 20);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(143, 560);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(50, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // thingDesc
            // 
            this.thingDesc.Location = new System.Drawing.Point(6, 19);
            this.thingDesc.Multiline = true;
            this.thingDesc.Name = "thingDesc";
            this.thingDesc.ReadOnly = true;
            this.thingDesc.Size = new System.Drawing.Size(237, 69);
            this.thingDesc.TabIndex = 10;
            // 
            // xmlView
            // 
            this.xmlView.Location = new System.Drawing.Point(441, 32);
            this.xmlView.Multiline = true;
            this.xmlView.Name = "xmlView";
            this.xmlView.ReadOnly = true;
            this.xmlView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.xmlView.Size = new System.Drawing.Size(374, 313);
            this.xmlView.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Mods";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Filter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Mods directory";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(438, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "XML";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Definitions";
            // 
            // gbDesc
            // 
            this.gbDesc.Controls.Add(this.thingDesc);
            this.gbDesc.Location = new System.Drawing.Point(441, 361);
            this.gbDesc.Name = "gbDesc";
            this.gbDesc.Size = new System.Drawing.Size(249, 95);
            this.gbDesc.TabIndex = 19;
            this.gbDesc.TabStop = false;
            this.gbDesc.Text = "Description";
            this.gbDesc.Visible = false;
            // 
            // gbRecipe
            // 
            this.gbRecipe.Controls.Add(this.lwDetail);
            this.gbRecipe.Location = new System.Drawing.Point(441, 457);
            this.gbRecipe.Name = "gbRecipe";
            this.gbRecipe.Size = new System.Drawing.Size(374, 99);
            this.gbRecipe.TabIndex = 20;
            this.gbRecipe.TabStop = false;
            this.gbRecipe.Text = "Recipe";
            this.gbRecipe.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 603);
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
        private System.Windows.Forms.ListView lwDetail;
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
    }
}


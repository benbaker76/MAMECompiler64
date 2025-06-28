namespace MAMECompiler64
{
    partial class frmMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.grpConsoleOutput = new System.Windows.Forms.GroupBox();
			this.rtbConsoleOutput = new System.Windows.Forms.RichTextBox();
			this.cmsCompileOutput = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copySelectedTextToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.butGo = new System.Windows.Forms.Button();
			this.grpGeneral = new System.Windows.Forms.GroupBox();
			this.lblSourceFolder = new System.Windows.Forms.Label();
			this.butOpenSourceFolder = new System.Windows.Forms.Button();
			this.butSourceFolder = new System.Windows.Forms.Button();
			this.txtSourceFolder = new System.Windows.Forms.TextBox();
			this.lblPatchFolder = new System.Windows.Forms.Label();
			this.butOpenPatchFolder = new System.Windows.Forms.Button();
			this.butPatchFolder = new System.Windows.Forms.Button();
			this.txtPatchFolder = new System.Windows.Forms.TextBox();
			this.lblMAMESourceFolder = new System.Windows.Forms.Label();
			this.lblBuildToolsFolder = new System.Windows.Forms.Label();
			this.butOpenMAMESourceFolder = new System.Windows.Forms.Button();
			this.butMAMESourceFolder = new System.Windows.Forms.Button();
			this.butOpenBuildToolsFolder = new System.Windows.Forms.Button();
			this.txtMAMESourceFolder = new System.Windows.Forms.TextBox();
			this.butBuildToolsFolder = new System.Windows.Forms.Button();
			this.txtBuildToolsFolder = new System.Windows.Forms.TextBox();
			this.lblCompilerParallelJobs = new System.Windows.Forms.Label();
			this.cboCompilerParallelJobs = new System.Windows.Forms.ComboBox();
			this.chkCleanCompile = new System.Windows.Forms.CheckBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslMAMELogo = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblElapsed = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabOptions = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.tabDownloads = new System.Windows.Forms.TabPage();
			this.grpDownloads = new System.Windows.Forms.GroupBox();
			this.butUpdateMAMECompiler = new System.Windows.Forms.Button();
			this.butUpdateCompileTools = new System.Windows.Forms.Button();
			this.butDownloadFileList = new System.Windows.Forms.Button();
			this.butDownloadSelected = new System.Windows.Forms.Button();
			this.lvwDownloads = new System.Windows.Forms.ListView();
			this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmsDownloads = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyLinkStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabApplyPatch = new System.Windows.Forms.TabPage();
			this.grpApplyPatch = new System.Windows.Forms.GroupBox();
			this.chkEOLConversion = new System.Windows.Forms.CheckBox();
			this.cboPrefixStrip = new System.Windows.Forms.ComboBox();
			this.lblPrefixStrip = new System.Windows.Forms.Label();
			this.butTestDiffPatch = new System.Windows.Forms.Button();
			this.lblDiffPatchFile = new System.Windows.Forms.Label();
			this.butReverseDiffPatch = new System.Windows.Forms.Button();
			this.butOpenDiffPatchFolder = new System.Windows.Forms.Button();
			this.butApplyDiffPatch = new System.Windows.Forms.Button();
			this.butDiffPatchFile = new System.Windows.Forms.Button();
			this.txtDiffPatchFile = new System.Windows.Forms.TextBox();
			this.tabCreatePatch = new System.Windows.Forms.TabPage();
			this.grpCreatePatch = new System.Windows.Forms.GroupBox();
			this.lblOutputPatchFile = new System.Windows.Forms.Label();
			this.butOpenOutputPatchFile = new System.Windows.Forms.Button();
			this.butOutputPatchFile = new System.Windows.Forms.Button();
			this.txtOutputPatchFile = new System.Windows.Forms.TextBox();
			this.lblOriginalSourceFolder = new System.Windows.Forms.Label();
			this.butOpenOriginalSourceFolder = new System.Windows.Forms.Button();
			this.butOriginalSourceFolder = new System.Windows.Forms.Button();
			this.txtOriginalSourceFolder = new System.Windows.Forms.TextBox();
			this.lblModifiedSourceFolder = new System.Windows.Forms.Label();
			this.butOpenModifiedSourceFolder = new System.Windows.Forms.Button();
			this.butCreateDiffPatch = new System.Windows.Forms.Button();
			this.butModifiedSourceFolder = new System.Windows.Forms.Button();
			this.txtModifiedSourceFolder = new System.Windows.Forms.TextBox();
			this.tabBuild = new System.Windows.Forms.TabPage();
			this.grpBuild = new System.Windows.Forms.GroupBox();
			this.pnlBuild = new System.Windows.Forms.Panel();
			this.lblProcesor = new System.Windows.Forms.Label();
			this.cboOptimize = new System.Windows.Forms.ComboBox();
			this.lblTargetOS = new System.Windows.Forms.Label();
			this.cboTargetOS = new System.Windows.Forms.ComboBox();
			this.cboTarget = new System.Windows.Forms.ComboBox();
			this.lblOSD = new System.Windows.Forms.Label();
			this.lblTarget = new System.Windows.Forms.Label();
			this.cboOSD = new System.Windows.Forms.ComboBox();
			this.cboSubTarget = new System.Windows.Forms.ComboBox();
			this.lblSubTarget = new System.Windows.Forms.Label();
			this.lblOptimizeLevel = new System.Windows.Forms.Label();
			this.cboOptimizeLevel = new System.Windows.Forms.ComboBox();
			this.tabHelp = new System.Windows.Forms.TabPage();
			this.grpHelp = new System.Windows.Forms.GroupBox();
			this.wbHelp = new System.Windows.Forms.WebBrowser();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.grpConsoleOutput.SuspendLayout();
			this.cmsCompileOutput.SuspendLayout();
			this.grpGeneral.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tabOptions.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.tabDownloads.SuspendLayout();
			this.grpDownloads.SuspendLayout();
			this.cmsDownloads.SuspendLayout();
			this.tabApplyPatch.SuspendLayout();
			this.grpApplyPatch.SuspendLayout();
			this.tabCreatePatch.SuspendLayout();
			this.grpCreatePatch.SuspendLayout();
			this.tabBuild.SuspendLayout();
			this.grpBuild.SuspendLayout();
			this.tabHelp.SuspendLayout();
			this.grpHelp.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpConsoleOutput
			// 
			this.grpConsoleOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpConsoleOutput.Controls.Add(this.rtbConsoleOutput);
			this.grpConsoleOutput.Location = new System.Drawing.Point(13, 223);
			this.grpConsoleOutput.Name = "grpConsoleOutput";
			this.grpConsoleOutput.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
			this.grpConsoleOutput.Size = new System.Drawing.Size(599, 123);
			this.grpConsoleOutput.TabIndex = 1;
			this.grpConsoleOutput.TabStop = false;
			this.grpConsoleOutput.Text = "Console Output";
			// 
			// rtbConsoleOutput
			// 
			this.rtbConsoleOutput.BackColor = System.Drawing.Color.Black;
			this.rtbConsoleOutput.ContextMenuStrip = this.cmsCompileOutput;
			this.rtbConsoleOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbConsoleOutput.ForeColor = System.Drawing.Color.White;
			this.rtbConsoleOutput.Location = new System.Drawing.Point(8, 17);
			this.rtbConsoleOutput.Name = "rtbConsoleOutput";
			this.rtbConsoleOutput.ReadOnly = true;
			this.rtbConsoleOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.rtbConsoleOutput.Size = new System.Drawing.Size(583, 98);
			this.rtbConsoleOutput.TabIndex = 0;
			this.rtbConsoleOutput.Text = "";
			this.rtbConsoleOutput.WordWrap = false;
			this.rtbConsoleOutput.TextChanged += new System.EventHandler(this.rtbConsoleOutput_TextChanged);
			// 
			// cmsCompileOutput
			// 
			this.cmsCompileOutput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySelectedTextToClipboardToolStripMenuItem,
            this.copyAllToolStripMenuItem,
            this.clearAllToolStripMenuItem});
			this.cmsCompileOutput.Name = "contextMenuStrip1";
			this.cmsCompileOutput.Size = new System.Drawing.Size(154, 70);
			// 
			// copySelectedTextToClipboardToolStripMenuItem
			// 
			this.copySelectedTextToClipboardToolStripMenuItem.Name = "copySelectedTextToClipboardToolStripMenuItem";
			this.copySelectedTextToClipboardToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.copySelectedTextToClipboardToolStripMenuItem.Text = "Copy Selection";
			this.copySelectedTextToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copySelectionToolStripMenuItem_Click);
			// 
			// copyAllToolStripMenuItem
			// 
			this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
			this.copyAllToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.copyAllToolStripMenuItem.Text = "Copy All";
			this.copyAllToolStripMenuItem.Click += new System.EventHandler(this.copyAllToolStripMenuItem_Click);
			// 
			// clearAllToolStripMenuItem
			// 
			this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
			this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.clearAllToolStripMenuItem.Text = "Clear All";
			this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
			// 
			// butGo
			// 
			this.butGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.butGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(161)))), ((int)(((byte)(226)))));
			this.butGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.butGo.ForeColor = System.Drawing.Color.White;
			this.butGo.Location = new System.Drawing.Point(12, 352);
			this.butGo.Name = "butGo";
			this.butGo.Size = new System.Drawing.Size(600, 28);
			this.butGo.TabIndex = 2;
			this.butGo.Text = "GO!";
			this.butGo.UseVisualStyleBackColor = false;
			this.butGo.Click += new System.EventHandler(this.butGo_Click);
			// 
			// grpGeneral
			// 
			this.grpGeneral.Controls.Add(this.lblSourceFolder);
			this.grpGeneral.Controls.Add(this.butOpenSourceFolder);
			this.grpGeneral.Controls.Add(this.butSourceFolder);
			this.grpGeneral.Controls.Add(this.txtSourceFolder);
			this.grpGeneral.Controls.Add(this.lblPatchFolder);
			this.grpGeneral.Controls.Add(this.butOpenPatchFolder);
			this.grpGeneral.Controls.Add(this.butPatchFolder);
			this.grpGeneral.Controls.Add(this.txtPatchFolder);
			this.grpGeneral.Controls.Add(this.lblMAMESourceFolder);
			this.grpGeneral.Controls.Add(this.lblBuildToolsFolder);
			this.grpGeneral.Controls.Add(this.butOpenMAMESourceFolder);
			this.grpGeneral.Controls.Add(this.butMAMESourceFolder);
			this.grpGeneral.Controls.Add(this.butOpenBuildToolsFolder);
			this.grpGeneral.Controls.Add(this.txtMAMESourceFolder);
			this.grpGeneral.Controls.Add(this.butBuildToolsFolder);
			this.grpGeneral.Controls.Add(this.txtBuildToolsFolder);
			this.grpGeneral.Controls.Add(this.lblCompilerParallelJobs);
			this.grpGeneral.Controls.Add(this.cboCompilerParallelJobs);
			this.grpGeneral.Controls.Add(this.chkCleanCompile);
			this.grpGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpGeneral.Location = new System.Drawing.Point(3, 3);
			this.grpGeneral.Name = "grpGeneral";
			this.grpGeneral.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
			this.grpGeneral.Size = new System.Drawing.Size(586, 170);
			this.grpGeneral.TabIndex = 0;
			this.grpGeneral.TabStop = false;
			this.grpGeneral.Text = "General Options";
			// 
			// lblSourceFolder
			// 
			this.lblSourceFolder.AutoSize = true;
			this.lblSourceFolder.Location = new System.Drawing.Point(53, 54);
			this.lblSourceFolder.Name = "lblSourceFolder";
			this.lblSourceFolder.Size = new System.Drawing.Size(76, 13);
			this.lblSourceFolder.TabIndex = 4;
			this.lblSourceFolder.Text = "Source Folder:";
			// 
			// butOpenSourceFolder
			// 
			this.butOpenSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOpenSourceFolder.Image = ((System.Drawing.Image)(resources.GetObject("butOpenSourceFolder.Image")));
			this.butOpenSourceFolder.Location = new System.Drawing.Point(538, 48);
			this.butOpenSourceFolder.Name = "butOpenSourceFolder";
			this.butOpenSourceFolder.Size = new System.Drawing.Size(28, 24);
			this.butOpenSourceFolder.TabIndex = 7;
			this.butOpenSourceFolder.UseVisualStyleBackColor = true;
			this.butOpenSourceFolder.Click += new System.EventHandler(this.butOpenSourceFolder_Click);
			// 
			// butSourceFolder
			// 
			this.butSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSourceFolder.Image = ((System.Drawing.Image)(resources.GetObject("butSourceFolder.Image")));
			this.butSourceFolder.Location = new System.Drawing.Point(508, 48);
			this.butSourceFolder.Name = "butSourceFolder";
			this.butSourceFolder.Size = new System.Drawing.Size(28, 24);
			this.butSourceFolder.TabIndex = 6;
			this.butSourceFolder.UseVisualStyleBackColor = true;
			this.butSourceFolder.Click += new System.EventHandler(this.butSourceFolder_Click);
			// 
			// txtSourceFolder
			// 
			this.txtSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSourceFolder.Location = new System.Drawing.Point(135, 51);
			this.txtSourceFolder.Name = "txtSourceFolder";
			this.txtSourceFolder.Size = new System.Drawing.Size(371, 20);
			this.txtSourceFolder.TabIndex = 5;
			// 
			// lblPatchFolder
			// 
			this.lblPatchFolder.AutoSize = true;
			this.lblPatchFolder.Location = new System.Drawing.Point(59, 84);
			this.lblPatchFolder.Name = "lblPatchFolder";
			this.lblPatchFolder.Size = new System.Drawing.Size(70, 13);
			this.lblPatchFolder.TabIndex = 8;
			this.lblPatchFolder.Text = "Patch Folder:";
			// 
			// butOpenPatchFolder
			// 
			this.butOpenPatchFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOpenPatchFolder.Image = ((System.Drawing.Image)(resources.GetObject("butOpenPatchFolder.Image")));
			this.butOpenPatchFolder.Location = new System.Drawing.Point(538, 78);
			this.butOpenPatchFolder.Name = "butOpenPatchFolder";
			this.butOpenPatchFolder.Size = new System.Drawing.Size(28, 24);
			this.butOpenPatchFolder.TabIndex = 11;
			this.butOpenPatchFolder.UseVisualStyleBackColor = true;
			this.butOpenPatchFolder.Click += new System.EventHandler(this.butOpenPatchFolder_Click);
			// 
			// butPatchFolder
			// 
			this.butPatchFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butPatchFolder.Image = ((System.Drawing.Image)(resources.GetObject("butPatchFolder.Image")));
			this.butPatchFolder.Location = new System.Drawing.Point(508, 78);
			this.butPatchFolder.Name = "butPatchFolder";
			this.butPatchFolder.Size = new System.Drawing.Size(28, 24);
			this.butPatchFolder.TabIndex = 10;
			this.butPatchFolder.UseVisualStyleBackColor = true;
			this.butPatchFolder.Click += new System.EventHandler(this.butPatchFolder_Click);
			// 
			// txtPatchFolder
			// 
			this.txtPatchFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPatchFolder.Location = new System.Drawing.Point(135, 81);
			this.txtPatchFolder.Name = "txtPatchFolder";
			this.txtPatchFolder.Size = new System.Drawing.Size(371, 20);
			this.txtPatchFolder.TabIndex = 9;
			// 
			// lblMAMESourceFolder
			// 
			this.lblMAMESourceFolder.AutoSize = true;
			this.lblMAMESourceFolder.Location = new System.Drawing.Point(21, 114);
			this.lblMAMESourceFolder.Name = "lblMAMESourceFolder";
			this.lblMAMESourceFolder.Size = new System.Drawing.Size(111, 13);
			this.lblMAMESourceFolder.TabIndex = 12;
			this.lblMAMESourceFolder.Text = "MAME Source Folder:";
			// 
			// lblBuildToolsFolder
			// 
			this.lblBuildToolsFolder.AutoSize = true;
			this.lblBuildToolsFolder.Location = new System.Drawing.Point(35, 24);
			this.lblBuildToolsFolder.Name = "lblBuildToolsFolder";
			this.lblBuildToolsFolder.Size = new System.Drawing.Size(94, 13);
			this.lblBuildToolsFolder.TabIndex = 0;
			this.lblBuildToolsFolder.Text = "Build Tools Folder:";
			// 
			// butOpenMAMESourceFolder
			// 
			this.butOpenMAMESourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOpenMAMESourceFolder.Image = ((System.Drawing.Image)(resources.GetObject("butOpenMAMESourceFolder.Image")));
			this.butOpenMAMESourceFolder.Location = new System.Drawing.Point(538, 108);
			this.butOpenMAMESourceFolder.Name = "butOpenMAMESourceFolder";
			this.butOpenMAMESourceFolder.Size = new System.Drawing.Size(28, 24);
			this.butOpenMAMESourceFolder.TabIndex = 15;
			this.butOpenMAMESourceFolder.UseVisualStyleBackColor = true;
			this.butOpenMAMESourceFolder.Click += new System.EventHandler(this.butOpenMAMESourceFolder_Click);
			// 
			// butMAMESourceFolder
			// 
			this.butMAMESourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMAMESourceFolder.Image = ((System.Drawing.Image)(resources.GetObject("butMAMESourceFolder.Image")));
			this.butMAMESourceFolder.Location = new System.Drawing.Point(508, 108);
			this.butMAMESourceFolder.Name = "butMAMESourceFolder";
			this.butMAMESourceFolder.Size = new System.Drawing.Size(28, 24);
			this.butMAMESourceFolder.TabIndex = 14;
			this.butMAMESourceFolder.UseVisualStyleBackColor = true;
			this.butMAMESourceFolder.Click += new System.EventHandler(this.butMAMESourceFolder_Click);
			// 
			// butOpenBuildToolsFolder
			// 
			this.butOpenBuildToolsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOpenBuildToolsFolder.Image = ((System.Drawing.Image)(resources.GetObject("butOpenBuildToolsFolder.Image")));
			this.butOpenBuildToolsFolder.Location = new System.Drawing.Point(538, 19);
			this.butOpenBuildToolsFolder.Name = "butOpenBuildToolsFolder";
			this.butOpenBuildToolsFolder.Size = new System.Drawing.Size(28, 24);
			this.butOpenBuildToolsFolder.TabIndex = 3;
			this.butOpenBuildToolsFolder.UseVisualStyleBackColor = true;
			this.butOpenBuildToolsFolder.Click += new System.EventHandler(this.butOpenMinGWFolder_Click);
			// 
			// txtMAMESourceFolder
			// 
			this.txtMAMESourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMAMESourceFolder.Location = new System.Drawing.Point(135, 111);
			this.txtMAMESourceFolder.Name = "txtMAMESourceFolder";
			this.txtMAMESourceFolder.Size = new System.Drawing.Size(371, 20);
			this.txtMAMESourceFolder.TabIndex = 13;
			// 
			// butBuildToolsFolder
			// 
			this.butBuildToolsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butBuildToolsFolder.Image = ((System.Drawing.Image)(resources.GetObject("butBuildToolsFolder.Image")));
			this.butBuildToolsFolder.Location = new System.Drawing.Point(508, 19);
			this.butBuildToolsFolder.Name = "butBuildToolsFolder";
			this.butBuildToolsFolder.Size = new System.Drawing.Size(28, 24);
			this.butBuildToolsFolder.TabIndex = 2;
			this.butBuildToolsFolder.UseVisualStyleBackColor = true;
			this.butBuildToolsFolder.Click += new System.EventHandler(this.butMinGWFolder_Click);
			// 
			// txtBuildToolsFolder
			// 
			this.txtBuildToolsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBuildToolsFolder.Location = new System.Drawing.Point(135, 21);
			this.txtBuildToolsFolder.Name = "txtBuildToolsFolder";
			this.txtBuildToolsFolder.Size = new System.Drawing.Size(371, 20);
			this.txtBuildToolsFolder.TabIndex = 1;
			// 
			// lblCompilerParallelJobs
			// 
			this.lblCompilerParallelJobs.AutoSize = true;
			this.lblCompilerParallelJobs.Location = new System.Drawing.Point(17, 145);
			this.lblCompilerParallelJobs.Name = "lblCompilerParallelJobs";
			this.lblCompilerParallelJobs.Size = new System.Drawing.Size(112, 13);
			this.lblCompilerParallelJobs.TabIndex = 16;
			this.lblCompilerParallelJobs.Text = "Compiler Parallel Jobs:";
			// 
			// cboCompilerParallelJobs
			// 
			this.cboCompilerParallelJobs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCompilerParallelJobs.FormattingEnabled = true;
			this.cboCompilerParallelJobs.Location = new System.Drawing.Point(135, 142);
			this.cboCompilerParallelJobs.Name = "cboCompilerParallelJobs";
			this.cboCompilerParallelJobs.Size = new System.Drawing.Size(62, 21);
			this.cboCompilerParallelJobs.TabIndex = 17;
			// 
			// chkCleanCompile
			// 
			this.chkCleanCompile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkCleanCompile.AutoSize = true;
			this.chkCleanCompile.Location = new System.Drawing.Point(473, 146);
			this.chkCleanCompile.Name = "chkCleanCompile";
			this.chkCleanCompile.Size = new System.Drawing.Size(93, 17);
			this.chkCleanCompile.TabIndex = 18;
			this.chkCleanCompile.Text = "Clean Compile";
			this.chkCleanCompile.UseVisualStyleBackColor = true;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMAMELogo,
            this.lblStatus,
            this.lblElapsed});
			this.statusStrip1.Location = new System.Drawing.Point(0, 389);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(624, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsslMAMELogo
			// 
			this.tsslMAMELogo.AutoSize = false;
			this.tsslMAMELogo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsslMAMELogo.Image = ((System.Drawing.Image)(resources.GetObject("tsslMAMELogo.Image")));
			this.tsslMAMELogo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.tsslMAMELogo.IsLink = true;
			this.tsslMAMELogo.Name = "tsslMAMELogo";
			this.tsslMAMELogo.Size = new System.Drawing.Size(65, 17);
			this.tsslMAMELogo.Click += new System.EventHandler(this.tsslHeadsoftLogo_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(412, 17);
			this.lblStatus.Spring = true;
			this.lblStatus.Text = "Idle.";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblElapsed
			// 
			this.lblElapsed.Name = "lblElapsed";
			this.lblElapsed.Size = new System.Drawing.Size(49, 17);
			this.lblElapsed.Text = "00:00:00";
			// 
			// tabOptions
			// 
			this.tabOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabOptions.Controls.Add(this.tabGeneral);
			this.tabOptions.Controls.Add(this.tabDownloads);
			this.tabOptions.Controls.Add(this.tabApplyPatch);
			this.tabOptions.Controls.Add(this.tabCreatePatch);
			this.tabOptions.Controls.Add(this.tabBuild);
			this.tabOptions.Controls.Add(this.tabHelp);
			this.tabOptions.ImageList = this.imageList1;
			this.tabOptions.Location = new System.Drawing.Point(12, 13);
			this.tabOptions.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
			this.tabOptions.Name = "tabOptions";
			this.tabOptions.SelectedIndex = 0;
			this.tabOptions.Size = new System.Drawing.Size(600, 203);
			this.tabOptions.TabIndex = 0;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.grpGeneral);
			this.tabGeneral.ImageIndex = 3;
			this.tabGeneral.Location = new System.Drawing.Point(4, 23);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(592, 176);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// tabDownloads
			// 
			this.tabDownloads.Controls.Add(this.grpDownloads);
			this.tabDownloads.ImageIndex = 1;
			this.tabDownloads.Location = new System.Drawing.Point(4, 23);
			this.tabDownloads.Name = "tabDownloads";
			this.tabDownloads.Padding = new System.Windows.Forms.Padding(3);
			this.tabDownloads.Size = new System.Drawing.Size(592, 176);
			this.tabDownloads.TabIndex = 7;
			this.tabDownloads.Text = "Downloads";
			this.tabDownloads.UseVisualStyleBackColor = true;
			// 
			// grpDownloads
			// 
			this.grpDownloads.Controls.Add(this.butUpdateMAMECompiler);
			this.grpDownloads.Controls.Add(this.butUpdateCompileTools);
			this.grpDownloads.Controls.Add(this.butDownloadFileList);
			this.grpDownloads.Controls.Add(this.butDownloadSelected);
			this.grpDownloads.Controls.Add(this.lvwDownloads);
			this.grpDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpDownloads.Location = new System.Drawing.Point(3, 3);
			this.grpDownloads.Name = "grpDownloads";
			this.grpDownloads.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
			this.grpDownloads.Size = new System.Drawing.Size(586, 170);
			this.grpDownloads.TabIndex = 0;
			this.grpDownloads.TabStop = false;
			this.grpDownloads.Text = "Downloads";
			// 
			// butUpdateMAMECompiler
			// 
			this.butUpdateMAMECompiler.Image = ((System.Drawing.Image)(resources.GetObject("butUpdateMAMECompiler.Image")));
			this.butUpdateMAMECompiler.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUpdateMAMECompiler.Location = new System.Drawing.Point(283, 137);
			this.butUpdateMAMECompiler.Name = "butUpdateMAMECompiler";
			this.butUpdateMAMECompiler.Size = new System.Drawing.Size(144, 24);
			this.butUpdateMAMECompiler.TabIndex = 4;
			this.butUpdateMAMECompiler.Text = "Update MAME Compiler";
			this.butUpdateMAMECompiler.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butUpdateMAMECompiler.UseVisualStyleBackColor = true;
			this.butUpdateMAMECompiler.Click += new System.EventHandler(this.butUpdateMAMECompiler_Click);
			// 
			// butUpdateCompileTools
			// 
			this.butUpdateCompileTools.Image = ((System.Drawing.Image)(resources.GetObject("butUpdateCompileTools.Image")));
			this.butUpdateCompileTools.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUpdateCompileTools.Location = new System.Drawing.Point(148, 137);
			this.butUpdateCompileTools.Name = "butUpdateCompileTools";
			this.butUpdateCompileTools.Size = new System.Drawing.Size(129, 24);
			this.butUpdateCompileTools.TabIndex = 3;
			this.butUpdateCompileTools.Text = "Update Build Tools";
			this.butUpdateCompileTools.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butUpdateCompileTools.UseVisualStyleBackColor = true;
			this.butUpdateCompileTools.Click += new System.EventHandler(this.butUpdateCompileTools_Click);
			// 
			// butDownloadFileList
			// 
			this.butDownloadFileList.Image = ((System.Drawing.Image)(resources.GetObject("butDownloadFileList.Image")));
			this.butDownloadFileList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDownloadFileList.Location = new System.Drawing.Point(13, 137);
			this.butDownloadFileList.Name = "butDownloadFileList";
			this.butDownloadFileList.Size = new System.Drawing.Size(129, 24);
			this.butDownloadFileList.TabIndex = 1;
			this.butDownloadFileList.Text = "Download File List";
			this.butDownloadFileList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butDownloadFileList.UseVisualStyleBackColor = true;
			this.butDownloadFileList.Click += new System.EventHandler(this.butDownloadFileList_Click);
			// 
			// butDownloadSelected
			// 
			this.butDownloadSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butDownloadSelected.Image = ((System.Drawing.Image)(resources.GetObject("butDownloadSelected.Image")));
			this.butDownloadSelected.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDownloadSelected.Location = new System.Drawing.Point(443, 137);
			this.butDownloadSelected.Name = "butDownloadSelected";
			this.butDownloadSelected.Size = new System.Drawing.Size(129, 24);
			this.butDownloadSelected.TabIndex = 2;
			this.butDownloadSelected.Text = "Download Selected";
			this.butDownloadSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butDownloadSelected.UseVisualStyleBackColor = true;
			this.butDownloadSelected.Click += new System.EventHandler(this.butDownloadSelected_Click);
			// 
			// lvwDownloads
			// 
			this.lvwDownloads.CheckBoxes = true;
			this.lvwDownloads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colDescription,
            this.colType,
            this.colUrl});
			this.lvwDownloads.ContextMenuStrip = this.cmsDownloads;
			this.lvwDownloads.Dock = System.Windows.Forms.DockStyle.Top;
			this.lvwDownloads.FullRowSelect = true;
			this.lvwDownloads.GridLines = true;
			this.lvwDownloads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwDownloads.HideSelection = false;
			this.lvwDownloads.Location = new System.Drawing.Point(8, 17);
			this.lvwDownloads.MultiSelect = false;
			this.lvwDownloads.Name = "lvwDownloads";
			this.lvwDownloads.Size = new System.Drawing.Size(570, 113);
			this.lvwDownloads.TabIndex = 0;
			this.lvwDownloads.UseCompatibleStateImageBehavior = false;
			this.lvwDownloads.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Name";
			// 
			// colDescription
			// 
			this.colDescription.Text = "Description";
			this.colDescription.Width = 161;
			// 
			// colType
			// 
			this.colType.Text = "Type";
			// 
			// colUrl
			// 
			this.colUrl.Text = "Url";
			this.colUrl.Width = 314;
			// 
			// cmsDownloads
			// 
			this.cmsDownloads.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyLinkStripMenuItem});
			this.cmsDownloads.Name = "cmsDownloads";
			this.cmsDownloads.Size = new System.Drawing.Size(128, 26);
			// 
			// copyLinkStripMenuItem
			// 
			this.copyLinkStripMenuItem.Name = "copyLinkStripMenuItem";
			this.copyLinkStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.copyLinkStripMenuItem.Text = "Copy Link";
			this.copyLinkStripMenuItem.Click += new System.EventHandler(this.copyLinkStripMenuItem_Click);
			// 
			// tabApplyPatch
			// 
			this.tabApplyPatch.Controls.Add(this.grpApplyPatch);
			this.tabApplyPatch.ImageIndex = 2;
			this.tabApplyPatch.Location = new System.Drawing.Point(4, 23);
			this.tabApplyPatch.Name = "tabApplyPatch";
			this.tabApplyPatch.Padding = new System.Windows.Forms.Padding(3);
			this.tabApplyPatch.Size = new System.Drawing.Size(592, 176);
			this.tabApplyPatch.TabIndex = 6;
			this.tabApplyPatch.Text = "Apply Patch";
			this.tabApplyPatch.UseVisualStyleBackColor = true;
			// 
			// grpApplyPatch
			// 
			this.grpApplyPatch.Controls.Add(this.chkEOLConversion);
			this.grpApplyPatch.Controls.Add(this.cboPrefixStrip);
			this.grpApplyPatch.Controls.Add(this.lblPrefixStrip);
			this.grpApplyPatch.Controls.Add(this.butTestDiffPatch);
			this.grpApplyPatch.Controls.Add(this.lblDiffPatchFile);
			this.grpApplyPatch.Controls.Add(this.butReverseDiffPatch);
			this.grpApplyPatch.Controls.Add(this.butOpenDiffPatchFolder);
			this.grpApplyPatch.Controls.Add(this.butApplyDiffPatch);
			this.grpApplyPatch.Controls.Add(this.butDiffPatchFile);
			this.grpApplyPatch.Controls.Add(this.txtDiffPatchFile);
			this.grpApplyPatch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpApplyPatch.Location = new System.Drawing.Point(3, 3);
			this.grpApplyPatch.Name = "grpApplyPatch";
			this.grpApplyPatch.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
			this.grpApplyPatch.Size = new System.Drawing.Size(586, 170);
			this.grpApplyPatch.TabIndex = 0;
			this.grpApplyPatch.TabStop = false;
			this.grpApplyPatch.Text = "Apply Patch";
			// 
			// chkEOLConversion
			// 
			this.chkEOLConversion.AutoSize = true;
			this.chkEOLConversion.Location = new System.Drawing.Point(107, 111);
			this.chkEOLConversion.Name = "chkEOLConversion";
			this.chkEOLConversion.Size = new System.Drawing.Size(103, 17);
			this.chkEOLConversion.TabIndex = 33;
			this.chkEOLConversion.Text = "EOL Conversion";
			this.chkEOLConversion.UseVisualStyleBackColor = true;
			// 
			// cboPrefixStrip
			// 
			this.cboPrefixStrip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPrefixStrip.FormattingEnabled = true;
			this.cboPrefixStrip.Location = new System.Drawing.Point(107, 134);
			this.cboPrefixStrip.Name = "cboPrefixStrip";
			this.cboPrefixStrip.Size = new System.Drawing.Size(57, 21);
			this.cboPrefixStrip.TabIndex = 32;
			// 
			// lblPrefixStrip
			// 
			this.lblPrefixStrip.AutoSize = true;
			this.lblPrefixStrip.Location = new System.Drawing.Point(41, 138);
			this.lblPrefixStrip.Name = "lblPrefixStrip";
			this.lblPrefixStrip.Size = new System.Drawing.Size(60, 13);
			this.lblPrefixStrip.TabIndex = 31;
			this.lblPrefixStrip.Text = "Prefix Strip:";
			// 
			// butTestDiffPatch
			// 
			this.butTestDiffPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butTestDiffPatch.Image = ((System.Drawing.Image)(resources.GetObject("butTestDiffPatch.Image")));
			this.butTestDiffPatch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butTestDiffPatch.Location = new System.Drawing.Point(232, 132);
			this.butTestDiffPatch.Name = "butTestDiffPatch";
			this.butTestDiffPatch.Size = new System.Drawing.Size(102, 24);
			this.butTestDiffPatch.TabIndex = 4;
			this.butTestDiffPatch.Text = "Test Patch";
			this.butTestDiffPatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butTestDiffPatch.UseVisualStyleBackColor = true;
			this.butTestDiffPatch.Click += new System.EventHandler(this.butTestDiffPatch_Click);
			// 
			// lblDiffPatchFile
			// 
			this.lblDiffPatchFile.AutoSize = true;
			this.lblDiffPatchFile.Location = new System.Drawing.Point(25, 69);
			this.lblDiffPatchFile.Name = "lblDiffPatchFile";
			this.lblDiffPatchFile.Size = new System.Drawing.Size(76, 13);
			this.lblDiffPatchFile.TabIndex = 0;
			this.lblDiffPatchFile.Text = "Diff Patch File:";
			// 
			// butReverseDiffPatch
			// 
			this.butReverseDiffPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butReverseDiffPatch.Image = ((System.Drawing.Image)(resources.GetObject("butReverseDiffPatch.Image")));
			this.butReverseDiffPatch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butReverseDiffPatch.Location = new System.Drawing.Point(340, 132);
			this.butReverseDiffPatch.Name = "butReverseDiffPatch";
			this.butReverseDiffPatch.Size = new System.Drawing.Size(102, 24);
			this.butReverseDiffPatch.TabIndex = 5;
			this.butReverseDiffPatch.Text = "Reverse Patch";
			this.butReverseDiffPatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butReverseDiffPatch.UseVisualStyleBackColor = true;
			this.butReverseDiffPatch.Click += new System.EventHandler(this.butReverseDiffPatch_Click);
			// 
			// butOpenDiffPatchFolder
			// 
			this.butOpenDiffPatchFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOpenDiffPatchFolder.Image = ((System.Drawing.Image)(resources.GetObject("butOpenDiffPatchFolder.Image")));
			this.butOpenDiffPatchFolder.Location = new System.Drawing.Point(522, 63);
			this.butOpenDiffPatchFolder.Name = "butOpenDiffPatchFolder";
			this.butOpenDiffPatchFolder.Size = new System.Drawing.Size(28, 24);
			this.butOpenDiffPatchFolder.TabIndex = 3;
			this.butOpenDiffPatchFolder.UseVisualStyleBackColor = true;
			this.butOpenDiffPatchFolder.Click += new System.EventHandler(this.butOpenDiffPatchFolder_Click);
			// 
			// butApplyDiffPatch
			// 
			this.butApplyDiffPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butApplyDiffPatch.Image = ((System.Drawing.Image)(resources.GetObject("butApplyDiffPatch.Image")));
			this.butApplyDiffPatch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butApplyDiffPatch.Location = new System.Drawing.Point(448, 132);
			this.butApplyDiffPatch.Name = "butApplyDiffPatch";
			this.butApplyDiffPatch.Size = new System.Drawing.Size(102, 24);
			this.butApplyDiffPatch.TabIndex = 6;
			this.butApplyDiffPatch.Text = "Apply  Patch";
			this.butApplyDiffPatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butApplyDiffPatch.UseVisualStyleBackColor = true;
			this.butApplyDiffPatch.Click += new System.EventHandler(this.butApplyDiffPatch_Click);
			// 
			// butDiffPatchFile
			// 
			this.butDiffPatchFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butDiffPatchFile.Image = ((System.Drawing.Image)(resources.GetObject("butDiffPatchFile.Image")));
			this.butDiffPatchFile.Location = new System.Drawing.Point(492, 63);
			this.butDiffPatchFile.Name = "butDiffPatchFile";
			this.butDiffPatchFile.Size = new System.Drawing.Size(28, 24);
			this.butDiffPatchFile.TabIndex = 2;
			this.butDiffPatchFile.UseVisualStyleBackColor = true;
			this.butDiffPatchFile.Click += new System.EventHandler(this.butDiffFile_Click);
			// 
			// txtDiffPatchFile
			// 
			this.txtDiffPatchFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDiffPatchFile.Location = new System.Drawing.Point(107, 65);
			this.txtDiffPatchFile.Name = "txtDiffPatchFile";
			this.txtDiffPatchFile.Size = new System.Drawing.Size(379, 20);
			this.txtDiffPatchFile.TabIndex = 1;
			// 
			// tabCreatePatch
			// 
			this.tabCreatePatch.Controls.Add(this.grpCreatePatch);
			this.tabCreatePatch.ImageIndex = 2;
			this.tabCreatePatch.Location = new System.Drawing.Point(4, 23);
			this.tabCreatePatch.Name = "tabCreatePatch";
			this.tabCreatePatch.Padding = new System.Windows.Forms.Padding(3);
			this.tabCreatePatch.Size = new System.Drawing.Size(592, 176);
			this.tabCreatePatch.TabIndex = 9;
			this.tabCreatePatch.Text = "Create Patch";
			this.tabCreatePatch.UseVisualStyleBackColor = true;
			// 
			// grpCreatePatch
			// 
			this.grpCreatePatch.Controls.Add(this.lblOutputPatchFile);
			this.grpCreatePatch.Controls.Add(this.butOpenOutputPatchFile);
			this.grpCreatePatch.Controls.Add(this.butOutputPatchFile);
			this.grpCreatePatch.Controls.Add(this.txtOutputPatchFile);
			this.grpCreatePatch.Controls.Add(this.lblOriginalSourceFolder);
			this.grpCreatePatch.Controls.Add(this.butOpenOriginalSourceFolder);
			this.grpCreatePatch.Controls.Add(this.butOriginalSourceFolder);
			this.grpCreatePatch.Controls.Add(this.txtOriginalSourceFolder);
			this.grpCreatePatch.Controls.Add(this.lblModifiedSourceFolder);
			this.grpCreatePatch.Controls.Add(this.butOpenModifiedSourceFolder);
			this.grpCreatePatch.Controls.Add(this.butCreateDiffPatch);
			this.grpCreatePatch.Controls.Add(this.butModifiedSourceFolder);
			this.grpCreatePatch.Controls.Add(this.txtModifiedSourceFolder);
			this.grpCreatePatch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpCreatePatch.Location = new System.Drawing.Point(3, 3);
			this.grpCreatePatch.Name = "grpCreatePatch";
			this.grpCreatePatch.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
			this.grpCreatePatch.Size = new System.Drawing.Size(586, 170);
			this.grpCreatePatch.TabIndex = 1;
			this.grpCreatePatch.TabStop = false;
			this.grpCreatePatch.Text = "Create Patch";
			// 
			// lblOutputPatchFile
			// 
			this.lblOutputPatchFile.AutoSize = true;
			this.lblOutputPatchFile.Location = new System.Drawing.Point(40, 94);
			this.lblOutputPatchFile.Name = "lblOutputPatchFile";
			this.lblOutputPatchFile.Size = new System.Drawing.Size(92, 13);
			this.lblOutputPatchFile.TabIndex = 11;
			this.lblOutputPatchFile.Text = "Output Patch File:";
			// 
			// butOpenOutputPatchFile
			// 
			this.butOpenOutputPatchFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOpenOutputPatchFile.Image = ((System.Drawing.Image)(resources.GetObject("butOpenOutputPatchFile.Image")));
			this.butOpenOutputPatchFile.Location = new System.Drawing.Point(522, 89);
			this.butOpenOutputPatchFile.Name = "butOpenOutputPatchFile";
			this.butOpenOutputPatchFile.Size = new System.Drawing.Size(28, 24);
			this.butOpenOutputPatchFile.TabIndex = 14;
			this.butOpenOutputPatchFile.UseVisualStyleBackColor = true;
			this.butOpenOutputPatchFile.Click += new System.EventHandler(this.butOpenOutputPatchFile_Click);
			// 
			// butOutputPatchFile
			// 
			this.butOutputPatchFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOutputPatchFile.Image = ((System.Drawing.Image)(resources.GetObject("butOutputPatchFile.Image")));
			this.butOutputPatchFile.Location = new System.Drawing.Point(492, 89);
			this.butOutputPatchFile.Name = "butOutputPatchFile";
			this.butOutputPatchFile.Size = new System.Drawing.Size(28, 24);
			this.butOutputPatchFile.TabIndex = 13;
			this.butOutputPatchFile.UseVisualStyleBackColor = true;
			this.butOutputPatchFile.Click += new System.EventHandler(this.butOutputPatchFile_Click);
			// 
			// txtOutputPatchFile
			// 
			this.txtOutputPatchFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutputPatchFile.Location = new System.Drawing.Point(138, 91);
			this.txtOutputPatchFile.Name = "txtOutputPatchFile";
			this.txtOutputPatchFile.Size = new System.Drawing.Size(348, 20);
			this.txtOutputPatchFile.TabIndex = 12;
			// 
			// lblOriginalSourceFolder
			// 
			this.lblOriginalSourceFolder.AutoSize = true;
			this.lblOriginalSourceFolder.Location = new System.Drawing.Point(18, 42);
			this.lblOriginalSourceFolder.Name = "lblOriginalSourceFolder";
			this.lblOriginalSourceFolder.Size = new System.Drawing.Size(114, 13);
			this.lblOriginalSourceFolder.TabIndex = 7;
			this.lblOriginalSourceFolder.Text = "Original Source Folder:";
			// 
			// butOpenOriginalSourceFolder
			// 
			this.butOpenOriginalSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOpenOriginalSourceFolder.Image = ((System.Drawing.Image)(resources.GetObject("butOpenOriginalSourceFolder.Image")));
			this.butOpenOriginalSourceFolder.Location = new System.Drawing.Point(522, 37);
			this.butOpenOriginalSourceFolder.Name = "butOpenOriginalSourceFolder";
			this.butOpenOriginalSourceFolder.Size = new System.Drawing.Size(28, 24);
			this.butOpenOriginalSourceFolder.TabIndex = 10;
			this.butOpenOriginalSourceFolder.UseVisualStyleBackColor = true;
			this.butOpenOriginalSourceFolder.Click += new System.EventHandler(this.butOpenOriginalSourceFolder_Click);
			// 
			// butOriginalSourceFolder
			// 
			this.butOriginalSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOriginalSourceFolder.Image = ((System.Drawing.Image)(resources.GetObject("butOriginalSourceFolder.Image")));
			this.butOriginalSourceFolder.Location = new System.Drawing.Point(492, 37);
			this.butOriginalSourceFolder.Name = "butOriginalSourceFolder";
			this.butOriginalSourceFolder.Size = new System.Drawing.Size(28, 24);
			this.butOriginalSourceFolder.TabIndex = 9;
			this.butOriginalSourceFolder.UseVisualStyleBackColor = true;
			this.butOriginalSourceFolder.Click += new System.EventHandler(this.butOriginalSourceFolder_Click);
			// 
			// txtOriginalSourceFolder
			// 
			this.txtOriginalSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOriginalSourceFolder.Location = new System.Drawing.Point(138, 39);
			this.txtOriginalSourceFolder.Name = "txtOriginalSourceFolder";
			this.txtOriginalSourceFolder.Size = new System.Drawing.Size(348, 20);
			this.txtOriginalSourceFolder.TabIndex = 8;
			// 
			// lblModifiedSourceFolder
			// 
			this.lblModifiedSourceFolder.AutoSize = true;
			this.lblModifiedSourceFolder.Location = new System.Drawing.Point(13, 68);
			this.lblModifiedSourceFolder.Name = "lblModifiedSourceFolder";
			this.lblModifiedSourceFolder.Size = new System.Drawing.Size(119, 13);
			this.lblModifiedSourceFolder.TabIndex = 0;
			this.lblModifiedSourceFolder.Text = "Modified Source Folder:";
			// 
			// butOpenModifiedSourceFolder
			// 
			this.butOpenModifiedSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butOpenModifiedSourceFolder.Image = ((System.Drawing.Image)(resources.GetObject("butOpenModifiedSourceFolder.Image")));
			this.butOpenModifiedSourceFolder.Location = new System.Drawing.Point(522, 63);
			this.butOpenModifiedSourceFolder.Name = "butOpenModifiedSourceFolder";
			this.butOpenModifiedSourceFolder.Size = new System.Drawing.Size(28, 24);
			this.butOpenModifiedSourceFolder.TabIndex = 3;
			this.butOpenModifiedSourceFolder.UseVisualStyleBackColor = true;
			this.butOpenModifiedSourceFolder.Click += new System.EventHandler(this.butOpenModifiedSourceFolder_Click);
			// 
			// butCreateDiffPatch
			// 
			this.butCreateDiffPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butCreateDiffPatch.Image = ((System.Drawing.Image)(resources.GetObject("butCreateDiffPatch.Image")));
			this.butCreateDiffPatch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCreateDiffPatch.Location = new System.Drawing.Point(448, 132);
			this.butCreateDiffPatch.Name = "butCreateDiffPatch";
			this.butCreateDiffPatch.Size = new System.Drawing.Size(102, 24);
			this.butCreateDiffPatch.TabIndex = 6;
			this.butCreateDiffPatch.Text = "Create Patch";
			this.butCreateDiffPatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butCreateDiffPatch.UseVisualStyleBackColor = true;
			this.butCreateDiffPatch.Click += new System.EventHandler(this.butCreateDiffPatch_Click);
			// 
			// butModifiedSourceFolder
			// 
			this.butModifiedSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butModifiedSourceFolder.Image = ((System.Drawing.Image)(resources.GetObject("butModifiedSourceFolder.Image")));
			this.butModifiedSourceFolder.Location = new System.Drawing.Point(492, 63);
			this.butModifiedSourceFolder.Name = "butModifiedSourceFolder";
			this.butModifiedSourceFolder.Size = new System.Drawing.Size(28, 24);
			this.butModifiedSourceFolder.TabIndex = 2;
			this.butModifiedSourceFolder.UseVisualStyleBackColor = true;
			this.butModifiedSourceFolder.Click += new System.EventHandler(this.butModifiedSourceFolder_Click);
			// 
			// txtModifiedSourceFolder
			// 
			this.txtModifiedSourceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtModifiedSourceFolder.Location = new System.Drawing.Point(138, 65);
			this.txtModifiedSourceFolder.Name = "txtModifiedSourceFolder";
			this.txtModifiedSourceFolder.Size = new System.Drawing.Size(348, 20);
			this.txtModifiedSourceFolder.TabIndex = 1;
			// 
			// tabBuild
			// 
			this.tabBuild.Controls.Add(this.grpBuild);
			this.tabBuild.ImageIndex = 4;
			this.tabBuild.Location = new System.Drawing.Point(4, 23);
			this.tabBuild.Name = "tabBuild";
			this.tabBuild.Padding = new System.Windows.Forms.Padding(3);
			this.tabBuild.Size = new System.Drawing.Size(592, 176);
			this.tabBuild.TabIndex = 1;
			this.tabBuild.Text = "Build Options";
			this.tabBuild.UseVisualStyleBackColor = true;
			// 
			// grpBuild
			// 
			this.grpBuild.Controls.Add(this.pnlBuild);
			this.grpBuild.Controls.Add(this.lblProcesor);
			this.grpBuild.Controls.Add(this.cboOptimize);
			this.grpBuild.Controls.Add(this.lblTargetOS);
			this.grpBuild.Controls.Add(this.cboTargetOS);
			this.grpBuild.Controls.Add(this.cboTarget);
			this.grpBuild.Controls.Add(this.lblOSD);
			this.grpBuild.Controls.Add(this.lblTarget);
			this.grpBuild.Controls.Add(this.cboOSD);
			this.grpBuild.Controls.Add(this.cboSubTarget);
			this.grpBuild.Controls.Add(this.lblSubTarget);
			this.grpBuild.Controls.Add(this.lblOptimizeLevel);
			this.grpBuild.Controls.Add(this.cboOptimizeLevel);
			this.grpBuild.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpBuild.Location = new System.Drawing.Point(3, 3);
			this.grpBuild.Name = "grpBuild";
			this.grpBuild.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
			this.grpBuild.Size = new System.Drawing.Size(586, 170);
			this.grpBuild.TabIndex = 0;
			this.grpBuild.TabStop = false;
			this.grpBuild.Text = "Build Options";
			// 
			// pnlBuild
			// 
			this.pnlBuild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlBuild.AutoScroll = true;
			this.pnlBuild.Location = new System.Drawing.Point(249, 18);
			this.pnlBuild.Name = "pnlBuild";
			this.pnlBuild.Size = new System.Drawing.Size(326, 142);
			this.pnlBuild.TabIndex = 33;
			// 
			// lblProcesor
			// 
			this.lblProcesor.AutoSize = true;
			this.lblProcesor.Location = new System.Drawing.Point(24, 22);
			this.lblProcesor.Name = "lblProcesor";
			this.lblProcesor.Size = new System.Drawing.Size(65, 13);
			this.lblProcesor.TabIndex = 0;
			this.lblProcesor.Text = "Optimize for:";
			// 
			// cboOptimize
			// 
			this.cboOptimize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOptimize.FormattingEnabled = true;
			this.cboOptimize.Location = new System.Drawing.Point(93, 18);
			this.cboOptimize.Name = "cboOptimize";
			this.cboOptimize.Size = new System.Drawing.Size(150, 21);
			this.cboOptimize.TabIndex = 1;
			// 
			// lblTargetOS
			// 
			this.lblTargetOS.AutoSize = true;
			this.lblTargetOS.Location = new System.Drawing.Point(31, 70);
			this.lblTargetOS.Name = "lblTargetOS";
			this.lblTargetOS.Size = new System.Drawing.Size(59, 13);
			this.lblTargetOS.TabIndex = 4;
			this.lblTargetOS.Text = "Target OS:";
			// 
			// cboTargetOS
			// 
			this.cboTargetOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTargetOS.FormattingEnabled = true;
			this.cboTargetOS.Location = new System.Drawing.Point(93, 66);
			this.cboTargetOS.Name = "cboTargetOS";
			this.cboTargetOS.Size = new System.Drawing.Size(150, 21);
			this.cboTargetOS.TabIndex = 5;
			// 
			// cboTarget
			// 
			this.cboTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTarget.FormattingEnabled = true;
			this.cboTarget.Location = new System.Drawing.Point(93, 91);
			this.cboTarget.Name = "cboTarget";
			this.cboTarget.Size = new System.Drawing.Size(150, 21);
			this.cboTarget.TabIndex = 7;
			// 
			// lblOSD
			// 
			this.lblOSD.AutoSize = true;
			this.lblOSD.Location = new System.Drawing.Point(56, 143);
			this.lblOSD.Name = "lblOSD";
			this.lblOSD.Size = new System.Drawing.Size(33, 13);
			this.lblOSD.TabIndex = 10;
			this.lblOSD.Text = "OSD:";
			// 
			// lblTarget
			// 
			this.lblTarget.AutoSize = true;
			this.lblTarget.Location = new System.Drawing.Point(49, 95);
			this.lblTarget.Name = "lblTarget";
			this.lblTarget.Size = new System.Drawing.Size(41, 13);
			this.lblTarget.TabIndex = 6;
			this.lblTarget.Text = "Target:";
			// 
			// cboOSD
			// 
			this.cboOSD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOSD.FormattingEnabled = true;
			this.cboOSD.Location = new System.Drawing.Point(93, 139);
			this.cboOSD.Name = "cboOSD";
			this.cboOSD.Size = new System.Drawing.Size(150, 21);
			this.cboOSD.TabIndex = 11;
			// 
			// cboSubTarget
			// 
			this.cboSubTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSubTarget.FormattingEnabled = true;
			this.cboSubTarget.Location = new System.Drawing.Point(93, 115);
			this.cboSubTarget.Name = "cboSubTarget";
			this.cboSubTarget.Size = new System.Drawing.Size(150, 21);
			this.cboSubTarget.TabIndex = 9;
			// 
			// lblSubTarget
			// 
			this.lblSubTarget.AutoSize = true;
			this.lblSubTarget.Location = new System.Drawing.Point(27, 119);
			this.lblSubTarget.Name = "lblSubTarget";
			this.lblSubTarget.Size = new System.Drawing.Size(63, 13);
			this.lblSubTarget.TabIndex = 8;
			this.lblSubTarget.Text = "Sub Target:";
			// 
			// lblOptimizeLevel
			// 
			this.lblOptimizeLevel.AutoSize = true;
			this.lblOptimizeLevel.Location = new System.Drawing.Point(11, 46);
			this.lblOptimizeLevel.Name = "lblOptimizeLevel";
			this.lblOptimizeLevel.Size = new System.Drawing.Size(79, 13);
			this.lblOptimizeLevel.TabIndex = 2;
			this.lblOptimizeLevel.Text = "Optimize Level:";
			// 
			// cboOptimizeLevel
			// 
			this.cboOptimizeLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOptimizeLevel.FormattingEnabled = true;
			this.cboOptimizeLevel.Location = new System.Drawing.Point(93, 42);
			this.cboOptimizeLevel.Name = "cboOptimizeLevel";
			this.cboOptimizeLevel.Size = new System.Drawing.Size(150, 21);
			this.cboOptimizeLevel.TabIndex = 3;
			// 
			// tabHelp
			// 
			this.tabHelp.Controls.Add(this.grpHelp);
			this.tabHelp.ImageIndex = 5;
			this.tabHelp.Location = new System.Drawing.Point(4, 23);
			this.tabHelp.Name = "tabHelp";
			this.tabHelp.Padding = new System.Windows.Forms.Padding(3);
			this.tabHelp.Size = new System.Drawing.Size(592, 176);
			this.tabHelp.TabIndex = 8;
			this.tabHelp.Text = "Help";
			this.tabHelp.UseVisualStyleBackColor = true;
			// 
			// grpHelp
			// 
			this.grpHelp.Controls.Add(this.wbHelp);
			this.grpHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpHelp.Location = new System.Drawing.Point(3, 3);
			this.grpHelp.Name = "grpHelp";
			this.grpHelp.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
			this.grpHelp.Size = new System.Drawing.Size(586, 170);
			this.grpHelp.TabIndex = 0;
			this.grpHelp.TabStop = false;
			this.grpHelp.Text = "Help";
			// 
			// wbHelp
			// 
			this.wbHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wbHelp.Location = new System.Drawing.Point(8, 17);
			this.wbHelp.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbHelp.Name = "wbHelp";
			this.wbHelp.Size = new System.Drawing.Size(570, 145);
			this.wbHelp.TabIndex = 0;
			this.wbHelp.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbHelp_DocumentCompleted);
			this.wbHelp.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbHelp_Navigating);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Folder.png");
			this.imageList1.Images.SetKeyName(1, "Download_Small.png");
			this.imageList1.Images.SetKeyName(2, "DiffPatch.png");
			this.imageList1.Images.SetKeyName(3, "General.png");
			this.imageList1.Images.SetKeyName(4, "Target.png");
			this.imageList1.Images.SetKeyName(5, "Help.png");
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 411);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tabOptions);
			this.Controls.Add(this.butGo);
			this.Controls.Add(this.grpConsoleOutput);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(640, 400);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MAME Compiler 64 [VERSION]";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.grpConsoleOutput.ResumeLayout(false);
			this.cmsCompileOutput.ResumeLayout(false);
			this.grpGeneral.ResumeLayout(false);
			this.grpGeneral.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabOptions.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabDownloads.ResumeLayout(false);
			this.grpDownloads.ResumeLayout(false);
			this.cmsDownloads.ResumeLayout(false);
			this.tabApplyPatch.ResumeLayout(false);
			this.grpApplyPatch.ResumeLayout(false);
			this.grpApplyPatch.PerformLayout();
			this.tabCreatePatch.ResumeLayout(false);
			this.grpCreatePatch.ResumeLayout(false);
			this.grpCreatePatch.PerformLayout();
			this.tabBuild.ResumeLayout(false);
			this.grpBuild.ResumeLayout(false);
			this.grpBuild.PerformLayout();
			this.tabHelp.ResumeLayout(false);
			this.grpHelp.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.GroupBox grpConsoleOutput;
        private System.Windows.Forms.Button butGo;
		private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.CheckBox chkCleanCompile;
        private System.Windows.Forms.RichTextBox rtbConsoleOutput;
        private System.Windows.Forms.ToolStripStatusLabel lblElapsed;
        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabBuild;
		private System.Windows.Forms.Label lblCompilerParallelJobs;
		private System.Windows.Forms.ComboBox cboCompilerParallelJobs;
		private System.Windows.Forms.TabPage tabDownloads;
		private System.Windows.Forms.TabPage tabApplyPatch;
		private System.Windows.Forms.GroupBox grpApplyPatch;
		private System.Windows.Forms.Label lblDiffPatchFile;
		private System.Windows.Forms.Button butReverseDiffPatch;
		private System.Windows.Forms.Button butOpenDiffPatchFolder;
		private System.Windows.Forms.Button butApplyDiffPatch;
		private System.Windows.Forms.Button butDiffPatchFile;
		private System.Windows.Forms.TextBox txtDiffPatchFile;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.GroupBox grpDownloads;
		private System.Windows.Forms.ListView lvwDownloads;
		private System.Windows.Forms.Button butOpenMAMESourceFolder;
		private System.Windows.Forms.Button butMAMESourceFolder;
		private System.Windows.Forms.Button butOpenBuildToolsFolder;
		private System.Windows.Forms.TextBox txtMAMESourceFolder;
		private System.Windows.Forms.Button butBuildToolsFolder;
		private System.Windows.Forms.TextBox txtBuildToolsFolder;
		private System.Windows.Forms.GroupBox grpBuild;
		private System.Windows.Forms.Label lblProcesor;
		private System.Windows.Forms.ComboBox cboOptimize;
		private System.Windows.Forms.Label lblTargetOS;
		private System.Windows.Forms.ComboBox cboTargetOS;
		private System.Windows.Forms.ComboBox cboTarget;
		private System.Windows.Forms.Label lblOSD;
		private System.Windows.Forms.Label lblTarget;
		private System.Windows.Forms.ComboBox cboOSD;
		private System.Windows.Forms.ComboBox cboSubTarget;
		private System.Windows.Forms.Label lblSubTarget;
		private System.Windows.Forms.Label lblOptimizeLevel;
		private System.Windows.Forms.ComboBox cboOptimizeLevel;
		private System.Windows.Forms.Label lblMAMESourceFolder;
		private System.Windows.Forms.Label lblBuildToolsFolder;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colDescription;
		private System.Windows.Forms.ColumnHeader colUrl;
		private System.Windows.Forms.ContextMenuStrip cmsCompileOutput;
		private System.Windows.Forms.ToolStripMenuItem copyAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copySelectedTextToClipboardToolStripMenuItem;
		private System.Windows.Forms.Button butDownloadFileList;
		private System.Windows.Forms.Button butDownloadSelected;
		private System.Windows.Forms.ColumnHeader colType;
		private System.Windows.Forms.Label lblPatchFolder;
		private System.Windows.Forms.Button butOpenPatchFolder;
		private System.Windows.Forms.Button butPatchFolder;
		private System.Windows.Forms.TextBox txtPatchFolder;
		private System.Windows.Forms.Label lblSourceFolder;
		private System.Windows.Forms.Button butOpenSourceFolder;
		private System.Windows.Forms.Button butSourceFolder;
		private System.Windows.Forms.TextBox txtSourceFolder;
		private System.Windows.Forms.TabPage tabHelp;
		private System.Windows.Forms.GroupBox grpHelp;
		private System.Windows.Forms.ContextMenuStrip cmsDownloads;
		private System.Windows.Forms.ToolStripMenuItem copyLinkStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel tsslMAMELogo;
		private System.Windows.Forms.Button butUpdateCompileTools;
		private System.Windows.Forms.Button butUpdateMAMECompiler;
		private System.Windows.Forms.WebBrowser wbHelp;
		private System.Windows.Forms.Button butTestDiffPatch;
		private System.Windows.Forms.Panel pnlBuild;
		private System.Windows.Forms.TabPage tabCreatePatch;
		private System.Windows.Forms.GroupBox grpCreatePatch;
		private System.Windows.Forms.Label lblModifiedSourceFolder;
		private System.Windows.Forms.Button butOpenModifiedSourceFolder;
		private System.Windows.Forms.Button butCreateDiffPatch;
		private System.Windows.Forms.Button butModifiedSourceFolder;
		private System.Windows.Forms.TextBox txtModifiedSourceFolder;
		private System.Windows.Forms.Label lblOriginalSourceFolder;
		private System.Windows.Forms.Button butOpenOriginalSourceFolder;
		private System.Windows.Forms.Button butOriginalSourceFolder;
		private System.Windows.Forms.TextBox txtOriginalSourceFolder;
		private System.Windows.Forms.Label lblOutputPatchFile;
		private System.Windows.Forms.Button butOpenOutputPatchFile;
		private System.Windows.Forms.Button butOutputPatchFile;
		private System.Windows.Forms.TextBox txtOutputPatchFile;
		private System.Windows.Forms.ComboBox cboPrefixStrip;
		private System.Windows.Forms.Label lblPrefixStrip;
		private System.Windows.Forms.CheckBox chkEOLConversion;
    }
}


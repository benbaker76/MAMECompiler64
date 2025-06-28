namespace MAMECompiler64
{
    partial class frmDownload
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownload));
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.pbFileProgress = new System.Windows.Forms.ProgressBar();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.downloadLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblFileName = new System.Windows.Forms.Label();
			this.lblDownloading = new System.Windows.Forms.Label();
			this.lblFileSize = new System.Windows.Forms.Label();
			this.lblSize = new System.Windows.Forms.Label();
			this.lblSpeed = new System.Windows.Forms.Label();
			this.lblFileSpeed = new System.Windows.Forms.Label();
			this.lblFileProgress = new System.Windows.Forms.Label();
			this.picImage = new System.Windows.Forms.PictureBox();
			this.grpFileProgress = new System.Windows.Forms.GroupBox();
			this.grpFileInfo = new System.Windows.Forms.GroupBox();
			this.grpTotalProgress = new System.Windows.Forms.GroupBox();
			this.lblTotalProgress = new System.Windows.Forms.Label();
			this.pbTotalProgress = new System.Windows.Forms.ProgressBar();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
			this.grpFileProgress.SuspendLayout();
			this.grpFileInfo.SuspendLayout();
			this.grpTotalProgress.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(14, 275);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(107, 27);
			this.butOK.TabIndex = 0;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(315, 275);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(107, 27);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// pbFileProgress
			// 
			this.pbFileProgress.Location = new System.Drawing.Point(11, 20);
			this.pbFileProgress.Name = "pbFileProgress";
			this.pbFileProgress.Size = new System.Drawing.Size(386, 23);
			this.pbFileProgress.TabIndex = 3;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 313);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(434, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// downloadLabel
			// 
			this.downloadLabel.Name = "downloadLabel";
			this.downloadLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// lblFileName
			// 
			this.lblFileName.AutoSize = true;
			this.lblFileName.Location = new System.Drawing.Point(91, 23);
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new System.Drawing.Size(0, 13);
			this.lblFileName.TabIndex = 5;
			// 
			// lblDownloading
			// 
			this.lblDownloading.AutoSize = true;
			this.lblDownloading.Location = new System.Drawing.Point(14, 23);
			this.lblDownloading.Name = "lblDownloading";
			this.lblDownloading.Size = new System.Drawing.Size(72, 13);
			this.lblDownloading.TabIndex = 19;
			this.lblDownloading.Text = "Downloading:";
			// 
			// lblFileSize
			// 
			this.lblFileSize.AutoSize = true;
			this.lblFileSize.Location = new System.Drawing.Point(14, 41);
			this.lblFileSize.Name = "lblFileSize";
			this.lblFileSize.Size = new System.Drawing.Size(49, 13);
			this.lblFileSize.TabIndex = 20;
			this.lblFileSize.Text = "File Size:";
			// 
			// lblSize
			// 
			this.lblSize.AutoSize = true;
			this.lblSize.Location = new System.Drawing.Point(68, 41);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(0, 13);
			this.lblSize.TabIndex = 21;
			// 
			// lblSpeed
			// 
			this.lblSpeed.AutoSize = true;
			this.lblSpeed.Location = new System.Drawing.Point(58, 59);
			this.lblSpeed.Name = "lblSpeed";
			this.lblSpeed.Size = new System.Drawing.Size(0, 13);
			this.lblSpeed.TabIndex = 23;
			// 
			// lblFileSpeed
			// 
			this.lblFileSpeed.AutoSize = true;
			this.lblFileSpeed.Location = new System.Drawing.Point(14, 59);
			this.lblFileSpeed.Name = "lblFileSpeed";
			this.lblFileSpeed.Size = new System.Drawing.Size(41, 13);
			this.lblFileSpeed.TabIndex = 22;
			this.lblFileSpeed.Text = "Speed:";
			// 
			// lblFileProgress
			// 
			this.lblFileProgress.AutoSize = true;
			this.lblFileProgress.Location = new System.Drawing.Point(11, 50);
			this.lblFileProgress.Name = "lblFileProgress";
			this.lblFileProgress.Size = new System.Drawing.Size(0, 13);
			this.lblFileProgress.TabIndex = 24;
			// 
			// picImage
			// 
			this.picImage.Image = ((System.Drawing.Image)(resources.GetObject("picImage.Image")));
			this.picImage.Location = new System.Drawing.Point(9, 12);
			this.picImage.Name = "picImage";
			this.picImage.Size = new System.Drawing.Size(48, 48);
			this.picImage.TabIndex = 25;
			this.picImage.TabStop = false;
			// 
			// grpFileProgress
			// 
			this.grpFileProgress.Controls.Add(this.lblFileProgress);
			this.grpFileProgress.Controls.Add(this.pbFileProgress);
			this.grpFileProgress.Location = new System.Drawing.Point(14, 109);
			this.grpFileProgress.Name = "grpFileProgress";
			this.grpFileProgress.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
			this.grpFileProgress.Size = new System.Drawing.Size(408, 77);
			this.grpFileProgress.TabIndex = 26;
			this.grpFileProgress.TabStop = false;
			this.grpFileProgress.Text = "File Progress";
			// 
			// grpFileInfo
			// 
			this.grpFileInfo.Controls.Add(this.lblSpeed);
			this.grpFileInfo.Controls.Add(this.lblFileSpeed);
			this.grpFileInfo.Controls.Add(this.lblSize);
			this.grpFileInfo.Controls.Add(this.lblFileSize);
			this.grpFileInfo.Controls.Add(this.lblDownloading);
			this.grpFileInfo.Controls.Add(this.lblFileName);
			this.grpFileInfo.Location = new System.Drawing.Point(69, 12);
			this.grpFileInfo.Name = "grpFileInfo";
			this.grpFileInfo.Size = new System.Drawing.Size(353, 91);
			this.grpFileInfo.TabIndex = 27;
			this.grpFileInfo.TabStop = false;
			this.grpFileInfo.Text = "File Info";
			// 
			// grpTotalProgress
			// 
			this.grpTotalProgress.Controls.Add(this.lblTotalProgress);
			this.grpTotalProgress.Controls.Add(this.pbTotalProgress);
			this.grpTotalProgress.Location = new System.Drawing.Point(14, 192);
			this.grpTotalProgress.Name = "grpTotalProgress";
			this.grpTotalProgress.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
			this.grpTotalProgress.Size = new System.Drawing.Size(408, 77);
			this.grpTotalProgress.TabIndex = 28;
			this.grpTotalProgress.TabStop = false;
			this.grpTotalProgress.Text = "Total Progress";
			// 
			// lblTotalProgress
			// 
			this.lblTotalProgress.AutoSize = true;
			this.lblTotalProgress.Location = new System.Drawing.Point(11, 50);
			this.lblTotalProgress.Name = "lblTotalProgress";
			this.lblTotalProgress.Size = new System.Drawing.Size(0, 13);
			this.lblTotalProgress.TabIndex = 24;
			// 
			// pbTotalProgress
			// 
			this.pbTotalProgress.Location = new System.Drawing.Point(11, 20);
			this.pbTotalProgress.Name = "pbTotalProgress";
			this.pbTotalProgress.Size = new System.Drawing.Size(386, 23);
			this.pbTotalProgress.TabIndex = 3;
			// 
			// frmDownload
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 335);
			this.ControlBox = false;
			this.Controls.Add(this.grpTotalProgress);
			this.Controls.Add(this.grpFileInfo);
			this.Controls.Add(this.grpFileProgress);
			this.Controls.Add(this.picImage);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmDownload";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Download";
			this.Load += new System.EventHandler(this.frmDownload_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDownload_FormClosing);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
			this.grpFileProgress.ResumeLayout(false);
			this.grpFileProgress.PerformLayout();
			this.grpFileInfo.ResumeLayout(false);
			this.grpFileInfo.PerformLayout();
			this.grpTotalProgress.ResumeLayout(false);
			this.grpTotalProgress.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.ProgressBar pbFileProgress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel downloadLabel;
		private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblDownloading;
        private System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblFileSpeed;
        private System.Windows.Forms.Label lblFileProgress;
		private System.Windows.Forms.PictureBox picImage;
		private System.Windows.Forms.GroupBox grpFileProgress;
		private System.Windows.Forms.GroupBox grpFileInfo;
		private System.Windows.Forms.GroupBox grpTotalProgress;
		private System.Windows.Forms.Label lblTotalProgress;
		private System.Windows.Forms.ProgressBar pbTotalProgress;
    }
}
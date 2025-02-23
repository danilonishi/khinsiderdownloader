namespace KHInsiderDownloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.selectDownloadFolderButton = new System.Windows.Forms.Button();
            this.localFolderLabel = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.urlFeedbackLabel = new System.Windows.Forms.Label();
            this.fetchSoundtrackButton = new System.Windows.Forms.Button();
            this.downloadListBox = new System.Windows.Forms.ListBox();
            this.downloadButton = new System.Windows.Forms.Button();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.selectNoneButton = new System.Windows.Forms.Button();
            this.invertSelectionButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.customAlbumTextBox = new System.Windows.Forms.TextBox();
            this.openDownloadsButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectDownloadFolderButton
            // 
            this.selectDownloadFolderButton.Location = new System.Drawing.Point(12, 12);
            this.selectDownloadFolderButton.Name = "selectDownloadFolderButton";
            this.selectDownloadFolderButton.Size = new System.Drawing.Size(143, 23);
            this.selectDownloadFolderButton.TabIndex = 0;
            this.selectDownloadFolderButton.Text = "Select Download Folder";
            this.selectDownloadFolderButton.UseVisualStyleBackColor = true;
            this.selectDownloadFolderButton.Click += new System.EventHandler(this.selectDownloadFolderButton_Click);
            // 
            // localFolderLabel
            // 
            this.localFolderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.localFolderLabel.AutoSize = true;
            this.localFolderLabel.Location = new System.Drawing.Point(161, 17);
            this.localFolderLabel.Name = "localFolderLabel";
            this.localFolderLabel.Size = new System.Drawing.Size(93, 13);
            this.localFolderLabel.TabIndex = 1;
            this.localFolderLabel.Text = "Download Folder: ";
            // 
            // urlTextBox
            // 
            this.urlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urlTextBox.Location = new System.Drawing.Point(12, 41);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(1071, 20);
            this.urlTextBox.TabIndex = 2;
            this.urlTextBox.Text = "Please paste a KH Insider album URL.";
            this.urlTextBox.TextChanged += new System.EventHandler(this.urlTextBox_TextChanged);
            // 
            // urlFeedbackLabel
            // 
            this.urlFeedbackLabel.AutoSize = true;
            this.urlFeedbackLabel.Location = new System.Drawing.Point(12, 64);
            this.urlFeedbackLabel.Name = "urlFeedbackLabel";
            this.urlFeedbackLabel.Size = new System.Drawing.Size(35, 13);
            this.urlFeedbackLabel.TabIndex = 4;
            this.urlFeedbackLabel.Text = "label3";
            // 
            // fetchSoundtrackButton
            // 
            this.fetchSoundtrackButton.Enabled = false;
            this.fetchSoundtrackButton.Location = new System.Drawing.Point(15, 80);
            this.fetchSoundtrackButton.Name = "fetchSoundtrackButton";
            this.fetchSoundtrackButton.Size = new System.Drawing.Size(140, 23);
            this.fetchSoundtrackButton.TabIndex = 5;
            this.fetchSoundtrackButton.Text = "Fetch Soundtrack";
            this.fetchSoundtrackButton.UseVisualStyleBackColor = true;
            this.fetchSoundtrackButton.Click += new System.EventHandler(this.fetchSoundtrackButton_Click);
            // 
            // downloadListBox
            // 
            this.downloadListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadListBox.FormattingEnabled = true;
            this.downloadListBox.HorizontalScrollbar = true;
            this.downloadListBox.Location = new System.Drawing.Point(15, 109);
            this.downloadListBox.Name = "downloadListBox";
            this.downloadListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.downloadListBox.Size = new System.Drawing.Size(1068, 511);
            this.downloadListBox.TabIndex = 6;
            this.downloadListBox.SelectedValueChanged += new System.EventHandler(this.downloadListBoxSelectedValueChanged);
            this.downloadListBox.SizeChanged += new System.EventHandler(this.downloadListBoxSizeChanged);
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadButton.Enabled = false;
            this.downloadButton.Location = new System.Drawing.Point(953, 632);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(130, 59);
            this.downloadButton.TabIndex = 7;
            this.downloadButton.Text = "Download Selected Files";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectAllButton.Enabled = false;
            this.selectAllButton.Location = new System.Drawing.Point(15, 632);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(75, 23);
            this.selectAllButton.TabIndex = 8;
            this.selectAllButton.Text = "All";
            this.selectAllButton.UseVisualStyleBackColor = true;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // selectNoneButton
            // 
            this.selectNoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectNoneButton.Enabled = false;
            this.selectNoneButton.Location = new System.Drawing.Point(96, 632);
            this.selectNoneButton.Name = "selectNoneButton";
            this.selectNoneButton.Size = new System.Drawing.Size(75, 23);
            this.selectNoneButton.TabIndex = 9;
            this.selectNoneButton.Text = "None";
            this.selectNoneButton.UseVisualStyleBackColor = true;
            this.selectNoneButton.Click += new System.EventHandler(this.selectNoneButton_Click);
            // 
            // invertSelectionButton
            // 
            this.invertSelectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.invertSelectionButton.Enabled = false;
            this.invertSelectionButton.Location = new System.Drawing.Point(177, 632);
            this.invertSelectionButton.Name = "invertSelectionButton";
            this.invertSelectionButton.Size = new System.Drawing.Size(92, 23);
            this.invertSelectionButton.TabIndex = 10;
            this.invertSelectionButton.Text = "Invert";
            this.invertSelectionButton.UseVisualStyleBackColor = true;
            this.invertSelectionButton.Click += new System.EventHandler(this.invertSelectionButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(15, 668);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(932, 23);
            this.progressBar.TabIndex = 11;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 694);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1095, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel.Text = "Ready.";
            // 
            // customAlbumTextBox
            // 
            this.customAlbumTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customAlbumTextBox.Location = new System.Drawing.Point(162, 83);
            this.customAlbumTextBox.Name = "customAlbumTextBox";
            this.customAlbumTextBox.Size = new System.Drawing.Size(921, 20);
            this.customAlbumTextBox.TabIndex = 13;
            this.customAlbumTextBox.Text = "Custom Album Name";
            this.customAlbumTextBox.TextChanged += new System.EventHandler(this.customAlbumTextBox_TextChanged);
            this.customAlbumTextBox.Enter += new System.EventHandler(this.customAlbumTextBox_Enter);
            this.customAlbumTextBox.Leave += new System.EventHandler(this.customAlbumTextBox_Leave);
            // 
            // openDownloadsButton
            // 
            this.openDownloadsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openDownloadsButton.Location = new System.Drawing.Point(981, 12);
            this.openDownloadsButton.Name = "openDownloadsButton";
            this.openDownloadsButton.Size = new System.Drawing.Size(102, 23);
            this.openDownloadsButton.TabIndex = 14;
            this.openDownloadsButton.Text = "Open Downloads";
            this.openDownloadsButton.UseVisualStyleBackColor = true;
            this.openDownloadsButton.Click += new System.EventHandler(this.openDownloadsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 716);
            this.Controls.Add(this.openDownloadsButton);
            this.Controls.Add(this.customAlbumTextBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.invertSelectionButton);
            this.Controls.Add(this.selectNoneButton);
            this.Controls.Add(this.selectAllButton);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.downloadListBox);
            this.Controls.Add(this.fetchSoundtrackButton);
            this.Controls.Add(this.urlFeedbackLabel);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.localFolderLabel);
            this.Controls.Add(this.selectDownloadFolderButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "Form1";
            this.Text = "KHInsider Album Downloader";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button selectDownloadFolderButton;
		private System.Windows.Forms.Label localFolderLabel;
		private System.Windows.Forms.TextBox urlTextBox;
		private System.Windows.Forms.Label urlFeedbackLabel;
		private System.Windows.Forms.Button fetchSoundtrackButton;
		private System.Windows.Forms.ListBox downloadListBox;
		private System.Windows.Forms.Button downloadButton;
		private System.Windows.Forms.Button selectAllButton;
		private System.Windows.Forms.Button selectNoneButton;
		private System.Windows.Forms.Button invertSelectionButton;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.TextBox customAlbumTextBox;
		private System.Windows.Forms.Button openDownloadsButton;
	}
}


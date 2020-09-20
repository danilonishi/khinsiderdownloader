namespace KHInsiderDownloader
{
	partial class AlbumTrackSelector
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumTrackSelector));
			this.tracksListBox = new System.Windows.Forms.ListBox();
			this.enqueueButton = new System.Windows.Forms.Button();
			this.pageUrlTextBox = new System.Windows.Forms.TextBox();
			this.fetchButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tracksListBox
			// 
			this.tracksListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tracksListBox.FormattingEnabled = true;
			this.tracksListBox.Location = new System.Drawing.Point(12, 41);
			this.tracksListBox.Name = "tracksListBox";
			this.tracksListBox.Size = new System.Drawing.Size(462, 381);
			this.tracksListBox.TabIndex = 0;
			// 
			// enqueueButton
			// 
			this.enqueueButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.enqueueButton.Location = new System.Drawing.Point(12, 432);
			this.enqueueButton.Name = "enqueueButton";
			this.enqueueButton.Size = new System.Drawing.Size(462, 23);
			this.enqueueButton.TabIndex = 1;
			this.enqueueButton.Text = "Enqueue Selected";
			this.enqueueButton.UseVisualStyleBackColor = true;
			// 
			// pageUrlTextBox
			// 
			this.pageUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pageUrlTextBox.Location = new System.Drawing.Point(12, 12);
			this.pageUrlTextBox.Name = "pageUrlTextBox";
			this.pageUrlTextBox.Size = new System.Drawing.Size(381, 20);
			this.pageUrlTextBox.TabIndex = 2;
			// 
			// fetchButton
			// 
			this.fetchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.fetchButton.Location = new System.Drawing.Point(399, 12);
			this.fetchButton.Name = "fetchButton";
			this.fetchButton.Size = new System.Drawing.Size(75, 23);
			this.fetchButton.TabIndex = 3;
			this.fetchButton.Text = "button2";
			this.fetchButton.UseVisualStyleBackColor = true;
			// 
			// AlbumTrackSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(486, 467);
			this.Controls.Add(this.fetchButton);
			this.Controls.Add(this.pageUrlTextBox);
			this.Controls.Add(this.enqueueButton);
			this.Controls.Add(this.tracksListBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(270, 270);
			this.Name = "AlbumTrackSelector";
			this.Text = "Album Track Selector";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox tracksListBox;
		private System.Windows.Forms.Button enqueueButton;
		private System.Windows.Forms.TextBox pageUrlTextBox;
		private System.Windows.Forms.Button fetchButton;
	}
}
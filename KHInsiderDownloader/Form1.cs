using HtmlAgilityPack;
using KHInsiderDownloader.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.ListBox;

namespace KHInsiderDownloader
{
	public partial class Form1 : Form
	{
		//

		static string defaultDownloadPath = Environment.SpecialFolder.UserProfile + @"\Downloads";

		//

		MainPage mainPage = new MainPage();
		Downloader fileDownloader;
		string baseDownloadFolderPath = defaultDownloadPath;
		string completeDownloadFolderPath;
		string siteUrl = string.Empty;
		string urlAlbumName;
		string possibleAlbumName;

		//

		public Form1()
		{
			InitializeComponent();
			LoadSettings();
		}

		private void LoadSettings()
		{
			if (!string.IsNullOrEmpty(Settings.Default.downloadPath))
			{
				baseDownloadFolderPath = Settings.Default.downloadPath;
			}
			UpdateLocalDownloadPathLabel();

			if (!string.IsNullOrEmpty(Settings.Default.lastDownloadURL))
			{
				urlTextBox.Text = Settings.Default.lastDownloadURL;
			}
			ValidateURLTextBox(urlTextBox);
		}

		public void SelectDownloadFolder()
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			dialog.InitialDirectory = baseDownloadFolderPath;
			dialog.IsFolderPicker = true;
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				baseDownloadFolderPath = dialog.FileName;
			}
			else
			{
				baseDownloadFolderPath = defaultDownloadPath;
			}

			UpdateLocalDownloadPathLabel();
			Settings.Default.downloadPath = baseDownloadFolderPath;
			Settings.Default.Save();

		}

		private void UpdateLocalDownloadPathLabel()
		{
			//completeDownloadFolderPath
			string customAlbum = customAlbumTextBox.Text;

			if (!string.IsNullOrWhiteSpace(possibleAlbumName))
			{
				completeDownloadFolderPath = $"{baseDownloadFolderPath}\\{possibleAlbumName}";
			}
			else if (!string.IsNullOrWhiteSpace(urlAlbumName))
			{
				completeDownloadFolderPath = $"{baseDownloadFolderPath}\\{urlAlbumName}";
			}
			else if (!string.IsNullOrWhiteSpace(customAlbum))
			{
				completeDownloadFolderPath = $"{baseDownloadFolderPath}\\{customAlbum}";
			}
			else
			{
				completeDownloadFolderPath = baseDownloadFolderPath;
			}

			localFolderLabel.Text = $"Download Folder: {completeDownloadFolderPath}";
		}

		public void LoadMainPage()
		{
			toolStripStatusLabel.Text = "Loading song list...";
			Thread.Sleep(100);


			if (mainPage != null)
			{
				mainPage.Dispose();
			}

			mainPage = new MainPage();
			mainPage.LoadURL(siteUrl);
			mainPage.OnPageLoaded += HandlePageLoaded;
		}

        private void HandlePageLoaded()
        {
            UpdateDownloadListBox();
            toolStripStatusLabel.Text = "Ready.";

            possibleAlbumName = mainPage.possibleAlbumName;

            string txt = customAlbumTextBox.Text;
            UpdateLocalDownloadPathLabel();
            SetFetchSoundtrackButtonEnabled(true);
        }

        public void UpdateDownloadListBox()
		{
			downloadListBox.Items.Clear();
			var names = mainPage.GetFileNames();

			foreach (var name in names)
			{
				downloadListBox.Items.Add(name);
			}

			selectAllButton.Enabled = selectNoneButton.Enabled = invertSelectionButton.Enabled = mainPage.validHashset.Count > 0;
			SetDownloadButtonEnabled(false);
		}

		private void ValidateURLTextBox(TextBox sender)
		{
			string txt = sender.Text;
			if (!txt.StartsWith("https://downloads.khinsider.com/game-soundtracks/album/"))
			{
				urlFeedbackLabel.Text = "Please paste a KH Insider album URL.";
				urlFeedbackLabel.ForeColor = Color.Red;
				siteUrl = string.Empty;
				fetchSoundtrackButton.Enabled = false;
			}
			else if (txt.EndsWith(".mp3"))
			{
				urlFeedbackLabel.Text = "Please paste a KH Insider **album** URL.";
				urlFeedbackLabel.ForeColor = Color.Red;
				siteUrl = string.Empty;
				fetchSoundtrackButton.Enabled = false;
			}
			else if (string.IsNullOrEmpty(txt))
			{
				urlFeedbackLabel.Text = "Please paste a KH Insider album URL.";
				urlFeedbackLabel.ForeColor = Color.Red;
				siteUrl = string.Empty;
				fetchSoundtrackButton.Enabled = false;
			}
			else
			{
				urlFeedbackLabel.Text = "URL OK!";
				urlFeedbackLabel.ForeColor = Color.Black;
				siteUrl = txt;
				fetchSoundtrackButton.Enabled = true;
				urlAlbumName = txt.Split('/').Last();
				UpdateLocalDownloadPathLabel();

			}
			Settings.Default.lastDownloadURL = txt;
			Settings.Default.Save();
		}

		private void SetDownloadButtonEnabled(bool enabled)
		{
			if (downloadListBox.SelectedIndices.Count <= 0)
				enabled = false;

			if (fileDownloader != null)
			{
				if (fileDownloader.GetStatus() != DownloaderStatus.Ready)
				{
					enabled = false;
				}
			}

			downloadButton.Enabled = enabled;
		}

		private void SetFetchSoundtrackButtonEnabled(bool enabled)
		{
			fetchSoundtrackButton.Enabled = enabled;
        }


		// Callbacks

		private void selectDownloadFolderButton_Click(object sender, EventArgs e)
		{
			SelectDownloadFolder();
			UpdateLocalDownloadPathLabel();
		}

		private void urlTextBox_TextChanged(object sender, EventArgs e)
		{
			ValidateURLTextBox(sender as TextBox);
		}

		private void fetchSoundtrackButton_Click(object sender, EventArgs e)
		{
			SetFetchSoundtrackButtonEnabled(false);
            LoadMainPage();
		}

		private void downloadButton_Click(object sender, EventArgs e)
		{
			SetDownloadButtonEnabled(false);

			fileDownloader = new Downloader();
			fileDownloader.Completed += FileDownloader_Completed;
            fileDownloader.Canceled += FileDownloader_Cancelled;
            fileDownloader.ProgressChanged += FileDownloader_ProgressChanged;

			toolStripStatusLabel.Text = "Fetching Download File List. This may take a while.";

			int idx = 0;
			int count = downloadListBox.SelectedIndices.Count;
			foreach (int item in downloadListBox.SelectedIndices)
			{
				using (var page = new MP3Page())
				{
					//progressBar.Value = (int)(((float)++idx / (float)count) * 100);
					page.OnFileDownloadAvailable += HandlePageDownloadComplete;

                    page.GetFileDownload(mainPage.validLinks[item], completeDownloadFolderPath);
				}
			}
			toolStripStatusLabel.Text = $"Listing Completed. Downloading {downloadListBox.SelectedIndices.Count} files.";
		}

        private void HandlePageDownloadComplete(FileDownload download)
        {
			fileDownloader.Add(download);
        }

        private void FileDownloader_Cancelled(FileDownload download)
        {
            progressBar.Value = 0;
            SetDownloadButtonEnabled(true);
            fileDownloader.Dispose();
        }

        private void FileDownloader_ProgressChanged(int obj)
		{
			progressBar.Value = obj;
		}

		private void FileDownloader_Completed()
		{
			progressBar.Value = 100;
			SetDownloadButtonEnabled(true);
			fileDownloader.Dispose();

        }

		private void selectAllButton_Click(object sender, EventArgs e)
		{
			downloadListBox.SelectedItems.Clear();
			for (int i = 0; i < downloadListBox.Items.Count; i++)
			{
				downloadListBox.SetSelected(i, true);
			}
		}

		private void selectNoneButton_Click(object sender, EventArgs e)
		{
			downloadListBox.SelectedItems.Clear();
			for (int i = 0; i < downloadListBox.Items.Count; i++)
			{
				downloadListBox.SetSelected(i, false);
			}
		}

		private void invertSelectionButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < downloadListBox.Items.Count; i++)
			{
				downloadListBox.SetSelected(i, !downloadListBox.GetSelected(i));
			}
		}

		private void downloadListBoxSizeChanged(object sender, EventArgs e)
		{

		}

		private void downloadListBoxSelectedValueChanged(object sender, EventArgs e)
		{
			SetDownloadButtonEnabled(true);
		}

		private void customAlbumTextBox_TextChanged(object sender, EventArgs e)
		{
			Settings.Default.customAlbumName = (sender as TextBox).Text;
			Settings.Default.Save();
			UpdateLocalDownloadPathLabel();
		}

		private void openDownloadsButton_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(completeDownloadFolderPath))
			{
				Process.Start("explorer.exe", completeDownloadFolderPath);
			}
			else
			{
				int tries = 20;
				string path = Directory.GetParent(completeDownloadFolderPath).FullName;
				while (!Directory.Exists(path))
				{
					path = Directory.GetParent(completeDownloadFolderPath).FullName;
					tries--;
					if (tries == 0)
					{
						Console.WriteLine("Gave up searching for a folder");
						return;
					}
				}
				if (!string.IsNullOrWhiteSpace(path))
				{
					Process.Start("explorer.exe", path);
				}
			}
		}
	}
}

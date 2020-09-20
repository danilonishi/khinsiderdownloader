using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KHInsiderDownloader
{
	class FileDownload : IDisposable
	{
		private WebClient cli;
		private string localFilePath;
		private string remotePath;

		public event Action<FileDownload> Canceled;
		public event Action Completed;
		public event Action<int> ProgressChanged;

		public int Progress { get; private set; }

		public FileDownload(string remotePath, string localFilePath)
		{
			this.localFilePath = localFilePath;
			this.remotePath = remotePath;
			cli = new WebClient();
		}

		// Management

		public void DownloadAsync()
		{
			Debug.WriteLine("FD DownloadAsync");
			cli.DownloadProgressChanged += Cli_DownloadProgressChanged;
			cli.DownloadDataCompleted += Cli_DownloadDataCompleted;
			cli.DownloadDataAsync(new Uri(remotePath));
		}

		public void CancelAsync()
		{
			Debug.WriteLine("FD Cancel");
			cli.CancelAsync();
			Canceled(this);
		}

		// Callbacks

		private void Cli_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			Progress = e.ProgressPercentage;
			Debug.WriteLine("FD Progress:" + Progress);
			ProgressChanged(Progress);
		}

		private void Cli_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			File.WriteAllBytes(localFilePath, e.Result);
			Progress = 100;
			Debug.WriteLine("FD Completed.");
			Completed();
		}

		// IDisposable

		public void Dispose()
		{
			cli.Dispose();
			cli = null;
			Canceled = null;
			Completed = null;
			ProgressChanged = null;
		}
	}
}

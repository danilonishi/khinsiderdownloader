using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
            var fullPath = localFilePath;

			int fnIndex = fullPath.LastIndexOf('\\');
            int extIndex = fullPath.LastIndexOf('.');
			var basePath = fullPath.Substring(0, fnIndex+1);
			var fileName = localFilePath.Substring(fnIndex+1, extIndex - fnIndex-1);
            var ext = fullPath.Substring(extIndex);
            
            // Fix invalid chars in path
            var invalidChars = Path.GetInvalidFileNameChars();
			foreach(var c in invalidChars)
			{
                fileName = fileName.Replace(c, '_');
            }

			var finalFilePath = Path.Combine(basePath, fileName + ext);

            File.WriteAllBytes(finalFilePath, e.Result);
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

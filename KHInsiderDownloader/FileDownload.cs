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

		public event Action<FileDownload> Canceled = delegate { };
		public event Action Completed = delegate { };
		public event Action<int> ProgressChanged = delegate { };

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
			cli.DownloadProgressChanged += Cli_DownloadProgressChanged;
			cli.DownloadDataCompleted += Cli_DownloadDataCompleted;
			cli.DownloadDataAsync(new Uri(remotePath));
		}

		public void CancelAsync()
		{
			cli.CancelAsync();
			Canceled(this);
		}

		// Callbacks

		private void Cli_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			Progress = e.ProgressPercentage;
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
			Completed();
		}

		// IDisposable

		public void Dispose()
		{
            cli.DownloadProgressChanged -= Cli_DownloadProgressChanged;
            cli.DownloadDataCompleted -= Cli_DownloadDataCompleted;

            cli.Dispose();
			cli = null;
			localFilePath = null;
			remotePath = null;
            Canceled = null;
			Completed = null;
			ProgressChanged = null;
			GC.Collect();
		}
	}
}

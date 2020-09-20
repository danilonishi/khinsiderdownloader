using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHInsiderDownloader
{
	enum DownloaderStatus
	{
		Ready,
		Downloading
	}

	class Downloader : IDisposable
	{
		public event Action<FileDownload> Canceled;
		public event Action Completed;
		public event Action<int> ProgressChanged;

		private int count = 0;
		private int completed = 0;
		public List<FileDownload> downloads = new List<FileDownload>();

		public DownloaderStatus Status { get; private set; }

		public Downloader()
		{
			Status = DownloaderStatus.Ready;
		}

		// Management

		public void Add(FileDownload fd)
		{
			AddEvents(fd);
			downloads.Add(fd);
			count++;
		}

		public void Add(string remotePath, string localFilePath)
		{
			Add(new FileDownload(remotePath, localFilePath));
		}

		public void Cancel(FileDownload fd)
		{
			fd.CancelAsync();
		}

		public void CancelAll()
		{
			Debug.WriteLine("Cancelling all...");
			foreach (var d in downloads)
			{
				d.CancelAsync();
			}
			Clear();
		}

		public void Clear()
		{
			count = completed = 0;
			foreach (var d in downloads)
			{
				d.ProgressChanged -= Download_ProgressChanged;
				d.Completed -= Download_Completed;
				d.Dispose();
			}
			downloads.Clear();
		}

		public void StartAll()
		{
			Debug.WriteLine("Starting all...");
			Status = DownloaderStatus.Downloading;
			foreach (var fd in downloads)
			{
				fd.DownloadAsync();
			}
		}

		private void CheckFinished()
		{
			if (count == completed)
			{
				Debug.WriteLine("Process Finished.");
				Completed();
				downloads.Clear();
				Status = DownloaderStatus.Ready;
			}
		}

		// Events

		private void AddEvents(FileDownload fd)
		{
			fd.ProgressChanged += Download_ProgressChanged;
			fd.Completed += Download_Completed;
			fd.Canceled += Download_Canceled;
		}

		private void RemoveEvents(FileDownload fd)
		{
			fd.ProgressChanged += Download_ProgressChanged;
			fd.Completed += Download_Completed;
			fd.Canceled += Download_Canceled;
		}

		//Callbacks

		private void Download_Canceled(FileDownload fd)
		{
			Canceled(fd);
			RemoveEvents(fd);
			downloads.Remove(fd);
			count--;
			CheckFinished();
		}

		private void Download_Completed()
		{
			completed++;
			CheckFinished();
		}

		private void Download_ProgressChanged(int obj)
		{
			int progressSum = downloads.Sum(item => item.Progress);
			int progress = progressSum / count;
			Debug.WriteLine($"Progress: {progress}");
			ProgressChanged(progress);
		}

		// IDisposable

		public void Dispose()
		{
			foreach (var fd in downloads)
			{
				fd.Dispose();
			}

			if (downloads != null)
				downloads.Clear();
			downloads = null;
		}
	}
}

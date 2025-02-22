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

        public DownloaderStatus GetStatus()
        {
            DownloaderStatus _status = DownloaderStatus.Ready;
            if (downloads?.Count > 0)
            {
                foreach (var item in downloads)
                    if (item.Progress < 100)
                        _status = DownloaderStatus.Downloading;
                return _status;
            }
            return DownloaderStatus.Ready;
        }

        public Downloader()
        {

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
            foreach (var d in downloads)
            {
                d.CancelAsync();
            }
            Clear();
        }

        public void Clear()
        {
            count = completed = 0;
            foreach (var fd in downloads)
            {
                fd.ProgressChanged -= Download_ProgressChanged;
                fd.Completed -= Download_Completed;
                fd.Dispose();
            }
            downloads.Clear();
            GC.Collect();
        }

        public void StartAll()
        {
            foreach (var fd in downloads)
            {
                fd.DownloadAsync();
            }
        }

        private void CheckFinished()
        {
            if (count == completed)
            {
                Completed();
                downloads?.Clear();
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
            fd.ProgressChanged -= Download_ProgressChanged;
            fd.Completed -= Download_Completed;
            fd.Canceled -= Download_Canceled;
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
            ProgressChanged(progress);
        }

        // IDisposable

        public void Dispose()
        {
            if (downloads != null)
            {
                foreach (var fd in downloads)
                {
                    fd.ProgressChanged -= Download_ProgressChanged;
                    fd.Completed -= Download_Completed;
                    fd.Dispose();
                }
                downloads.Clear();
            }
            downloads = null;
            GC.Collect();

        }
    }
}

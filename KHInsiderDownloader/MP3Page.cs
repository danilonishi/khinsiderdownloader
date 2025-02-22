using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace KHInsiderDownloader
{
    class MP3Page : IDisposable
    {
        private HtmlWeb web;
        private HtmlDocument doc;
        private HtmlNode audioNode;
        public Action<FileDownload> OnFileDownloadAvailable = delegate { };

        private string myUrl;
        private string myDownloadFolderPath;
        private string myRemoteFilePath;
        private string myLocalFilePath;
        private string myAlbumPath;

        BackgroundWorker worker;

        public void GetFileDownload(string url, string downloadFolderPath)
        {
            myUrl = url;
            myDownloadFolderPath = downloadFolderPath;

            // Album Directory
            string albumPath = myDownloadFolderPath;

            foreach (var c in Path.GetInvalidPathChars())
                albumPath = albumPath.Replace(c, '-');

            myAlbumPath = albumPath;

            if (!Directory.Exists(albumPath))
                Directory.CreateDirectory(albumPath);

            if (worker != null)
            {
                if (worker.IsBusy)
                {
                    worker.CancelAsync();
                }
                worker.Dispose();
            }

            worker = new BackgroundWorker();
            worker.DoWork += HandleWork;
            worker.RunWorkerCompleted += HandleWorkCompleted;
            worker.RunWorkerAsync();

        }

        private void HandleWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FileDownload fd = new FileDownload(myRemoteFilePath, myLocalFilePath);
            OnFileDownloadAvailable(fd);
            fd.DownloadAsync();
            Destroy();
        }

        private void HandleWork(object sender, DoWorkEventArgs e)
        {
            // Startup
            web = new HtmlWeb();
            doc = web.Load(myUrl);
            audioNode = doc.DocumentNode.SelectSingleNode("//audio");

            // Remote Paths
            var remoteFilePath = audioNode.GetAttributeValue("src", "");
            var remoteFileName = WebUtility.UrlDecode(remoteFilePath.Split('/').Last());

            string localFilePath = myAlbumPath + "\\" + remoteFileName;

            myRemoteFilePath = remoteFilePath;
            myLocalFilePath = localFilePath;
        }

        public void Destroy()
        {
            web = null;
            doc = null;
            audioNode = null;
            myUrl = null;
            myDownloadFolderPath = null;
            myRemoteFilePath = null;
            myLocalFilePath = null;
            myAlbumPath = null;

            if (worker != null)
            {
                worker.DoWork -= HandleWork;
                worker.RunWorkerCompleted -= HandleWorkCompleted;
                if (worker.IsBusy)
                {
                    worker.CancelAsync();
                }
                worker.Dispose();
            }
            worker = null;

            GC.Collect();
        }

        public void Dispose()
        {

        }
    }
}

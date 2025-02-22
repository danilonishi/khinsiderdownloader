using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace KHInsiderDownloader
{
    class MainPage : IDisposable
    {
        private HtmlWeb web;
        private HtmlDocument doc;
        private HtmlNodeCollection linkNodes;
        private string pageUrl;
        BackgroundWorker worker;

        public Action OnPageLoaded = delegate { };

        public bool fixNames = true;
        public string possibleAlbumName { get; private set; }

        public HashSet<string> validHashset { get; private set; }
        public string[] validLinks { get; private set; }

        //
        public void Dispose()
        {
            web = null;
            doc = null;
            if (linkNodes != null)
                linkNodes.Clear();
            linkNodes = null;
            pageUrl = null;
            if (validHashset != null)
                validHashset.Clear();
            validHashset = null;
            validLinks = null;
            possibleAlbumName = null;
            OnPageLoaded = null;
            if (worker != null)
            {
                if (worker.IsBusy)
                    worker.CancelAsync();
                worker.Dispose();
            }
            worker = null;
            GC.Collect();
        }

        public void LoadURL(string url)
        {
            if (worker != null)
                worker.Dispose();

            worker = new BackgroundWorker();

            pageUrl = url;
            worker.DoWork += HandleWork;
            worker.RunWorkerCompleted += HandleWorkCompleted;

            worker.RunWorkerAsync();

        }

        private void HandleWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnPageLoaded();
        }

        private void HandleWork(object sender, DoWorkEventArgs e)
        {
            // Startup
            web = new HtmlWeb();
            doc = web.Load(pageUrl);
            linkNodes = doc.DocumentNode.SelectNodes("//a");
            validHashset = new HashSet<string>();

            // Get Links
            foreach (var linkNode in linkNodes)
            {
                string linkURL = linkNode.GetAttributeValue("href", "");
                if (linkURL.EndsWith(".mp3"))
                {
                    validHashset.Add(pageUrl + "/" + linkURL.Split('/').Last());
                }
            }
            validLinks = validHashset.ToArray();

            // Album Name
            var h2 = doc.DocumentNode.SelectNodes("//h2").First();
            possibleAlbumName = h2.InnerText;
        }

        public string[] GetFileNames()
        {
            int idx = 0;
            string[] values = new string[validHashset.Count];
            foreach (var item in validHashset)
            {
                string itemName = WebUtility.UrlDecode(item.Split('/').Last());
                itemName = WebUtility.UrlDecode(itemName);
                values[idx++] = itemName;
            }
            return values;
        }

        // IDisposable


    }
}

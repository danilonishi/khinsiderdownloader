using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KHInsiderDownloader
{
	class MP3Page : IDisposable
	{
		private HtmlWeb web;
		private HtmlDocument doc;
		private HtmlNode audioNode;

		public FileDownload GetFileDownload(string url, string downloadFolderPath)
		{
			// Startup
			web = new HtmlWeb();
			doc = web.Load(url);
			audioNode = doc.DocumentNode.SelectSingleNode("//audio");

			// Album Directory
			string albumPath = downloadFolderPath;
			if (!Directory.Exists(albumPath))
			{
				Directory.CreateDirectory(albumPath);
			}

			// Remote Paths
			var remoteFilePath = audioNode.GetAttributeValue("src", "");
			var remoteFileName = WebUtility.UrlDecode(remoteFilePath.Split('/').Last());

			string localFilePath = albumPath + "\\" + remoteFileName;

			return new FileDownload(remoteFilePath, localFilePath);
		}

		public void Dispose()
		{
			web = null;
			doc = null;
			audioNode = null;
		}
	}
}

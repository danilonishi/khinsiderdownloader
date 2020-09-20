﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KHInsiderDownloader
{
	class MainPage : IDisposable
	{
		private HtmlWeb web;
		private HtmlDocument doc;
		private HtmlNodeCollection linkNodes;

		public bool fixNames = true;
		public string possibleAlbumName { get; private set; }

		public HashSet<string> validHashset { get; private set; }
		public string[] validLinks { get; private set; }

		//

		public void LoadURL(string url)
		{
			// Startup
			web = new HtmlWeb();
			doc = web.Load(url);
			linkNodes = doc.DocumentNode.SelectNodes("//a");
			validHashset = new HashSet<string>();

			// Get Links
			foreach (var linkNode in linkNodes)
			{
				string linkURL = linkNode.GetAttributeValue("href", "");
				if (linkURL.EndsWith(".mp3"))
				{
					validHashset.Add(url + "/" + linkURL.Split('/').Last());
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

		public void Dispose()
		{
			web = null;
			doc = null;
			if (linkNodes != null)
				linkNodes.Clear();
			linkNodes = null;
			if (validHashset != null)
				validHashset.Clear();
			validHashset = null;
			validLinks = null;
		}
	}
}

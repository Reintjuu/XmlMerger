using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace XmlMerger
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				Console.WriteLine("Please provide a folder and an output filename as command line arguments.");
				Console.WriteLine("Like: XmlMerger . output.xml");
				return;
			}

			string path = GetAbsolutePath(null, args[0]);
			string[] files;
			Console.WriteLine($"Trying path: {path}.");
			if (Directory.Exists(path))
			{
				files = Directory.GetFiles(path, "*.xml");
				if (files.Length == 0)
				{
					Console.WriteLine("No XML files have been found.");
					return;
				}
			}
			else
			{
				Console.WriteLine("Path not found.");
				return;
			}


			var xmlDocuments = new List<XmlDocument>();
			foreach (string file in files)
			{
				var doc = new XmlDocument();
				doc.Load(file);
				xmlDocuments.Add(doc);
			}

			for (int i = xmlDocuments.Count - 1; i >= 1; i--)
			{
				XmlDocument docA = xmlDocuments[i - 1];
				XmlDocument docB = xmlDocuments[i];

				foreach (XmlNode childEl in docB.DocumentElement.ChildNodes)
				{
					XmlNode newNode = docA.ImportNode(childEl, true);
					docA.DocumentElement.AppendChild(newNode);
				}
			}

			Console.WriteLine($"Writing to {args[1]} in current directory.");
			xmlDocuments[0].Save(args[1]);
		}

		private static string GetAbsolutePath(string basePath, string path)
		{
			if (path == null)
				return null;
			if (basePath == null)
				basePath = Path.GetFullPath("."); // quick way of getting current working directory
			else
				basePath = GetAbsolutePath(null, basePath); // to be REALLY sure ;)
			string finalPath;
			// specific for windows paths starting on \ - they need the drive added to them.
			// I constructed this piece like this for possible Mono support.
			if (!Path.IsPathRooted(path) || "\\".Equals(Path.GetPathRoot(path)))
			{
				finalPath = path.StartsWith(Path.DirectorySeparatorChar.ToString())
					? Path.Combine(Path.GetPathRoot(basePath), path.TrimStart(Path.DirectorySeparatorChar))
					: Path.Combine(basePath, path);
			}
			else
			{
				finalPath = path;
			}

			// resolves any internal "..\" to get the true full path.
			return Path.GetFullPath(finalPath);
		}
	}
}
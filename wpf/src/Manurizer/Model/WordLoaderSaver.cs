using Manurizer.Core;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Manurizer.Model
{
	public static class WordLoaderSaver
	{
		public static readonly string LibraryFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Manurizer");
		public static readonly string FileName = Path.Combine(LibraryFolder, "save.txt");

		public static Word[] Words { get; set; }

		static WordLoaderSaver()
		{
			if (File.Exists(FileName))
			{
				Words = JsonConvert.DeserializeObject<Word[]>(File.ReadAllText(FileName)).OrderBy(t => t.Category).OrderBy(t => t.Meaning).OrderBy(t => t.Name).ToArray();
			}
		}

		public static void Save(Word[] words)
		{
			if (!Directory.Exists(LibraryFolder))
			{
				Directory.CreateDirectory(LibraryFolder);
			}
			File.WriteAllText(FileName, JsonConvert.SerializeObject(words));
		}
	}
}

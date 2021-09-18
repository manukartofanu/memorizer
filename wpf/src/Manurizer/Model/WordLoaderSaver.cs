using Manurizer.Entity;
using Manurizer.Entity.Database;
using System;
using System.IO;

namespace Manurizer.Model
{
	public static class WordLoaderSaver
	{
		public static readonly string LibraryFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Manurizer");
		public static readonly string FileName = Path.Combine(LibraryFolder, "schema.db");

		public static Word[] Words { get; set; }

		static WordLoaderSaver()
		{
			if (!File.Exists(FileName))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(FileName));
				File.Copy(@".\schema.db", FileName);
			}
			using (var repository = new WordRepository(DatabaseSourceDefinitor.ConnectionString))
			{
				Words = repository.GetAllItemsEx();
			}
		}

		public static void SaveWord(Word word)
		{
			using (var repository = new WordRepository(DatabaseSourceDefinitor.ConnectionString))
			{
				repository.CreateItem(word);
			}
		}
	}
}

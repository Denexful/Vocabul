using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;

using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Vocabul.Droid
{
	public class FileAccessHelper
	{
		public static string GetLocalFilePath(string filename)
		{
			string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string dbPath = Path.Combine(path, filename);

			CopyDatabaseIfNotExists(dbPath);

			return dbPath;
		}

		private static void CopyDatabaseIfNotExists(string dbPath)
		{
			if (!File.Exists(dbPath))
			{
				using (var br = new BinaryReader(Application.Context.Assets.Open("VocalDB.db")))
				{
					using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int length = 0;
						while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write(buffer, 0, length);
						}
					}
				}
			}
		}
	}
}
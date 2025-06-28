using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Windows.Forms;
using System.IO;

namespace MAMECompiler64
{
    public class Globals
    {
		public static int MaxDownloads = 10;

		public static string GetTempFolder()
		{
			string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

			return Path.Combine(System.IO.Path.GetTempPath(), assemblyName);
		}

		public static string GetTempFileName(string fileName)
		{
			return Path.Combine(GetTempFolder(), fileName);
		}
    }

	public class Settings
	{
		public class Files
		{
			public static string ZipExe = String.Empty;
		}

		public class Folders
		{
			public static string MAMECompiler64 = String.Empty;
		}

		public class Urls
		{
			public static string MC64SetupExe = "https://baker76.com/download/mame/MC64Setup.exe";
			public static string MAMEFileListTxt = "https://baker76.com/download/mame/MameFileList.txt";
		}
	}
}

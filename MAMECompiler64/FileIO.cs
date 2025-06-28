using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace MAMECompiler64
{
	public class FileIO
	{
		public static bool TryGetDirectoryName(string path, out string directory)
		{
			directory = null;

			try
			{
				directory = Path.GetDirectoryName(path);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public static string AddBackslash(string path)
		{
			if (String.IsNullOrEmpty(path))
				return path;

			if (path.EndsWith(@"\"))
				return path;

			return path + @"\";
		}

		public static bool TryFindFile(string path, string name, out string fileName, bool recursive)
		{
			fileName = null;
			string extension = Path.GetExtension(name);
			string searchPattern = String.Format("*{0}", String.IsNullOrEmpty(extension) ? ".*" : extension);
			List<FileInfo> fileList = FileIO.GetFileList(path, searchPattern, recursive);

			foreach (FileInfo fileInfo in fileList)
			{
				if (fileInfo.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
				{
					fileName = fileInfo.FullName;

					return true;
				}
			}

			return false;
		}

		public static string GetTempFolder()
		{
			string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			string tempPath = Path.GetTempPath();
			return Path.Combine(tempPath, assemblyName);
		}

		public static string GetTempFileName(string fileName)
		{
			return Path.Combine(GetTempFolder(), fileName);
		}

		public static string GetRandomTempFolder()
		{
			string tempPath = Path.GetTempPath();
			string randomFolderName = GetRandomFolderName();
			return Path.Combine(tempPath, randomFolderName);
		}

		public static string GetRandomFolderName()
		{
			byte[] data = new byte[10];
			new RNGCryptoServiceProvider().GetBytes(data);
			char[] chArray = ToBase32StringSuitableForDirName(data).ToCharArray();
			return new string(chArray, 0, 12);
		}

		public static long GetFileSize(string path)
		{
			return new System.IO.FileInfo(path).Length;
		}

		public static List<FileInfo> GetFileList(string path, string[] searchPattern, bool recursive)
		{
			List<FileInfo> fileList = new List<FileInfo>();

			foreach (string ext in searchPattern)
				fileList.AddRange(GetFileList(path, ext, recursive));

			return fileList;
		}

		public static List<FileInfo> GetFileList(string path, string searchPattern, bool recursive)
		{
			List<FileInfo> fileList = new List<FileInfo>();
			DirectoryInfo di = new DirectoryInfo(path);

			if (!Directory.Exists(path))
				return fileList;

			FileInfo[] fileInfo = di.GetFiles(searchPattern);

			foreach (FileInfo fi in fileInfo)
				fileList.Add(fi);

			if (recursive)
			{
				DirectoryInfo[] directoryInfo = di.GetDirectories();

				foreach (DirectoryInfo diSub in directoryInfo)
					fileList.AddRange(GetFileList(diSub.FullName, searchPattern, recursive));
			}

			return fileList;
		}

		public static void DeleteAll(string targetFolder)
		{
			string[] fileArray = Directory.GetFiles(targetFolder);
			string[] directoryArray = Directory.GetDirectories(targetFolder);

			foreach (string file in fileArray)
			{
				File.SetAttributes(file, FileAttributes.Normal);
				File.Delete(file);
			}

			foreach (string directory in directoryArray)
				DeleteAll(directory);

			Directory.Delete(targetFolder, false);
		}

		public static void MoveDirectory(string sourceFolder, string targetFolder)
		{
			CopyDirectory(sourceFolder, targetFolder, "*.*", true, new string[] { });
		}

		public static void CopyDirectory(string sourceFolder, string targetFolder)
		{
			CopyDirectory(sourceFolder, targetFolder, "*.*", false, new string[] { });
		}

		public static void CopyDirectory(string sourceFolder, string targetFolder, string searchPattern, bool deleteSource, string[] noOverwriteExtensionArray)
		{
			Stack<FolderNode> folderStack = new Stack<FolderNode>();

			folderStack.Push(new FolderNode(sourceFolder, targetFolder));

			while (folderStack.Count > 0)
			{
				FolderNode folderNode = folderStack.Pop();

				Directory.CreateDirectory(folderNode.Target);

				foreach (string sourceFile in Directory.GetFiles(folderNode.Source, searchPattern))
				{
					string targetFile = Path.Combine(folderNode.Target, Path.GetFileName(sourceFile));
					string targetExtension = Path.GetExtension(sourceFile);

					if (File.Exists(targetFile))
					{
						if (Array.Exists<string>(noOverwriteExtensionArray, element => element == targetExtension))
							continue;

						File.SetAttributes(targetFile, FileAttributes.Normal);
						File.Delete(targetFile);
					}

					FileInfo fileInfo = new FileInfo(sourceFile);

					if (deleteSource)
						fileInfo.MoveTo(targetFile);
					else
						fileInfo.CopyTo(targetFile);
				}

				foreach (string folder in Directory.GetDirectories(folderNode.Source))
					folderStack.Push(new FolderNode(folder, Path.Combine(folderNode.Target, Path.GetFileName(folder))));
			}

			if (deleteSource)
				Directory.Delete(sourceFolder, true);
		}

		public class FolderNode
		{
			public string Source { get; private set; }
			public string Target { get; private set; }

			public FolderNode(string source, string target)
			{
				Source = source;
				Target = target;
			}
		}

		public static string GetRelativePath(string fromDirectory, string toPath, bool addRelativeFolders)
		{
			if (String.IsNullOrEmpty(fromDirectory))
				return toPath;

			if (String.IsNullOrEmpty(toPath))
				return toPath;

			bool isRooted = (Path.IsPathRooted(fromDirectory) && Path.IsPathRooted(toPath));

			if (isRooted)
			{
				bool isDifferentRoot = (string.Compare(Path.GetPathRoot(fromDirectory), Path.GetPathRoot(toPath), true) != 0);

				if (isDifferentRoot)
					return toPath;
			}

			List<string> relativePath = new List<string>();
			string[] fromDirectories = fromDirectory.Split(Path.DirectorySeparatorChar);

			string[] toDirectories = toPath.Split(Path.DirectorySeparatorChar);

			int length = Math.Min(fromDirectories.Length, toDirectories.Length);

			int lastCommonRoot = -1;

			// find common root
			for (int x = 0; x < length; x++)
			{
				if (string.Compare(fromDirectories[x], toDirectories[x], true) != 0)
					break;

				lastCommonRoot = x;
			}

			if (lastCommonRoot == -1)
				return toPath;

			// add relative folders in from path
			if (addRelativeFolders)
			{
				for (int x = lastCommonRoot + 1; x < fromDirectories.Length; x++)
				{
					if (fromDirectories[x].Length > 0)
						relativePath.Add("..");
				}
			}

			// add to folders to path
			for (int x = lastCommonRoot + 1; x < toDirectories.Length; x++)
			{
				relativePath.Add(toDirectories[x]);
			}

			// create relative path
			string[] relativeParts = new string[relativePath.Count];
			relativePath.CopyTo(relativeParts, 0);

			string newPath = string.Join(Path.DirectorySeparatorChar.ToString(), relativeParts);

			return newPath;
		}

		private static readonly Char[] s_Base32Char = {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 
                'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
                'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 
                'y', 'z', '0', '1', '2', '3', '4', '5'};

		public static String ToBase32StringSuitableForDirName(byte[] buff)
		{
			// This routine is optimised to be used with buffs of length 20
			Debug.Assert(((buff.Length % 5) == 0), "Unexpected hash length");

			StringBuilder sb = new StringBuilder();
			byte b0, b1, b2, b3, b4;
			int l, i;

			l = buff.Length;
			i = 0;

			// Create l chars using the last 5 bits of each byte.  
			// Consume 3 MSB bits 5 bytes at a time.

			do
			{
				b0 = (i < l) ? buff[i++] : (byte)0;
				b1 = (i < l) ? buff[i++] : (byte)0;
				b2 = (i < l) ? buff[i++] : (byte)0;
				b3 = (i < l) ? buff[i++] : (byte)0;
				b4 = (i < l) ? buff[i++] : (byte)0;

				// Consume the 5 Least significant bits of each byte
				sb.Append(s_Base32Char[b0 & 0x1F]);
				sb.Append(s_Base32Char[b1 & 0x1F]);
				sb.Append(s_Base32Char[b2 & 0x1F]);
				sb.Append(s_Base32Char[b3 & 0x1F]);
				sb.Append(s_Base32Char[b4 & 0x1F]);

				// Consume 3 MSB of b0, b1, MSB bits 6, 7 of b3, b4
				sb.Append(s_Base32Char[(
						((b0 & 0xE0) >> 5) |
						((b3 & 0x60) >> 2))]);

				sb.Append(s_Base32Char[(
						((b1 & 0xE0) >> 5) |
						((b4 & 0x60) >> 2))]);

				// Consume 3 MSB bits of b2, 1 MSB bit of b3, b4

				b2 >>= 5;

				Debug.Assert(((b2 & 0xF8) == 0), "Unexpected set bits");

				if ((b3 & 0x80) != 0)
					b2 |= 0x08;
				if ((b4 & 0x80) != 0)
					b2 |= 0x10;

				sb.Append(s_Base32Char[b2]);

			} while (i < l);

			return sb.ToString();
		}
	}
}

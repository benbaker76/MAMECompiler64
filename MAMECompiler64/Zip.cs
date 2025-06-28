
#define USE_PROGRESS_BAR

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;

namespace MAMECompiler64
{
	// 7-Zip 4.53 beta  Copyright (c) 1999-2007 Igor Pavlov  2007-08-27
	// 
	// Usage: 7z <command> [<switches>...] <archive_name> [<file_names>...]
	//        [<@listfiles...>]
	// 
	// <Commands>
	//   a: Add files to archive
	//   b: Benchmark
	//   d: Delete files from archive
	//   e: Extract files from archive (without using directory names)
	//   l: List contents of archive
	//   t: Test integrity of archive
	//   u: Update files to archive
	//   x: eXtract files with full paths
	// <Switches>
	//   -ai[r[-|0]]{@listfile|!wildcard}: Include archives
	//   -ax[r[-|0]]{@listfile|!wildcard}: eXclude archives
	//   -bd: Disable percentage indicator
	//   -i[r[-|0]]{@listfile|!wildcard}: Include filenames
	//   -m{Parameters}: set compression Method
	//   -o{Directory}: set Output directory
	//   -p{Password}: set Password
	//   -r[-|0]: Recurse subdirectories
	//   -scs{UTF-8 | WIN | DOS}: set charset for list files
	//   -sfx[{name}]: Create SFX archive
	//   -si[{name}]: read data from stdin
	//   -slt: show technical information for l (List) command
	//   -so: write data to stdout
	//   -ssc[-]: set sensitive case mode
	//   -ssw: compress shared files
	//   -t{Type}: Set type of archive
	//   -v{Size}[b|k|m|g]: Create volumes
	//   -u[-][p#][q#][r#][x#][y#][z#][!newArchiveName]: Update options
	//   -w[{path}]: assign Work directory. Empty path means a temporary directory
	//   -x[r[-|0]]]{@listfile|!wildcard}: eXclude filenames
	//   -y: assume Yes on all queries

	public class Zip
	{
		public static event EventHandler ZipExited = null;
		public static event DataReceivedEventHandler OutputDataReceived = null;
		public static event DataReceivedEventHandler ErrorDataReceived = null;

		public static bool Extract(Control control, string archiveName, string outputDirectory)
		{
			string errorMessage = null;

			return Extract(control, archiveName, outputDirectory, true, true, false, null, out errorMessage);
		}

		public static bool Extract(Control control, string archiveName, string outputDirectory, bool waitForExit)
		{
			string errorMessage = null;

			return Extract(control, archiveName, outputDirectory, true, true, waitForExit, null, out errorMessage);
		}

		public static bool Extract(Control control, string archiveName, string outputDirectory, bool extractFullPath, bool processOutput, bool waitForExit, object tag, out string errorMessage)
		{
			string arguments = String.Format("{0} -y -o\"{1}\" \"{2}\"", extractFullPath ? "x" : "e", outputDirectory, archiveName);

#if USE_PROGRESS_BAR
			return TryLaunchProcessWithProgressBar(control, "Extracting...", Settings.Files.ZipExe, arguments, processOutput, false, waitForExit, tag, OutputDataReceived, ErrorDataReceived, ZipExited, out errorMessage);
#else
			return TryLaunchProcess(control, Settings.Files.ZipExe, arguments, processOutput, false, waitForExit, tag, OutputDataReceived, ErrorDataReceived, ZipExited, out errorMessage);
#endif
		}

		public static bool Add(Control control, string inputDirectory, string archiveName)
		{
			string errorMessage = null;

			return Add(control, inputDirectory, archiveName, true, false, null, out errorMessage);
		}

		public static bool Add(Control control, string inputDirectory, string archiveName, bool waitForExit)
		{
			string errorMessage = null;

			return Add(control, inputDirectory, archiveName, true, waitForExit, null, out errorMessage);
		}

		public static bool Add(Control control, string inputDirectory, string archiveName, bool processOutput, bool waitForExit, object tag, out string errorMessage)
		{
			string arguments = String.Format("a \"{0}\" \"{1}\"", archiveName, inputDirectory);

#if USE_PROGRESS_BAR
			return TryLaunchProcessWithProgressBar(control, "Zipping...", Settings.Files.ZipExe, arguments, processOutput, false, waitForExit, null, OutputDataReceived, ErrorDataReceived, ZipExited, out errorMessage);
#else
			return TryLaunchProcess(control, Settings.Files.ZipExe, arguments, processOutput, false, waitForExit, null, OutputDataReceived, ErrorDataReceived, ZipExited, out errorMessage);
#endif
		}

		public static bool List(Control control, string archiveName, out List<ZipFileNode> zipFileList, out string comment)
		{
			zipFileList = null;
			comment = null;
			string errorMessage = String.Empty;
			List<string> outputList = new List<string>();
			List<string> errorList = new List<string>();
			bool isData = false;

			string arguments = String.Format("l \"{0}\"", archiveName);
			DataReceivedEventHandler outputDataReceived = new DataReceivedEventHandler
			(
				delegate(object sender, DataReceivedEventArgs e)
				{
					outputList.Add(e.Data);
				}
			);

			DataReceivedEventHandler errorDataReceived = new DataReceivedEventHandler
			(
				delegate(object sender, DataReceivedEventArgs e)
				{
					errorList.Add(e.Data);
				}
			);

#if USE_PROGRESS_BAR
			if (!TryLaunchProcessWithProgressBar(control, "Creating List...", Settings.Files.ZipExe, arguments, true, false, true, null, outputDataReceived, errorDataReceived, ZipExited, out errorMessage))
				return false;
#else
			if (!TryLaunchProcess(control, Settings.Files.ZipExe, arguments, true, false, true, null, outputDataReceived, errorDataReceived, ZipExited, out errorMessage))
				return false;
#endif

			zipFileList = new List<ZipFileNode>();

			foreach (string line in outputList)
			{
				if (String.IsNullOrEmpty(line))
					continue;

				if (line.StartsWith("Comment = "))
					comment = line.Substring(10);

				if (line.StartsWith("-------------------"))
				{
					isData = !isData;

					continue;
				}

				if (isData)
				{
					string date = line.Substring(0, 10);
					string time = line.Substring(11, 8);
					string attr = line.Substring(20, 5);
					long size = long.Parse(line.Substring(26, 12).Trim());
					long compressed = long.Parse(line.Substring(39, 12).Trim());
					string name = line.Substring(53, line.Length - 53);

					DateTime dateTime = DateTime.ParseExact(String.Format("{0} {1}", date, time), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
					FileAttributes fileAttributes = (attr[0] == 'D' ? FileAttributes.Directory : 0);
					fileAttributes |= (attr[1] == 'R' ? FileAttributes.ReadOnly : 0);
					fileAttributes |= (attr[2] == 'H' ? FileAttributes.Hidden : 0);
					fileAttributes |= (attr[3] == 'C' ? FileAttributes.Compressed : 0);
					fileAttributes |= (attr[4] == 'A' ? FileAttributes.Archive : 0);

					ZipFileNode zipFileNode = new ZipFileNode(dateTime, fileAttributes, size, compressed, name);

					zipFileList.Add(zipFileNode);
				}
			}

			//Console.WriteLine(String.Join(Environment.NewLine, outputList.ToArray()));

			return true;
		}

		private static bool TryLaunchProcessWithProgressBar(Control control, string message, string fileName, string arguments, bool processOutput, bool waitForInputIdle, bool waitForExit, object tag, DataReceivedEventHandler outputDataReceived, DataReceivedEventHandler errorDataReceived, EventHandler zipExited, out string errorMessage)
		{
			bool retVal = false;
			string tempErrorMessage = String.Empty;
			frmProcessing frmProcessing = new frmProcessing();

			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.WorkerReportsProgress = true;
			backgroundWorker.ProgressChanged += new ProgressChangedEventHandler((sender, e) =>
			{
				frmProcessing.UpdateProgressBar(e.ProgressPercentage, (string)e.UserState);
			});

			backgroundWorker.DoWork += new DoWorkEventHandler((sender, e) =>
			{
				backgroundWorker.ReportProgress(50, message);

				retVal = TryLaunchProcess(control, fileName, arguments, processOutput, waitForInputIdle, true, tag, outputDataReceived, errorDataReceived, zipExited, out tempErrorMessage);

				backgroundWorker.ReportProgress(100, "Done.");
			});

			backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender, e) =>
			{
				backgroundWorker.Dispose();

				frmProcessing.Close();
			});

			backgroundWorker.RunWorkerAsync();

			frmProcessing.ShowDialog(control);

			errorMessage = tempErrorMessage;

			return retVal;
		}

		private static bool TryLaunchProcess(Control control, string fileName, string arguments, bool processOutput, bool waitForInputIdle, bool waitForExit, object tag, DataReceivedEventHandler outputDataReceived, DataReceivedEventHandler errorDataReceived, EventHandler zipExited, out string errorMessage)
		{
			errorMessage = String.Empty;

			try
			{
				ProcessEx processEx = new ProcessEx();

				processEx.Tag = tag;
				processEx.ProcessOutput = processOutput;

				processEx.StartInfo.FileName = fileName;
				processEx.StartInfo.WorkingDirectory = Path.GetDirectoryName(fileName);
				processEx.StartInfo.Arguments = arguments;
				processEx.StartInfo.ErrorDialog = false;
				processEx.StartInfo.CreateNoWindow = true;
				processEx.StartInfo.UseShellExecute = false;

				processEx.StartInfo.RedirectStandardOutput = processOutput;
				processEx.StartInfo.RedirectStandardError = processOutput;

				processEx.OutputDataReceived += outputDataReceived;
				processEx.ErrorDataReceived += errorDataReceived;
				processEx.Exited += zipExited;

				processEx.SynchronizingObject = control;
				processEx.EnableRaisingEvents = true;

				bool retVal = processEx.Start();

				if (processOutput)
				{
					processEx.BeginOutputReadLine();
					processEx.BeginErrorReadLine();
				}

				if (waitForInputIdle)
					processEx.WaitForInputIdle();

				if (waitForExit)
					processEx.WaitForExit();

				return retVal;
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			return false;
		}
	}

	public class ZipFileNode
	{
		public DateTime DateTime;
		public FileAttributes FileAttributes;
		public long Size = 0;
		public long Compressed = 0;
		public string Name = null;

		public ZipFileNode(DateTime dateTime, FileAttributes fileAttributes, long size, long compressed, string name)
		{
			DateTime = dateTime;
			FileAttributes = fileAttributes;
			Size = size;
			Compressed = compressed;
			Name = name;
		}
	}

	public class ProcessEx : Process
	{
		public object Tag = null;
		public bool ProcessOutput = false;

		public ProcessEx()
			: base()
		{
		}
	}
}

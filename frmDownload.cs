using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Resources;
using System.Reflection;
using System.Security.Cryptography;

namespace MAMECompiler64
{
	public partial class frmDownload : Form
	{
		private enum DownloadStatus
		{
			Idle,
			Downloading,
			Extracting,
			Launching,
			DownloadError,
			LaunchError,
			ExtractError,
			DownloadSucceeded,
			DownloadStopped,
			ExtractFinished,
			LaunchFinished,
			DownloadCancelled
		}

		private FileDownloader m_fileDownloader = null;
		private List<DownloadFileNode> m_downloadFileList = null;

		private bool m_autoStart = false;

		private string[] m_noOutputCategoryArray = { "Tools", "Source" };

		private DownloadStatus m_downloadStatus = DownloadStatus.Idle;

		public event EventHandler<DownloadStatusChangedEventArgs> DownloadStatusChanged;
		public event EventHandler<FileDownloadEventArgs> FileDownloadSucceeded = null;
		public event EventHandler<FileDownloadEventArgs> FileDownloadFailed = null;
		public event EventHandler<FileDownloadEventArgs> FileDownloadStopped = null;
		public event EventHandler<FileDownloadEventArgs> PostProcessSucceeded = null;
		public event EventHandler<FileDownloadEventArgs> PostProcessFailed = null;
		public event EventHandler<DataReceivedEventArgs> OutputDataReceived = null;
		public event EventHandler<DataReceivedEventArgs> ErrorDataReceived = null;
		public event EventHandler<EventArgs> AllFilesDownloadCompleted = null;

		public frmDownload(List<DownloadFileNode> fileList, bool removeDuplicates, bool autoStart)
		{
			InitializeComponent();

			m_downloadFileList = (removeDuplicates ? RemoveDuplicates(fileList) : fileList);
			m_autoStart = autoStart;
		}

		private void butOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void butCancel_Click(object sender, EventArgs e)
		{
			m_fileDownloader.Cancel();

			DialogResult = DialogResult.Cancel;
		}

		private void OnFileDownloadProgressChanged(object sender, FileDownloadProgressChangedEventArgs e)
		{
			lblSize.Text = FileDownloader.FormatSizeBinary(e.CurrentTotalBytesToReceive);
			lblSpeed.Text = String.Format("{0}/s", FileDownloader.FormatSizeBinary((long)e.DownloadSpeed, 0));
			lblFileProgress.Text = String.Format("Downloaded {0} of {1} ({2:0}%)", FileDownloader.FormatSizeBinary(e.CurrentBytesReceived, 0), FileDownloader.FormatSizeBinary(e.CurrentTotalBytesToReceive, 0), e.CurrentProgressPercentage);
			pbFileProgress.Value = e.CurrentProgressPercentage;

			lblTotalProgress.Text = String.Format("Downloaded {0} of {1} ({2:0}%)", FileDownloader.FormatSizeBinary(e.TotalBytesReceived, 0), FileDownloader.FormatSizeBinary(e.TotalBytesToReceive, 0), e.TotalProgressPercentage);
			pbTotalProgress.Value = e.TotalProgressPercentage;
		}

		private void OnFileDownloadStarted(object sender, FileDownloadEventArgs e)
		{
			DownloadFileNode downloadFileNode = e.DownloadFile;

			SetDownloadStatus(downloadFileNode, DownloadStatus.Downloading);

			lblFileName.Text = downloadFileNode.FileName;
			lblSize.Text = FileDownloader.FormatSizeBinary(downloadFileNode.TotalBytes);
		}

		private void OnFileDownloadCompleted(object sender, FileDownloadEventArgs e)
		{
			DownloadFileNode downloadFileNode = e.DownloadFile;

			SetDownloadStatus(downloadFileNode, DownloadStatus.DownloadSucceeded);

			ActionDownloadStatus(downloadFileNode);

			if (FileDownloadSucceeded != null)
				FileDownloadSucceeded(sender, new FileDownloadEventArgs(downloadFileNode));
		}

		private void OnFileDownloadCancelled(object sender, FileDownloadEventArgs e)
		{
			DownloadFileNode downloadFileNode = e.DownloadFile;

			SetDownloadStatus(downloadFileNode, DownloadStatus.DownloadStopped);

			ActionDownloadStatus(downloadFileNode);

			if (FileDownloadStopped != null)
				FileDownloadStopped(sender, new FileDownloadEventArgs(downloadFileNode));
		}

		private void OnFileDownloadError(object sender, FileDownloadErrorEventArgs e)
		{
			DownloadFileNode downloadFileNode = e.DownloadFile;

			SetDownloadStatus(downloadFileNode, DownloadStatus.DownloadError);

			downloadLabel.Text = e.Error.Message;

			ActionDownloadStatus(downloadFileNode);

			if (FileDownloadFailed != null)
				FileDownloadFailed(sender, new FileDownloadEventArgs(downloadFileNode));
		}

		private void OnAllFilesDownloadCompleted(object sender, EventArgs e)
		{
			if (AllFilesDownloadCompleted != null)
				AllFilesDownloadCompleted(sender, e);
		}

		private void OnCancelled(object sender, EventArgs e)
		{
			SetDownloadStatus(null, DownloadStatus.DownloadCancelled);

			m_fileDownloader.Dispose();
			m_fileDownloader = null;

			DialogResult = DialogResult.Cancel;
		}

		private void ActionDownloadStatus(DownloadFileNode downloadFileNode)
		{
			switch (m_downloadStatus)
			{
				case DownloadStatus.Idle:
				case DownloadStatus.Downloading:
				case DownloadStatus.Extracting:
				case DownloadStatus.Launching:
					break;
				case DownloadStatus.DownloadError:
				case DownloadStatus.LaunchError:
				case DownloadStatus.ExtractError:
				case DownloadStatus.DownloadStopped:
					SetDownloadStatus(downloadFileNode, DownloadStatus.Idle);

					RemoveDownload(downloadFileNode);
					break;
				case DownloadStatus.DownloadSucceeded:
					SetDownloadStatus(downloadFileNode, DownloadStatus.Idle);

					if (downloadFileNode.NeedsPostProcessing)
					{
						string fileExtension = Path.GetExtension(downloadFileNode.FileName);
						string errorMessage = null;

						switch (fileExtension.ToLower())
						{
							case "":
							case ".zip":
							case ".7zip":
							case ".7z":
							case ".rar":
								if (!TryUnZipFile(downloadFileNode, out errorMessage))
								{
									if (PostProcessFailed != null)
										PostProcessFailed(this, new FileDownloadEventArgs(downloadFileNode));

									SetDownloadStatus(downloadFileNode, DownloadStatus.ExtractError);

									RemoveDownload(downloadFileNode);

									downloadLabel.Text = errorMessage;

									pbFileProgress.Value = 0;
								}
								break;
							case ".exe":
								if (!TryLaunchExe(downloadFileNode, out errorMessage))
								{
									if (PostProcessFailed != null)
										PostProcessFailed(this, new	FileDownloadEventArgs(downloadFileNode));

									SetDownloadStatus(downloadFileNode, DownloadStatus.LaunchError);

									RemoveDownload(downloadFileNode);

									downloadLabel.Text = errorMessage;

									pbFileProgress.Value = 0;
								}
								break;
							default:
								break;
						}
					}
					else
						RemoveDownload(downloadFileNode);
					break;
				case DownloadStatus.ExtractFinished:
				case DownloadStatus.LaunchFinished:
				case DownloadStatus.DownloadCancelled:
					DisplayStatus(downloadFileNode);

					RemoveDownload(downloadFileNode);
					break;
			}
		}

		private List<DownloadFileNode> RemoveDuplicates(List<DownloadFileNode> fileList)
		{
			List<DownloadFileNode> newFileList = new List<DownloadFileNode>();
			Dictionary<string, string> newFileListDictionary = new Dictionary<string, string>();

			foreach (DownloadFileNode downloadFileNode in fileList)
			{
				if (newFileListDictionary.ContainsKey(downloadFileNode.Id))
					continue;

				newFileList.Add(downloadFileNode);
				newFileListDictionary.Add(downloadFileNode.Id, null);
			}

			return newFileList;
		}

		private void RemoveDownload(DownloadFileNode downloadFileNode)
		{
			//Console.WriteLine("File '{0}' Index: {1} Count: {2}", downloadFileNode.FileName, m_downloadFileList.IndexOf(downloadFileNode), m_downloadFileList.Count);

			m_downloadFileList.Remove(downloadFileNode);

			if (m_downloadFileList.Count == 0)
			{
				butOK.Enabled = true;

				DialogResult = DialogResult.OK;
			}
		}

		private bool IsError()
		{
			switch (m_downloadStatus)
			{
				case DownloadStatus.DownloadError:
				case DownloadStatus.LaunchError:
				case DownloadStatus.ExtractError:
					return true;
			}

			return false;
		}

		private string GetStatusString(DownloadFileNode downloadFileNode)
		{
			switch (m_downloadStatus)
			{
				case DownloadStatus.Idle:
					return "Idle.";
				case DownloadStatus.Downloading:
					return String.Format("Downloading '{0}'...", downloadFileNode.FileName);
				case DownloadStatus.Extracting:
					return String.Format("Extracting '{0}'...", downloadFileNode.FileName);
				case DownloadStatus.Launching:
					return String.Format("Launching '{0}'...", downloadFileNode.FileName);
				case DownloadStatus.DownloadSucceeded:
					return String.Format("Downloading '{0}' Done.", downloadFileNode.FileName);
				case DownloadStatus.DownloadStopped:
					return String.Format("Downloading '{0}' Stopped.", downloadFileNode.FileName);
				case DownloadStatus.ExtractFinished:
					return String.Format("Extracting '{0}' Done.", downloadFileNode.FileName);
				case DownloadStatus.LaunchFinished:
					return String.Format("Launching '{0}' Done.", downloadFileNode.FileName);
				case DownloadStatus.DownloadError:
				case DownloadStatus.LaunchError:
				case DownloadStatus.ExtractError:
					return string.Empty;
				case DownloadStatus.DownloadCancelled:
					return "Download Cancelled.";
			}

			return string.Empty;
		}

		private void DisplayStatus(DownloadFileNode downloadFileNode)
		{
			string statusString = GetStatusString(downloadFileNode);

			if (!String.IsNullOrEmpty(statusString))
				downloadLabel.Text = statusString;
		}

		private bool TryUnZipFile(DownloadFileNode downloadFileNode, out string errorMessage)
		{
			errorMessage = String.Empty;

			SetDownloadStatus(downloadFileNode, DownloadStatus.Extracting);

			try
			{
				string sourceFileName = Path.Combine(downloadFileNode.DestinationFolder, downloadFileNode.FileName);
				bool processOutput = !(Array.IndexOf(m_noOutputCategoryArray, downloadFileNode.Category) > -1);

				if (!Directory.Exists(downloadFileNode.ExtractionFolder))
					Directory.CreateDirectory(downloadFileNode.ExtractionFolder);

				return Zip.Extract(this, sourceFileName, downloadFileNode.ExtractionFolder, true, processOutput, false, downloadFileNode, out errorMessage);
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;

				return false;
			}
		}

		private bool TryLaunchExe(DownloadFileNode downloadFileNode, out string errorMessage)
		{
			errorMessage = String.Empty;

			SetDownloadStatus(downloadFileNode, DownloadStatus.Launching);

			try
			{
				string sourceFileName = Path.Combine(downloadFileNode.DestinationFolder, downloadFileNode.FileName);
				bool isUpdate = downloadFileNode.Category.Equals("Update");
				bool processOutput = !(Array.IndexOf(m_noOutputCategoryArray, downloadFileNode.Category) > -1);

				if (!String.IsNullOrEmpty(downloadFileNode.ExtractionFolder))
				{
					if (!Directory.Exists(downloadFileNode.ExtractionFolder))
						Directory.CreateDirectory(downloadFileNode.ExtractionFolder);
				}

				string arguments = (isUpdate ? "/VERYSILENT" : String.Format("-o\"{0}\" -y", downloadFileNode.ExtractionFolder));

				bool retVal = TryLaunchProcessWithProgressBar(this, sourceFileName, arguments, downloadFileNode, isUpdate, processOutput, false, false, LaunchProcess_OutputDataReceived, LaunchProcess_ErrorDataReceived, LaunchProcess_Exited, out errorMessage);

				return retVal;
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;

				return false;
			}
		}

		private bool TryLaunchProcessWithProgressBar(Control control, string fileName, string arguments, object tag, bool isUpdate, bool processOutput, bool waitForInputIdle, bool waitForExit, DataReceivedEventHandler outputDataReceived, DataReceivedEventHandler errorDataReceived, EventHandler exited, out string errorMessage)
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
				backgroundWorker.ReportProgress(50, "Launching...");

				retVal = TryLaunchProcess(control, fileName, arguments, tag, isUpdate, processOutput, waitForInputIdle, true, outputDataReceived, errorDataReceived, exited, out tempErrorMessage);

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

		private bool TryLaunchProcess(Control control, string fileName, string arguments, object tag, bool isUpdate, bool processOutput, bool waitForInputIdle, bool waitForExit, DataReceivedEventHandler outputDataReceived, DataReceivedEventHandler errorDataReceived, EventHandler exited, out string errorMessage)
		{
			errorMessage = String.Empty;

			try
			{
				ProcessEx processEx = new ProcessEx();

				processEx.Tag = tag;
				processEx.ProcessOutput = (!isUpdate && processOutput);

				processEx.StartInfo.FileName = fileName;
				processEx.StartInfo.Arguments = arguments;
				processEx.StartInfo.ErrorDialog = false;
				processEx.StartInfo.CreateNoWindow = true;
				processEx.StartInfo.UseShellExecute = isUpdate; // false

				processEx.StartInfo.RedirectStandardOutput = (!isUpdate && processOutput); // true
				processEx.StartInfo.RedirectStandardError = (!isUpdate && processOutput); // true

				processEx.OutputDataReceived += outputDataReceived;
				processEx.ErrorDataReceived += errorDataReceived;
				processEx.Exited += exited;

				processEx.SynchronizingObject = control;
				processEx.EnableRaisingEvents = !isUpdate; // true

				bool retVal = processEx.Start();

				if (!isUpdate && processOutput)
				{
					processEx.BeginOutputReadLine();
					processEx.BeginErrorReadLine();
				}

				if (retVal && isUpdate)
					LaunchProcess_Exited(processEx, EventArgs.Empty);

				if (waitForInputIdle)
					processEx.WaitForInputIdle();

				if (waitForExit)
					processEx.WaitForExit();
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;

				return false;
			}

			return true;
		}

		private string GetFileNameFromURL(string url)
		{
			return url.Substring(url.LastIndexOf("/") + 1);
		}

		private void DownloadFile()
		{
			if (m_fileDownloader != null)
				m_fileDownloader.Dispose();

			m_fileDownloader = new FileDownloader(m_downloadFileList);

			m_fileDownloader.FileDownloadProgressChanged += OnFileDownloadProgressChanged;
			m_fileDownloader.FileDownloadError += OnFileDownloadError;
			m_fileDownloader.FileDownloadStarted += OnFileDownloadStarted;
			m_fileDownloader.FileDownloadCancelled += OnFileDownloadCancelled;
			m_fileDownloader.FileDownloadCompleted += OnFileDownloadCompleted;
			m_fileDownloader.AllFilesDownloadCompleted += OnAllFilesDownloadCompleted;

			//m_fileDownloader.FileDownloadStarted += OnFileDownloadStarted;
			//m_fileDownloader.ProgressChanged += OnProgressChanged;
			//m_fileDownloader.FileDownloadFailed += OnFileDownloadFailed;
			//m_fileDownloader.FileDownloadSucceeded += OnFileDownloadSucceeded;
			//m_fileDownloader.FileDownloadStopped += OnFileDownloadStopped;
			//m_fileDownloader.CalculationFileSizesStarted += OnCalculationFileSizesStarted;
			//m_fileDownloader.CalculatingFileSize += OnCalculatingFileSize;
			//m_fileDownloader.FileSizesCalculationComplete += OnFileSizesCalculationComplete;
			//m_fileDownloader.Completed += OnCompleted;
			//m_fileDownloader.Cancelled += OnCancelled;

			m_fileDownloader.Start();

			butOK.Enabled = false;
		}

		private void OnCalculationFileSizesStarted(object sender, EventArgs e)
		{
			downloadLabel.Text = "Calculating File Sizes...";
		}

		private void OnCalculatingFileSize(object sender, int fileIndex)
		{
			pbFileProgress.Value = (int)(((float)fileIndex / m_downloadFileList.Count) * 100);
		}

		private void OnFileSizesCalculationComplete(object sender, EventArgs e)
		{
			pbFileProgress.Value = 0;
		}

		private void StopDownload(DownloadFileNode downloadFileNode)
		{
			if (m_fileDownloader != null)
				m_fileDownloader.Dispose();

			SetDownloadStatus(downloadFileNode, DownloadStatus.Idle);

			pbFileProgress.Value = 0;
		}

		private void SetDownloadStatus(DownloadFileNode downloadFileNode, DownloadStatus downloadStatus)
		{
			m_downloadStatus = downloadStatus;

			DisplayStatus(downloadFileNode);

			if (DownloadStatusChanged != null)
				DownloadStatusChanged(this, new DownloadStatusChangedEventArgs(GetStatusString(downloadFileNode), IsError()));
		}

		private void LaunchProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (OutputDataReceived != null)
				OutputDataReceived(sender, e);
		}

		private void LaunchProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (ErrorDataReceived != null)
				ErrorDataReceived(sender, e);
		}

		private void Zip_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (OutputDataReceived != null)
				OutputDataReceived(sender, e);
		}

		private void Zip_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (ErrorDataReceived != null)
				ErrorDataReceived(sender, e);
		}

		private void Zip_ZipExited(object sender, EventArgs e)
		{
			if (this.IsDisposed)
				return;

			ProcessEx processEx = (ProcessEx)sender;
			DownloadFileNode downloadFileNode = (DownloadFileNode)processEx.Tag;

			if (processEx.ProcessOutput)
				processEx.CancelOutputRead();

			if (PostProcessSucceeded != null)
				PostProcessSucceeded(this, new FileDownloadEventArgs(downloadFileNode));

			SetDownloadStatus(downloadFileNode, DownloadStatus.ExtractFinished);
			ActionDownloadStatus(downloadFileNode);
		}

		private void LaunchProcess_Exited(object sender, EventArgs e)
		{
			if (this.IsDisposed)
				return;

			ProcessEx processEx = (ProcessEx)sender;
			DownloadFileNode downloadFileNode = (DownloadFileNode)processEx.Tag;

			if (processEx.ProcessOutput)
				processEx.CancelOutputRead();

			if (PostProcessSucceeded != null)
				PostProcessSucceeded(this, new FileDownloadEventArgs(downloadFileNode));

			SetDownloadStatus(downloadFileNode, DownloadStatus.LaunchFinished);
			ActionDownloadStatus(downloadFileNode);
		}

		private void frmDownload_Load(object sender, EventArgs e)
		{
			this.Show();
			this.Refresh();

			Zip.ZipExited += Zip_ZipExited;
			Zip.OutputDataReceived += Zip_OutputDataReceived;
			Zip.ErrorDataReceived += Zip_ErrorDataReceived;

			if (m_autoStart)
				DownloadFile();
		}

		private void frmDownload_FormClosing(object sender, FormClosingEventArgs e)
		{
			Zip.ZipExited -= Zip_ZipExited;
			Zip.OutputDataReceived -= Zip_OutputDataReceived;
			Zip.ErrorDataReceived -= Zip_ErrorDataReceived;
		}
	}

	public class DownloadStatusChangedEventArgs : EventArgs
	{
		public string Message = null;
		public bool IsError = false;

		public DownloadStatusChangedEventArgs(string message, bool isError)
		{
			Message = message;
			IsError = isError;
		}
	}
}
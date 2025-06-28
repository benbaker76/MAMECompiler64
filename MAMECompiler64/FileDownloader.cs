using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Diagnostics;

namespace MAMECompiler64
{
	public class FileDownloader : IDisposable
	{
		private const int DEFAULT_DECIMALS = 2;
		private const int DEFAULT_SPEED_SAMPLES = 10;
		private const string DEFAULT_USER_AGENT = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

		private WebClient m_webClient = null;

		private List<DownloadFileNode> m_downloadFileList = null;

		private int m_fileIndex = 0;

		private long m_totalBytesReceived = 0;
		private long m_totalBytesToReceive = 0;

		private long m_lastBytesReceived = 0;
		private Stopwatch m_speedStopwatch = null;
		private int m_speedSampleIndex = 0;
		private double[] m_speedSampleArray = null;
		private double m_downloadSpeed = 0.0;

		public event EventHandler<FileDownloadProgressChangedEventArgs> FileDownloadProgressChanged;
		public event EventHandler<FileDownloadErrorEventArgs> FileDownloadError;
		public event EventHandler<FileDownloadEventArgs> FileDownloadStarted;
		public event EventHandler<FileDownloadEventArgs> FileDownloadCancelled;
		public event EventHandler<FileDownloadEventArgs> FileDownloadCompleted;
		public event EventHandler<EventArgs> AllFilesDownloadCompleted;

		private bool m_disposed = false;

		public FileDownloader(List<DownloadFileNode> downloadFileList)
		{
			try
			{
				ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
			}
			catch { }

			m_speedStopwatch = new Stopwatch();
			m_speedSampleArray = new double[DEFAULT_SPEED_SAMPLES];

			m_downloadFileList = new List<DownloadFileNode>();
			m_downloadFileList.AddRange(downloadFileList);

			foreach (DownloadFileNode downloadFileNode in downloadFileList)
			{
				long contentLength = 0;

				if (TryGetUrlContentLength(downloadFileNode, out contentLength))
				{
					downloadFileNode.TotalBytes = contentLength;

					m_totalBytesToReceive += contentLength;
				}
			}
		}

		public bool Start()
		{
			return TryDownload();
		}

		public bool Cancel()
		{
			lock (m_downloadFileList)
			{
				if (m_webClient == null)
					return false;

				m_webClient.CancelAsync();

				m_downloadFileList.Clear();
			}

			return true;
		}

		private bool TryDownload()
		{
			if (AreAllFileDownloadCompleted())
				return false;

			lock (m_downloadFileList)
			{
				DownloadFileNode downloadFileNode = m_downloadFileList[m_fileIndex];

				return TryDownloadData(downloadFileNode);
			}
		}

		private bool AreAllFileDownloadCompleted()
		{
			lock (m_downloadFileList)
			{
				return (m_downloadFileList.Count == 0 || m_fileIndex >= m_downloadFileList.Count);
			}
		}

		private void UpdateDownloadSpeed(DownloadProgressChangedEventArgs e)
		{
			m_speedStopwatch.Stop();

			double totalMilliseconds = Math.Max(m_speedStopwatch.Elapsed.TotalMilliseconds, 1);
			long bytesDownloaded = e.BytesReceived - m_lastBytesReceived;
			m_lastBytesReceived = e.BytesReceived;
			double speedSample = (double)bytesDownloaded / (totalMilliseconds / 1000.0);
			m_speedSampleArray[m_speedSampleIndex++ % DEFAULT_SPEED_SAMPLES] = speedSample;

			double speedSampleTotal = 0.0;

			for (int i = 0; i < m_speedSampleArray.Length; i++)
				speedSampleTotal += m_speedSampleArray[i];

			m_downloadSpeed = speedSampleTotal / m_speedSampleArray.Length;

			m_speedStopwatch.Reset();
			m_speedStopwatch.Start();
		}

		private bool TryGetUrlContentLength(DownloadFileNode downloadFileNode, out long contentLength)
		{
			contentLength = 0;

			try
			{
				HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(downloadFileNode.SourceUrl);

				webRequest.Referer = GetParentUriString(downloadFileNode.SourceUrl);
				webRequest.UserAgent = DEFAULT_USER_AGENT;

				using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
				{
					contentLength = webResponse.ContentLength;

					return true;
				}
			}
			catch
			{
			}

			return false;
		}

		private bool TryDownloadData(DownloadFileNode downloadFileNode)
		{
			try
			{
				if (m_webClient != null)
				{
					m_webClient.Dispose();
					m_webClient = null;
				}

				Uri uri = new Uri(downloadFileNode.SourceUrl);

				m_webClient = new WebClient();
				m_webClient.Headers.Add(HttpRequestHeader.UserAgent, DEFAULT_USER_AGENT);

				m_webClient.DownloadDataCompleted += OnDownloadDataCompleted;
				m_webClient.DownloadProgressChanged += OnDownloadProgressChanged;
				m_webClient.DownloadDataAsync(uri, downloadFileNode);

				if (FileDownloadStarted != null)
					FileDownloadStarted(this, new FileDownloadEventArgs(downloadFileNode));

				return true;
			}
			catch (Exception ex)
			{
				if (FileDownloadError != null)
					FileDownloadError(this, new FileDownloadErrorEventArgs(downloadFileNode, ex));
			}

			return false;
		}

		private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			if (FileDownloadProgressChanged == null)
				return;

			DownloadFileNode downloadFileNode = (DownloadFileNode)e.UserState;

			if (downloadFileNode.TotalBytes == 0)
			{
				downloadFileNode.TotalBytes = e.TotalBytesToReceive;
				m_totalBytesToReceive += e.TotalBytesToReceive;
			}

			UpdateDownloadSpeed(e);

			FileDownloadProgressChangedEventArgs fileDownloadProgressChangedEventArgs = new FileDownloadProgressChangedEventArgs(downloadFileNode);

			fileDownloadProgressChangedEventArgs.CurrentBytesReceived = e.BytesReceived;
			fileDownloadProgressChangedEventArgs.CurrentTotalBytesToReceive = e.TotalBytesToReceive;
			fileDownloadProgressChangedEventArgs.CurrentProgressPercentage = e.ProgressPercentage;
			fileDownloadProgressChangedEventArgs.TotalBytesReceived = m_totalBytesReceived + e.BytesReceived;
			fileDownloadProgressChangedEventArgs.TotalBytesToReceive = m_totalBytesToReceive;
			fileDownloadProgressChangedEventArgs.TotalProgressPercentage = MathTools.Clamp<int>((int)((double)fileDownloadProgressChangedEventArgs.TotalBytesReceived / (double)m_totalBytesToReceive * 100.0), 0, 100);
			fileDownloadProgressChangedEventArgs.DownloadSpeed = m_downloadSpeed;

			FileDownloadProgressChanged(this, fileDownloadProgressChangedEventArgs);
		}

		private void OnDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			WebClient webClient = (WebClient)sender;
			DownloadFileNode downloadFileNode = (DownloadFileNode)e.UserState;

			try
			{
				if (e.Error != null)
				{
					if (FileDownloadError != null)
						FileDownloadError(this, new FileDownloadErrorEventArgs(downloadFileNode, e.Error));

					return;
				}

				if (e.Cancelled)
				{
					if (FileDownloadCancelled != null)
						FileDownloadCancelled(this, new FileDownloadEventArgs(downloadFileNode));

					return;
				}

				if (e.Result == null)
					return;

				if (!Directory.Exists(downloadFileNode.DestinationFolder))
					Directory.CreateDirectory(downloadFileNode.DestinationFolder);

				string destinationFileName = Path.Combine(downloadFileNode.DestinationFolder, downloadFileNode.FileName);

				File.WriteAllBytes(destinationFileName, e.Result);

				FileInfo fileInfo = new FileInfo(destinationFileName);
				long fileSize = fileInfo.Length;

				if (fileSize == 0)
				{
					File.Delete(destinationFileName);

					return;
				}

				if (FileDownloadCompleted != null)
					FileDownloadCompleted(this, new FileDownloadEventArgs(downloadFileNode));
			}
			catch (Exception ex)
			{
				if (FileDownloadError != null)
					FileDownloadError(this, new FileDownloadErrorEventArgs(downloadFileNode, ex));
			}
			finally
			{
				if (webClient != null)
				{
					webClient.Dispose();
					webClient = null;
				}

				m_totalBytesReceived += downloadFileNode.TotalBytes;

				m_fileIndex++;

				if (AreAllFileDownloadCompleted())
				{
					if (AllFilesDownloadCompleted != null)
						AllFilesDownloadCompleted(this, EventArgs.Empty);
				}
				else
					TryDownload();
			}
		}

		private string GetParentUriString(string url)
		{
			Uri uri = new Uri(url);
			return uri.AbsoluteUri.Remove(uri.AbsoluteUri.Length - uri.Segments[uri.Segments.Length - 1].Length - uri.Query.Length);
		}

		public static string FormatSizeBinary(long size)
		{
			return FileDownloader.FormatSizeBinary(size, DEFAULT_DECIMALS);
		}

		public static string FormatSizeBinary(long size, int decimals)
		{
			// By De Dauw Jeroen - April 2009 - jeroen_dedauw@yahoo.com
			String[] sizeArray = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB" };
			Double formattedSize = size;
			Int32 sizeIndex = 0;
			while (formattedSize >= 1024 && sizeIndex < sizeArray.Length)
			{
				formattedSize /= 1024;
				sizeIndex += 1;
			}
			return String.Format("{0} {1}", Math.Round(formattedSize, decimals), sizeArray[sizeIndex]);
		}

		public static string FormatSizeDecimal(long size)
		{
			return FileDownloader.FormatSizeDecimal(size, DEFAULT_DECIMALS);
		}

		public static string FormatSizeDecimal(long size, int decimals)
		{
			// By De Dauw Jeroen - April 2009 - jeroen_dedauw@yahoo.com
			String[] sizeArray = { "B", "kB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
			Double formattedSize = size;
			Int32 sizeIndex = 0;
			while (formattedSize >= 1000 && sizeIndex < sizeArray.Length)
			{
				formattedSize /= 1000;
				sizeIndex += 1;
			}
			return String.Format("{0} {1}", Math.Round(formattedSize, decimals), sizeArray[sizeIndex]);
		}

		#region Protected methods

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(Boolean disposing)
		{
			if (!m_disposed)
			{
				if (disposing)
				{
					Cancel();
				}
			}
		}
		#endregion

		public int FileIndex
		{
			get { return m_fileIndex; }
		}

		public int FileCount
		{
			get { return m_downloadFileList.Count; }
		}

		public double DownloadSpeed
		{
			get { return m_downloadSpeed; }
		}
	}

	public class FileDownloadProgressChangedEventArgs : EventArgs
	{
		public DownloadFileNode DownloadFile = null;
		public long CurrentBytesReceived = 0;
		public long CurrentTotalBytesToReceive = 0;
		public int CurrentProgressPercentage = 0;
		public long TotalBytesReceived = 0;
		public long TotalBytesToReceive = 0;
		public int TotalProgressPercentage = 0;
		public double DownloadSpeed = 0.0;

		public FileDownloadProgressChangedEventArgs(DownloadFileNode downloadFile)
			: base()
		{
			DownloadFile = downloadFile;
		}
	}

	public class FileDownloadErrorEventArgs : EventArgs
	{
		public DownloadFileNode DownloadFile = null;
		public Exception Error = null;

		public FileDownloadErrorEventArgs(DownloadFileNode downloadFile, Exception error)
			: base()
		{
			DownloadFile = downloadFile;
			Error = error;
		}
	}

	public class FileDownloadEventArgs : EventArgs
	{
		public DownloadFileNode DownloadFile = null;

		public FileDownloadEventArgs(DownloadFileNode downloadFile)
			: base()
		{
			DownloadFile = downloadFile;
		}
	}

	public class DownloadFileNode : ICloneable
	{
		public string Name = null;
		private string m_fileName = null;
		public string Version = null;
		public string Author = null;
		public string Category = null;
		public string Description = null;
		public string Folder = null;
		public string SourceUrl = null;
		public string DestinationFolder = null;
		public string ExtractionFolder = null;
		public bool NeedsPostProcessing = false;
		public long TotalBytes = 0;
		public object Tag = null;

		public DownloadFileNode(string sourceUrl, string destinationFolder)
		{
			SourceUrl = sourceUrl;
			DestinationFolder = destinationFolder;
			FileName = GetFileName(sourceUrl);
		}

		private string GetFileName(string url)
		{
			if (String.IsNullOrEmpty(url))
				return url;

			Uri uri = new Uri(url);
			string filePath = uri.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);

			return Path.GetFileName(filePath);
		}

		public string FileName
		{
			get { return m_fileName; }
			set { m_fileName = value; NeedsPostProcessing = Array.IndexOf(new string[] { ".zip", ".7zip", ".7z", ".rar", ".exe" }, Path.GetExtension(FileName)) > -1; }
		}

		public string Id
		{
			get
			{
				string downloadId = String.Format("{0}{1}", (SourceUrl ?? "").ToLower(), (DestinationFolder ?? "").ToLower());
				MD5 md5Hasher = MD5.Create();
				byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(downloadId));
				Guid guid = new Guid(data);
				return guid.ToString();
			}
		}

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}

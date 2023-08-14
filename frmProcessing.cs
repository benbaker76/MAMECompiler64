using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MAMECompiler64
{
	public partial class frmProcessing : Form
	{
		public delegate void UpdateProgressBarDelegate(int percent, string message);

		public frmProcessing()
		{
			InitializeComponent();
		}

		private void frmProcessing_Load(object sender, EventArgs e)
		{
		}

		public void UpdateProgressBar(int percent, string message)
		{
			if (progressBar1.InvokeRequired)
			{
				UpdateProgressBarDelegate updateProgressBar = new UpdateProgressBarDelegate(UpdateProgressBar);
				this.Invoke(updateProgressBar, percent, message);
			}
			else
			{
				progressBar1.Message = message;
				progressBar1.Value = MathTools.Clamp<int>(percent, 0, 100);
			}
		}

		protected override bool ShowWithoutActivation
		{
			get { return true; }
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= Win32.WS_EX_NOACTIVATE | Win32.WS_EX_TOPMOST;
				return createParams;
			}
		}

		protected override void WndProc(ref Message msg)
		{
			switch (msg.Msg)
			{
				case Win32.WM_MOUSEACTIVATE:
					msg.Result = (IntPtr)Win32.MA_NOACTIVATE;
					return;
			}

			base.WndProc(ref msg);
		}
	}
}

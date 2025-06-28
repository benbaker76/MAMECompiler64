using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MAMECompiler64
{
	public class Win32
	{
		public const int WS_EX_NOACTIVATE = 0x08000000;
		public const int WS_EX_TOPMOST = 0x00000008;
		public const int WM_MOUSEACTIVATE = 0x0021;
		public const int MA_NOACTIVATE = 3;  

		[DllImport("kernel32.dll")]
		public static extern void OutputDebugString(string lpOutputString);
	}
}

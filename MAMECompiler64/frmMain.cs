#define TOOLS_MAME0161_UPDATE
#define TOOLS_MAME0162_UPDATE
#define TOOLS_MAME0168_UPDATE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Reflection;

namespace MAMECompiler64
{
	public partial class frmMain : Form
	{
		private Process m_mameProcess = null;
		private Process m_patchProcess = null;
		private Process m_diffProcess = null;
		private Stopwatch m_stopWatch = null;
		private System.Windows.Forms.Timer m_compileTimer = null;
		private ToolTip m_toolTip = null;

		private string[] PrefixStripValues =
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8"
		};

		private string[] TargetOSNames =
        {
#if TOOLS_MAME0161_UPDATE
			"Windows",
#else
			"Win32",
#endif
            "OS2",
            "Linux",
            "Solaris",
            "FreeBSD",
            "GNU/kFreeBSD",
            "NetBSD",
            "OpenBSD",
            "Darwin",
            "Haiku",
			"SunOS"
        };

		private string[] TargetOSValues =
        {
#if TOOLS_MAME0161_UPDATE
			"windows",
#else
			"win32",
#endif
            "os2",
            "linux",
            "solaris",
            "freebsd",
            "freebsd",
            "netbsd",
            "openbsd",
            "macosx",
            "haiku",
			"solaris"
        };

		private string[] TargetNames =
        {
            "MAME",
            "MESS",
            "UME"
        };

		private string[] TargetValues =
        {
            "mame",
            "mess",
            "ume"
        };

		private string[] SubTargetNames =
        {
            "Default",
#if TOOLS_MAME0162_UPDATE
			"Arcade",
			"MESS",
#endif
            "Tiny"
        };

		private string[] SubTargetValues =
        {
            "",			// Full build
#if TOOLS_MAME0162_UPDATE
			"arcade",	// Arcade only drivers
			"mess",		// MESS only drivers
#endif
            "tiny"
        };

		private string[] OSDNames =
        {
            "Windows",
            "WinUI",
            "SDL"
        };

		private string[] OSDValues =
        {
            "windows",
            "winui",
            "sdl"
        };

		private string[] OptimizeNames =
        {
            "None",
            "Auto Detect",
            "Intel Core2",
            "Pentium M, Intel Core",
            "Pentium 4 (+ 64-bit)",
            "Pentium 4 (+ SSE3)",
            "Pentium 4 (- SSE3)",
            "AMD 64",
            "Athlon XP",
            "Motorola G4 (>= 7450)",
            "Motorola G4 (<= 7447)",
            "IBM G5",
            "IBM G4",
            "IBM G3"
        };

		private string[] OptimizeValues =
        {
            "",
            "-march=native",
            "-march=core2",
            "-march=pentium-m",
            "-march=nocona",
            "-march=prescott",
            "-march=pentium4",
            "-march=athlon64",
            "-march=athlon-xp",
            "-mcpu=7450",
            "-mcpu=7400",
            "-mcpu=G5",
            "-mcpu=G4",
            "-mcpu=G3"
        };

		private string[] CompilerParallelJobs =
		{
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9",
			"10",
			"11",
			"12"
		};

		private string[] SymbolLevelNames =
        {
            "Backtrace",
            "Default"
        };

		private string[] SymbolLevelValues =
        {
            "1",
            "2"
        };

		private string[] OptimizeLevelNames =
        {
            "0 (Symbols)",
            "1",
            "2",
            "3 (Default)"
        };

		private string[] OptimizeLevelValues =
        {
            "0",
            "1",
            "2",
            "3"
        };

		private enum PatchMode
		{
			Test,
			Reverse,
			Apply
		};

		private BuildOptionNode[] BuildOptions =
		{
			new BuildOptionNode("No Warnings as Errors",		"General",		"NoWError",				true,	"CheckBox",		"NOWERROR",				"No warnings as errors (NOWERROR)"),
			
			new BuildOptionNode("64-bit Target",				"Build",		"64Bit",				null,	"CheckBox",		"PTR64",				"Building for a 64-bit target"),
			new BuildOptionNode("Big Endian",					"Build",		"BigEndian",			null,	"CheckBox",		"BIGENDIAN",			"Building for a big-endian target"),
			new BuildOptionNode("No ASM",						"Build",		"NoASM",				false,	"CheckBox",		"NOASM",				"No ASM"),
			new BuildOptionNode("Include Profile Info",			"Build",		"IncludeProfileInfo",	false,	"CheckBox",		"PROFILE",				"Include profiling information from the compiler"),
			new BuildOptionNode("Generate Link Map",			"Build",		"GenerateLinkMap",		false,	"CheckBox",		"MAP",					"Generate a link map for exception handling in windows"),
			new BuildOptionNode("Verbose Build Info",			"Build",		"VerboseBuildInfo",		false,	"CheckBox",		"VERBOSE",				"Generate verbose build information"),
			new BuildOptionNode("Generate Deprecated",			"Build",		"GenerateDeprecated",	false,	"CheckBox",		"DEPRECATED",			"Generate deprecation warnings during compilation"),
			new BuildOptionNode("Enable LTO",					"Build",		"EnableLTO",			false,	"CheckBox",		"LTO",					"Enable LTO (link-time optimizations)"),
			new BuildOptionNode("Don't Use Network",			"Build",		"DontUseNetwork",		false,	"CheckBox",		"DONT_USE_NETWORK",		"Disable networking"),
			new BuildOptionNode("Enable SSE2",					"Build",		"EnableSSE2",			false,	"CheckBox",		"SSE2",					"Enable SSE2 optimized code and SSE2 code generation\n(implicitly enabled by 64-bit compilers)"),
			new BuildOptionNode("Enable OpenMP",				"Build",		"EnableOpenMP",			false,	"CheckBox",		"OPENMP",				"Enable OpenMP optimized code"),
			
			new BuildOptionNode("Debug Build",					"Program",		"GenerateDebugBuild",	false,	"CheckBox",		"DEBUG",				"Build a debug version"),
			new BuildOptionNode("Fast Debug",					"Program",		"FastDebugBuild",		false,	"CheckBox",		"FASTDEBUG",			"Disable some debug-related hotspots/slowdowns (e.g. for profiling)"),
			new BuildOptionNode("Include Internal Profiler",	"Program",		"IncludeProfiler",		false,	"CheckBox",		"PROFILER",				"Include the internal profiler"),
			new BuildOptionNode("Force DRC C Backend",			"Program",		"ForceDrcCBackend",		false,	"CheckBox",		"FORCE_DRC_C_BACKEND",	"Force the universal DRC to always use the C backend\nyou may need to do this if your target architecture does not have\na native backend"),
			new BuildOptionNode("Ignore Bad Localisation",		"Program",		"IgnoreBadLocalisation",false,	"CheckBox",		"IGNORE_BAD_LOCALISATION",	"Ignore Bad Localisation."),
			
			new BuildOptionNode("Force Direct Input",			"Input",		"ForceDirectInput",		false,	"CheckBox",		"[CUSTOM]",				"Force MAME to use DirectInput instead of Raw Input.\nEnables 3rd party tools to send keys to MAME."),
			new BuildOptionNode("Disable Use of XInput",		"Input",		"NoUseXInput",			false,	"CheckBox",		"NO_USE_XINPUT",		"Disable the use of XInput."),

			new BuildOptionNode("Disable Use of MIDI",			"Audio",		"NoUseMIDI",			false,	"CheckBox",		"NO_USE_MIDI",			"Disable the use of MIDI."),
			new BuildOptionNode("Disable Use of Port Audio",	"Audio",		"NoUsePortAudio",		false,	"CheckBox",		"NO_USE_PORTAUDIO",		"Disable the use of Port Audio."),

			new BuildOptionNode("Include Symbols",				"Symbols",		"IncludeSymbols",		false,	"CheckBox",		"SYMBOLS",				"Include the symbols"),
			new BuildOptionNode("Symbol Level",					"Symbols",		"SymbolLevel",			1,		"ComboBox",		"SYMLEVEL",				"Specify symbols level\n(default is SYMLEVEL = 2 normally;\nuse 1 if you only need backtrace)"),
			new BuildOptionNode("Dump Symbols",					"Symbols",		"DumpSymbols",			false,	"CheckBox",		"DUMPSYM",				"Dump the symbols to a .sym file"),
			new BuildOptionNode("Strip Symbols",				"Symbols",		"StripSymbols",			true,	"CheckBox",		"STRIP_SYMBOLS",		"Strip symbols"),
		};

		private void SetMakefileValues()
		{
			//SetMakefileValue("DEBUG", chkDebug.Checked ? "1" : "0");

			List<string> makeParamsList = new List<string>();

			makeParamsList.Add("-R");
			makeParamsList.Add(String.Format("-j{0}", cboCompilerParallelJobs.Text));

			SetMakefileValue("MAKEPARAMS", String.Join(" ", makeParamsList.ToArray()));

			List<string> ldOptsParamsList = new List<string>();

			ldOptsParamsList.Add("-static");

			SetMakefileValue("LDOPTS", String.Join(" ", ldOptsParamsList.ToArray()));
		}

		private bool TryGetBuildOption(string name, out BuildOptionNode buildOptionNode)
		{
			buildOptionNode = null;

			foreach (BuildOptionNode buildOption in BuildOptions)
			{
				if (buildOption.IniName.Equals(name))
				{
					buildOptionNode = buildOption;

					return true;
				}
			}

			return false;
		}

		private bool TryGetBuildControl<T>(string name, out T control)
		{
			control = default(T);
			BuildOptionNode buildOption = null;

			if (TryGetBuildOption(name, out buildOption))
			{
				control = (T)Convert.ChangeType(buildOption.Control, typeof(T));

				return true;
			}

			return false;
		}

		private string GetMAMEParameters()
		{
			List<string> paramList = new List<string>();

			try
			{
				// -Wno-array-bounds
				// -Wno-narrowing

				string minGW32Folder = String.Empty;
				string minGW64Folder = String.Empty;
				string minGWFolder = GetMinGWFolder(out minGW32Folder, out minGW64Folder);
				string bin32Folder = Path.Combine(minGW32Folder, "bin");
				string bin64Folder = Path.Combine(minGW64Folder, "bin");
				string binFolder = Path.Combine(minGWFolder, "bin");

				//paramList.Add("OS=Windows_NT"); // Required for MAME 0158+
				paramList.Add("PYTHON_AVAILABLE=python"); // Required for MAME 0161+
				paramList.Add("SHELLTYPE=msdos");

				// **** General Options ****

				paramList.Add(String.Format("-j{0}", cboCompilerParallelJobs.Text));

				if (cboOptimize.SelectedIndex != 0)
					paramList.Add(String.Format("ARCHOPTS={0}", OptimizeValues[cboOptimize.SelectedIndex]));

				if (cboOptimizeLevel.SelectedIndex != 3)
					paramList.Add(String.Format("OPTIMIZE={0}", OptimizeLevelValues[cboOptimizeLevel.SelectedIndex]));

				// **** Target Options ****

				paramList.Add(String.Format("TARGETOS={0}", TargetOSValues[cboTargetOS.SelectedIndex]));
				paramList.Add(String.Format("TARGET={0}", TargetValues[cboTarget.SelectedIndex]));

				if (cboSubTarget.SelectedIndex != 0)
					paramList.Add(String.Format("SUBTARGET={0}", SubTargetValues[cboSubTarget.SelectedIndex]));

				paramList.Add(String.Format("OSD={0}", OSDValues[cboOSD.SelectedIndex]));

				// **** Compile Options ****

				foreach (BuildOptionNode buildOption in BuildOptions)
				{
					if (buildOption.Constant.Equals("[CUSTOM]"))
						continue;

					if (buildOption.Control is CheckBox)
					{
						CheckBox checkBox = (CheckBox)buildOption.Control;

						if (checkBox.Checked)
							paramList.Add(String.Format("{0}={1}", buildOption.Constant, checkBox.Checked ? "1" : "0"));
					}
					else if (buildOption.Control is ComboBox)
					{
						ComboBox comboBox = (ComboBox)buildOption.Control;

						switch (buildOption.IniName)
						{
							case "SymbolLevel":
								paramList.Add(String.Format("{0}={1}", buildOption.Constant, SymbolLevelValues[comboBox.SelectedIndex]));
								break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				OutputMessage("GetMAMEParameters()", ex.Message, Color.Red);
			}

			return String.Join(" ", paramList.ToArray());
		}

		private int GetCompilerParallelJobCount()
		{
			// Should be 1.5x the number of cores
			int processorCount = Environment.ProcessorCount;

			return Clamp<int>((int)Math.Ceiling(processorCount * 1.5f), 1, 12);
		}

		private void CreateBuildControls(Control parent)
		{
			List<Control> controlList = new List<Control>();
			string iniSection = null;
			int x = 8;
			int y = 0;

			foreach (BuildOptionNode buildOption in BuildOptions)
			{
				if (iniSection != buildOption.IniSection)
				{
					y += 4;
					Label labelSection = new Label();
					labelSection.Location = new Point(x, y);
					labelSection.Font = new Font(labelSection.Font.Name, labelSection.Font.Size, FontStyle.Bold);
					labelSection.Name = buildOption.IniSection;
					labelSection.Text = buildOption.IniSection;
					y += labelSection.Height;

					/* Label labelLine = new Label();
					labelLine.Location = new Point(x, y);
					labelLine.Size = new Size(parent.Width - 32, 2);
					labelLine.Text = "";
					labelLine.BorderStyle = BorderStyle.Fixed3D;
					labelLine.AutoSize = false;
					y += labelLine.Height; */

					iniSection = buildOption.IniSection;

					controlList.Add(labelSection);
					//controlList.Add(labelLine);
				}

				if (buildOption.Type.Equals("CheckBox"))
				{
					CheckBox checkBox = new CheckBox();
					checkBox.Location = new Point(x, y);
					checkBox.Width = parent.Width - 32;
					checkBox.Name = buildOption.IniName;
					checkBox.Text = buildOption.Name;
					y += checkBox.Height;

					buildOption.Control = checkBox;

					controlList.Add(checkBox);
				}
				else if (buildOption.Type.Equals("ComboBox"))
				{
					ComboBox comboBox = new ComboBox();
					comboBox.Location = new Point(x, y);
					comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
					comboBox.Name = buildOption.IniName;
					comboBox.Text = buildOption.Name;
					y += comboBox.Height;

					switch (buildOption.IniName)
					{
						case "SymbolLevel":
							comboBox.Items.AddRange(SymbolLevelNames);
							break;
						default:
							break;
					}

					buildOption.Control = comboBox;

					controlList.Add(comboBox);
				}
			}

			parent.SuspendLayout();
			parent.Controls.Clear();
			parent.Controls.AddRange(controlList.ToArray());
			parent.ResumeLayout();
		}

		private void SetToolTips()
		{
			m_toolTip = new ToolTip();

			m_toolTip.AutoPopDelay = 5000;
			m_toolTip.InitialDelay = 1000;
			m_toolTip.ReshowDelay = 500;
			m_toolTip.ShowAlways = true;

			m_toolTip.SetToolTip(butBuildToolsFolder, "Select MinGW Folder");
			m_toolTip.SetToolTip(butSourceFolder, "Select Source Folder");
			m_toolTip.SetToolTip(butPatchFolder, "Select Patch Folder");
			m_toolTip.SetToolTip(butOriginalSourceFolder, "Select Original Source Folder");
			m_toolTip.SetToolTip(butModifiedSourceFolder, "Select Modified Source Folder");
			m_toolTip.SetToolTip(butMAMESourceFolder, "Select MAME Source Folder");

			m_toolTip.SetToolTip(butOpenBuildToolsFolder, "Open Folder in File Explorer");
			m_toolTip.SetToolTip(butOpenSourceFolder, "Open Folder in File Explorer");
			m_toolTip.SetToolTip(butOpenPatchFolder, "Open Folder in File Explorer");
			m_toolTip.SetToolTip(butOpenOriginalSourceFolder, "Open Folder in File Explorer");
			m_toolTip.SetToolTip(butOpenModifiedSourceFolder, "Open Folder in File Explorer");
			m_toolTip.SetToolTip(butOpenMAMESourceFolder, "Open Folder in File Explorer");

			m_toolTip.SetToolTip(butDownloadFileList, "Download Latest File List");
			m_toolTip.SetToolTip(butUpdateCompileTools, "Update the Compile Tools");
			m_toolTip.SetToolTip(butUpdateMAMECompiler, "Update MAME Compiler");
			m_toolTip.SetToolTip(butDownloadSelected, "Download Selected Files");

			m_toolTip.SetToolTip(butDiffPatchFile, "Select Diff Patch File");
			m_toolTip.SetToolTip(butOpenDiffPatchFolder, "Open Folder in File Explorer");
			m_toolTip.SetToolTip(chkEOLConversion, "Convert End of Line Characters");
			m_toolTip.SetToolTip(cboPrefixStrip, "Strip leading slashes from each file name in the patch file");

			m_toolTip.SetToolTip(butTestDiffPatch, "Test Diff Patch (Dry Run)");
			m_toolTip.SetToolTip(butReverseDiffPatch, "Reverse Diff Patch");
			m_toolTip.SetToolTip(butApplyDiffPatch, "Apply Diff Patch");
			m_toolTip.SetToolTip(butCreateDiffPatch, "Create Diff Patch");

			m_toolTip.SetToolTip(cboOptimize, "Architecture-specific optimization");
			m_toolTip.SetToolTip(cboOptimizeLevel, "Specify optimization level or leave commented to use the default\n(default is OPTIMIZE = 3 normally, or OPTIMIZE = 0 with symbols)");
			m_toolTip.SetToolTip(cboCompilerParallelJobs, "Number of parallel jobs the compiler can run");
			m_toolTip.SetToolTip(chkCleanCompile, "Perform a clean compile (delete 'obj' path)");

			m_toolTip.SetToolTip(cboTargetOS, "Set the underlying OS");
			m_toolTip.SetToolTip(cboTarget, "Set the core target");
			m_toolTip.SetToolTip(cboSubTarget, "Specify the sub target");
			m_toolTip.SetToolTip(cboOSD, "Specify the OSD layer");

			//-------------------------------------------------
			// specify program options; see each option below
			// for details
			//-------------------------------------------------

			foreach (BuildOptionNode buildOption in BuildOptions)
				m_toolTip.SetToolTip(buildOption.Control, buildOption.ToolTip);
		}

		private void LoadSettings()
		{
			try
			{
				using (IniFile iniFile = new IniFile(Path.Combine(Settings.Folders.MAMECompiler64, "Settings.ini")))
				{
					string SystemDrive = Environment.GetEnvironmentVariable("SystemDrive") + "\\";
					string buildTools = Path.Combine(SystemDrive, "buildtools");
					string oldFolder = Path.Combine(buildTools, @"old");
					string srcFolder = Path.Combine(buildTools, @"src");
					string patchFolder = Path.Combine(buildTools, @"patch");
					string mameSourceFolder = Path.Combine(srcFolder, "MAME0176");
					string mameSourceOldFolder = Path.Combine(mameSourceFolder, "old");
					string mameSourceSrcFolder = Path.Combine(mameSourceFolder, "src");
					string diffPathFile = Path.Combine(patchFolder, @"suppression_0182u0.txt");

					txtBuildToolsFolder.Text = iniFile.Read("General", "BuildToolsFolder", buildTools);
					txtSourceFolder.Text = iniFile.Read("General", "SourceFolder", srcFolder);
					txtPatchFolder.Text = iniFile.Read("General", "PatchFolder", patchFolder);
					txtMAMESourceFolder.Text = iniFile.Read("General", "MAMESourceFolder", mameSourceFolder);
					txtDiffPatchFile.Text = iniFile.Read("General", "DiffPatchFile", diffPathFile);
					txtOriginalSourceFolder.Text = iniFile.Read("General", "OriginalSourceFolder", mameSourceOldFolder);
					txtModifiedSourceFolder.Text = iniFile.Read("General", "ModifiedSourceFolder", mameSourceSrcFolder);
					txtOutputPatchFile.Text = iniFile.Read("General", "OutputPatchFile");

					chkEOLConversion.Checked = iniFile.Read<bool>("General", "EOLConversion", true);
					cboPrefixStrip.SelectedIndex = iniFile.Read<int>("General", "PrefixStrip", 0);

					cboOptimize.SelectedIndex = iniFile.Read<int>("General", "Optimize", 0);
					cboOptimizeLevel.SelectedIndex = iniFile.Read<int>("General", "OptimizeLevel", 3);
					cboCompilerParallelJobs.Text = iniFile.Read<int>("General", "CompilerParallelJobs", GetCompilerParallelJobCount()).ToString();
					chkCleanCompile.Checked = iniFile.Read<bool>("General", "CleanCompile", false);

					cboTargetOS.SelectedIndex = iniFile.Read<int>("Target", "TargetOS", 0);
					cboTarget.SelectedIndex = iniFile.Read<int>("Target", "Target", 0);
					cboSubTarget.SelectedIndex = iniFile.Read<int>("Target", "SubTarget", 0);
					cboOSD.SelectedIndex = iniFile.Read<int>("Target", "OSD", 0);

					foreach (BuildOptionNode buildOption in BuildOptions)
					{
						if (buildOption.Control is CheckBox)
						{
							CheckBox checkBox = (CheckBox)buildOption.Control;
							bool defaultValue = false;

							if (buildOption.IniDefault == null)
							{
								switch (buildOption.IniName)
								{
									case "64Bit":
										defaultValue = Is64BitMode();
										break;
									case "BigEndian":
										defaultValue = !BitConverter.IsLittleEndian;
										break;
									default:
										defaultValue = (bool)buildOption.IniDefault;
										break;
								}
							}

							checkBox.Checked = iniFile.Read<bool>(buildOption.IniSection, buildOption.IniName, defaultValue);
						}
						else if (buildOption.Control is ComboBox)
						{
							ComboBox comboBox = (ComboBox)buildOption.Control;

							comboBox.SelectedIndex = iniFile.Read<int>(buildOption.IniSection, buildOption.IniName, (int)buildOption.IniDefault);
						}
					}

					ReadWindowPosition(iniFile, this);
				}
			}
			catch (Exception ex)
			{
				OutputMessage("LoadSettings()", ex.Message, Color.Red);
			}
		}

		private void SaveSettings()
		{
			try
			{
				using (IniFile iniFile = new IniFile(Path.Combine(Settings.Folders.MAMECompiler64, "Settings.ini")))
				{
					iniFile.Write("General", "BuildToolsFolder", txtBuildToolsFolder.Text);
					iniFile.Write("General", "SourceFolder", txtSourceFolder.Text);
					iniFile.Write("General", "PatchFolder", txtPatchFolder.Text);
					iniFile.Write("General", "MAMESourceFolder", txtMAMESourceFolder.Text);
					iniFile.Write("General", "DiffPatchFile", txtDiffPatchFile.Text);
					iniFile.Write("General", "OriginalSourceFolder", txtOriginalSourceFolder.Text);
					iniFile.Write("General", "ModifiedSourceFolder", txtModifiedSourceFolder.Text);
					iniFile.Write("General", "OutputPatchFile", txtOutputPatchFile.Text);

					iniFile.Write<bool>("General", "EOLConversion", chkEOLConversion.Checked);
					iniFile.Write<int>("General", "PrefixStrip", cboPrefixStrip.SelectedIndex);

					iniFile.Write<int>("General", "Optimize", cboOptimize.SelectedIndex);
					iniFile.Write<int>("General", "OptimizeLevel", cboOptimizeLevel.SelectedIndex);
					iniFile.Write("General", "CompilerParallelJobs", cboCompilerParallelJobs.Text);
					iniFile.Write<bool>("General", "CleanCompile", chkCleanCompile.Checked);

					iniFile.Write<int>("Target", "TargetOS", cboTargetOS.SelectedIndex);
					iniFile.Write<int>("Target", "Target", cboTarget.SelectedIndex);
					iniFile.Write<int>("Target", "SubTarget", cboSubTarget.SelectedIndex);
					iniFile.Write<int>("Target", "OSD", cboOSD.SelectedIndex);

					foreach (BuildOptionNode buildOption in BuildOptions)
					{
						if (buildOption.Control is CheckBox)
						{
							CheckBox checkBox = (CheckBox)buildOption.Control;

							iniFile.Write<bool>(buildOption.IniSection, buildOption.IniName, checkBox.Checked);
						}
						else if (buildOption.Control is ComboBox)
						{
							ComboBox comboBox = (ComboBox)buildOption.Control;

							iniFile.Write<int>(buildOption.IniSection, buildOption.IniName, comboBox.SelectedIndex);
						}
					}

					WriteWindowPosition(iniFile, this);
				}
			}
			catch (Exception ex)
			{
				OutputMessage("SaveSettings()", ex.Message, Color.Red);
			}
		}

		public static void ReadWindowPosition(IniFile iniFile, Form form)
		{
			form.Size = iniFile.Read<Size>(form.Name, "Size", form.Size);
			form.Location = iniFile.Read<Point>(form.Name, "Location", form.Location);
			form.WindowState = iniFile.Read<FormWindowState>(form.Name, "WindowState", form.WindowState);
		}

		public static void WriteWindowPosition(IniFile iniFile, Form form)
		{
			iniFile.Write<FormWindowState>(form.Name, "WindowState", form.WindowState);

			if (form.WindowState == FormWindowState.Normal)
			{
				iniFile.Write<Size>(form.Name, "Size", form.Size);
				iniFile.Write<Point>(form.Name, "Location", form.Location);
			}
			else
			{
				iniFile.Write<Size>(form.Name, "Size", form.RestoreBounds.Size);
				iniFile.Write<Point>(form.Name, "Location", form.RestoreBounds.Location);
			}
		}

		public frmMain()
		{
			InitializeComponent();
		}

		private void EOLConversion(string fileName)
		{
			try
			{
				string text = File.ReadAllText(fileName);

				string[] lineArray = text.Split(new string[] { "\x0d\x0a", "\x0a" }, StringSplitOptions.None);

				File.WriteAllLines(fileName, lineArray);
			}
			catch (Exception ex)
			{
				OutputMessage("EOLConversion()", ex.Message, Color.Red);
			}
		}

		private void SetMakefileValue(string key, string value)
		{
			try
			{
				bool saveMakeFile = false;
				string makeFile = Path.Combine(txtMAMESourceFolder.Text, "makefile");

				if (!File.Exists(makeFile))
					return;

				string[] lines = File.ReadAllLines(makeFile);

				for (int i = 0; i < lines.Length; i++)
				{
					string line = lines[i];

					if (line.StartsWith("# "))
						line = line.Remove(0, 2);
					else if (line.StartsWith("#"))
						line = line.Remove(0, 1);

					if (line.StartsWith(key))
					{
						string[] lineSplit = line.Split(new char[] { '=' });

						if (lineSplit.Length == 2)
						{
							lines[i] = String.Format("{0}= {1}", lineSplit[0], value);

							saveMakeFile = true;
						}
					}
				}

				if (saveMakeFile)
					File.WriteAllLines(makeFile, lines);
			}
			catch (Exception ex)
			{
				OutputMessage("SetMakefileValue()", ex.Message, Color.Red);
			}
		}

		private void ForceDirectInput(bool enable)
		{
			try
			{
				string inputFile = Path.Combine(txtMAMESourceFolder.Text, @"src\osd\windows\input.c");

				if (!File.Exists(inputFile))
					return;

				string[] lineArray = File.ReadAllLines(inputFile);

				for (int i = 0; i < lineArray.Length; i++)
				{
					if (lineArray[i].Contains("#define FORCE_DIRECTINPUT"))
					{
						lineArray[i] = String.Format("#define FORCE_DIRECTINPUT\t{0}", enable ? 1 : 0);

						break;
					}
				}

				File.WriteAllLines(inputFile, lineArray);
			}
			catch (Exception ex)
			{
				OutputMessage("ForceDirectInput()", ex.Message, Color.Red);
			}
		}

		private void CopyMake()
		{
			try
			{
				string srcFileName = Path.Combine(Application.StartupPath, @"make.exe");
#if TOOLS_MAME0168_UPDATE
				string win32Folder = Path.Combine(txtBuildToolsFolder.Text, "win32");
				string destFileName = Path.Combine(win32Folder, "make.exe");
#else
				string minGWFolder = GetMinGWFolder();
				string destFileName = Path.Combine(minGWFolder, @"bin\make.exe");
#endif

				if (!File.Exists(srcFileName))
					return;

				if (!File.Exists(destFileName))
					return;

				File.Copy(srcFileName, destFileName, true);
			}
			catch (Exception ex)
			{
				OutputMessage("CopyMake()", ex.Message, Color.Red);
			}
		}

		private void CopyPatch()
		{
			try
			{
				string srcFileName = Path.Combine(Application.StartupPath, "patch.exe");
#if TOOLS_MAME0168_UPDATE
				string win32Folder = Path.Combine(txtBuildToolsFolder.Text, "win32");
				string destFileName = Path.Combine(win32Folder, "patch.exe");
#elif TOOLS_MAME0161_UPDATE
				string vendorFolder = Path.Combine(txtBuildToolsFolder.Text, "vendor");
				string unixToolsFolder = Path.Combine(vendorFolder, "unixtools");
				string destFileName = Path.Combine(unixToolsFolder, "patch.exe");
#endif

				if (!File.Exists(srcFileName))
					return;

				File.Copy(srcFileName, destFileName, true);
			}
			catch (Exception ex)
			{
				OutputMessage("CopyPatch()", ex.Message, Color.Red);
			}
		}

		private void CopyDiff()
		{
			try
			{
				string srcFileName1 = Path.Combine(Application.StartupPath, "diff.exe");
				string srcFileName2 = Path.Combine(Application.StartupPath, "libiconv2.dll");
				string srcFileName3 = Path.Combine(Application.StartupPath, "libintl3.dll");
#if TOOLS_MAME0168_UPDATE
				string win32Folder = Path.Combine(txtBuildToolsFolder.Text, "win32");
				string destFileName1 = Path.Combine(win32Folder, "diff.exe");
				string destFileName2 = Path.Combine(win32Folder, "libiconv2.dll");
				string destFileName3 = Path.Combine(win32Folder, "libintl3.dll");
#elif TOOLS_MAME0161_UPDATE
				string vendorFolder = Path.Combine(txtBuildToolsFolder.Text, "vendor");
				string unixToolsFolder = Path.Combine(vendorFolder, "unixtools");
				string destFileName1 = Path.Combine(unixToolsFolder, "diff.exe");
				string destFileName2 = Path.Combine(unixToolsFolder, "libiconv2.dll");
				string destFileName3 = Path.Combine(unixToolsFolder, "libintl3.dll");
#endif

				if (File.Exists(srcFileName1))
					File.Copy(srcFileName1, destFileName1, true);

				if (File.Exists(srcFileName2))
					File.Copy(srcFileName2, destFileName2, true);

				if (File.Exists(srcFileName3))
					File.Copy(srcFileName3, destFileName3, true);
			}
			catch (Exception ex)
			{
				OutputMessage("CopyDiff()", ex.Message, Color.Red);
			}
		}

		private void MoveSh()
		{
			try
			{
				string buildToolsFolder = txtBuildToolsFolder.Text;
				string usrBinFolder = Path.Combine(buildToolsFolder, @"usr\bin");
				string srcFileName = Path.Combine(usrBinFolder, "sh.exe");
				string destFileName = Path.Combine(usrBinFolder, "sh.exe.old");

				if (!File.Exists(srcFileName))
					return;

				File.Move(srcFileName, destFileName);
			}
			catch (Exception ex)
			{
				OutputMessage("MoveSh()", ex.Message, Color.Red);
			}
		}

		private void butMinGWFolder_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog fd = new FolderBrowserDialog();

				fd.SelectedPath = txtBuildToolsFolder.Text;

				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					txtBuildToolsFolder.Text = fd.SelectedPath;
				}
			}
			catch (Exception ex)
			{
				OutputMessage("butMinGWFolder_Click()", ex.Message, Color.Red);
			}
		}

		private void butSourceFolder_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog fd = new FolderBrowserDialog();

				fd.SelectedPath = txtSourceFolder.Text;

				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					txtSourceFolder.Text = fd.SelectedPath;
				}
			}
			catch (Exception ex)
			{
				OutputMessage("butSourceFolder_Click()", ex.Message, Color.Red);
			}
		}

		private void butPatchFolder_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog fd = new FolderBrowserDialog();

				fd.SelectedPath = txtPatchFolder.Text;

				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					txtPatchFolder.Text = fd.SelectedPath;
				}
			}
			catch (Exception ex)
			{
				OutputMessage("butPatchFolder_Click()", ex.Message, Color.Red);
			}
		}

		private void butMAMESourceFolder_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog fd = new FolderBrowserDialog();

				fd.SelectedPath = txtMAMESourceFolder.Text;

				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					txtMAMESourceFolder.Text = fd.SelectedPath;
				}
			}
			catch (Exception ex)
			{
				OutputMessage("butMAMESourceFolder_Click()", ex.Message, Color.Red);
			}
		}

		private void butOpenMinGWFolder_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(txtBuildToolsFolder.Text))
				System.Diagnostics.Process.Start("explorer.exe", txtBuildToolsFolder.Text);
		}

		private void butOpenSourceFolder_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(txtSourceFolder.Text))
				System.Diagnostics.Process.Start("explorer.exe", txtSourceFolder.Text);
		}

		private void butOpenPatchFolder_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(txtPatchFolder.Text))
				System.Diagnostics.Process.Start("explorer.exe", txtPatchFolder.Text);
		}

		private void butOpenMAMESourceFolder_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(txtMAMESourceFolder.Text))
				System.Diagnostics.Process.Start("explorer.exe", txtMAMESourceFolder.Text);
		}

		private void butOpenDiffPatchFolder_Click(object sender, EventArgs e)
		{
			string patchFolder = String.Empty;

			try
			{
				patchFolder = Path.GetDirectoryName(txtDiffPatchFile.Text);
			}
			catch
			{
			}

			if (!String.IsNullOrEmpty(patchFolder))
				if (Directory.Exists(patchFolder))
					System.Diagnostics.Process.Start("explorer.exe", patchFolder);
		}

		private void butDiffFile_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog fd = new OpenFileDialog();

				fd.RestoreDirectory = true;
				fd.Filter = "Diff Files (*.diff,*.txt).|*.diff;*.txt|All files (*.*)|*.*";

				string directory = null;

				if (FileIO.TryGetDirectoryName(txtDiffPatchFile.Text, out directory))
					fd.InitialDirectory = directory;

				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					txtDiffPatchFile.Text = fd.FileName;
				}
			}
			catch (Exception ex)
			{
				OutputMessage("butDiffFile_Click()", ex.Message, Color.Red);
			}
		}

		private void butOriginalSourceFolder_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog fd = new FolderBrowserDialog();

				fd.SelectedPath = txtOriginalSourceFolder.Text;

				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					txtOriginalSourceFolder.Text = fd.SelectedPath;
				}
			}
			catch (Exception ex)
			{
				OutputMessage("butOriginalSourceFolder_Click()", ex.Message, Color.Red);
			}
		}

		private void butOpenOriginalSourceFolder_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(txtOriginalSourceFolder.Text))
				System.Diagnostics.Process.Start("explorer.exe", txtOriginalSourceFolder.Text);
		}

		private void butModifiedSourceFolder_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog fd = new FolderBrowserDialog();

				fd.SelectedPath = txtModifiedSourceFolder.Text;

				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					txtModifiedSourceFolder.Text = fd.SelectedPath;
				}
			}
			catch (Exception ex)
			{
				OutputMessage("butModifiedSourceFolder_Click()", ex.Message, Color.Red);
			}
		}

		private void butOpenModifiedSourceFolder_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(txtModifiedSourceFolder.Text))
				System.Diagnostics.Process.Start("explorer.exe", txtModifiedSourceFolder.Text);
		}

		private void butOutputPatchFile_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog fd = new SaveFileDialog();

				fd.RestoreDirectory = true;
				fd.Filter = "Diff Files (*.diff,*.txt).|*.diff;*.txt|All files (*.*)|*.*";

				string directory = null;

				if(FileIO.TryGetDirectoryName(txtOutputPatchFile.Text, out directory))
					fd.InitialDirectory = directory;

				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					txtOutputPatchFile.Text = fd.FileName;
				}
			}
			catch (Exception ex)
			{
				OutputMessage("butOutputPatchFile_Click()", ex.Message, Color.Red);
			}
		}

		private void butOpenOutputPatchFile_Click(object sender, EventArgs e)
		{
			string patchFolder = String.Empty;

			try
			{
				patchFolder = Path.GetDirectoryName(txtOutputPatchFile.Text);
			}
			catch
			{
			}

			if (!String.IsNullOrEmpty(patchFolder))
				if (Directory.Exists(patchFolder))
					System.Diagnostics.Process.Start("explorer.exe", patchFolder);
		}

		private void butGo_Click(object sender, EventArgs e)
		{
			try
			{
				if (butGo.Text == "STOP")
				{
					KillProcesses();

					OutputMessage("Stopped.", Color.Red);
					StopTimer();

					butGo.Text = "GO!";

					return;
				}

				butGo.Text = "STOP";

				rtbConsoleOutput.Clear();

				CompileMAME();
			}
			catch (Exception ex)
			{
				OutputMessage("butGo_Click()", ex.Message, Color.Red);
			}
		}

		private void butTestDiffPatch_Click(object sender, EventArgs e)
		{
			try
			{
				rtbConsoleOutput.Clear();
				PatchDiff(PatchMode.Test);
			}
			catch (Exception ex)
			{
				OutputMessage("butTestDiffPatch_Click()", ex.Message, Color.Red);
			}
		}

		private void butReverseDiffPatch_Click(object sender, EventArgs e)
		{
			try
			{
				rtbConsoleOutput.Clear();
				PatchDiff(PatchMode.Reverse);
			}
			catch (Exception ex)
			{
				OutputMessage("butReverseDiffPatch_Click()", ex.Message, Color.Red);
			}
		}

		private void butApplyDiffPatch_Click(object sender, EventArgs e)
		{
			try
			{
				rtbConsoleOutput.Clear();
				PatchDiff(PatchMode.Apply);
			}
			catch (Exception ex)
			{
				OutputMessage("butApplyDiffPatch_Click()", ex.Message, Color.Red);
			}
		}

		private void butCreateDiffPatch_Click(object sender, EventArgs e)
		{
			try
			{
				rtbConsoleOutput.Clear();
				CreateDiff();
			}
			catch (Exception ex)
			{
				OutputMessage("butCreateDiffPatch_Click()", ex.Message, Color.Red);
			}
		}

		private void MyOutputDataHandler(object sendingProcess, DataReceivedEventArgs line)
		{
			try
			{
				if (!String.IsNullOrEmpty(line.Data))
				{
					rtbConsoleOutput.SelectionColor = Color.Lime;
					rtbConsoleOutput.AppendText(line.Data + Environment.NewLine);
				}
			}
			catch (Exception ex)
			{
				OutputMessage("MyOutputDataHandler()", ex.Message, Color.Red);
			}
		}

		private void MyPatchOutputDataHandler(object sendingProcess, DataReceivedEventArgs line)
		{
			try
			{
				if (!String.IsNullOrEmpty(line.Data))
				{
					File.AppendAllText(txtOutputPatchFile.Text, line.Data + Environment.NewLine);
				}
			}
			catch (Exception ex)
			{
				OutputMessage("MyPatchOutputDataHandler()", ex.Message, Color.Red);
			}
		}

		private void MyErrorDataHandler(object sendingProcess, DataReceivedEventArgs line)
		{
			try
			{
				if (!String.IsNullOrEmpty(line.Data))
				{
					rtbConsoleOutput.SelectionColor = Color.Red;
					rtbConsoleOutput.AppendText(line.Data + Environment.NewLine);
				}
			}
			catch (Exception ex)
			{
				OutputMessage("MyErrorDataHandler()", ex.Message, Color.Red);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
                var version = Assembly.GetExecutingAssembly().GetName().Version;

                this.Text = this.Text.Replace("[VERSION]", version.ToString(3));

				m_stopWatch = new Stopwatch();

				CreateBuildControls(pnlBuild);

				SetToolTips();

				cboOptimize.Items.AddRange(OptimizeNames);
				cboCompilerParallelJobs.Items.AddRange(CompilerParallelJobs);
				cboTargetOS.Items.AddRange(TargetOSNames);
				cboTarget.Items.AddRange(TargetNames);
				cboSubTarget.Items.AddRange(SubTargetNames);
				cboOSD.Items.AddRange(OSDNames);
				cboOptimizeLevel.Items.AddRange(OptimizeLevelNames);
				cboPrefixStrip.Items.AddRange(PrefixStripValues);

				Settings.Files.ZipExe = Path.Combine(Application.StartupPath, "7za.exe");
				Settings.Folders.MAMECompiler64 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MAME Compiler 64");

				if (!Directory.Exists(Settings.Folders.MAMECompiler64))
					Directory.CreateDirectory(Settings.Folders.MAMECompiler64);

				wbHelp.DocumentText = File.ReadAllText(Path.Combine(Application.StartupPath, "ReadMe.htm"));

				LoadSettings();

				string mc64Version = null;

				ReadMAMEFileList(out mc64Version);

				//FileIO.MoveDirectory(@"D:\buildtools\buildtools", @"D:\buildtools");
			}
			catch (Exception ex)
			{
				OutputMessage("Form1_Load()", ex.Message, Color.Red);
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				KillProcesses();
				SaveSettings();
			}
			catch (Exception ex)
			{
				OutputMessage("Form1_FormClosing()", ex.Message, Color.Red);
			}
		}

		private bool GitInit(bool waitForExit)
		{
			return RunGit("init", waitForExit);
		}

		private bool GitClone(bool waitForExit)
		{
			return RunGit("clone git@github.com:mamedev/buildtools.git", waitForExit);
		}

		private bool GitBuildToolsOrigin(bool waitForExit)
		{
			return RunGit("remote add origin git@github.com:mamedev/buildtools.git", waitForExit);
		}

		private bool GitCheckout(bool waitForExit)
		{
			return RunGit("checkout .", waitForExit);
		}

		private bool GitPull(bool waitForExit)
		{
			return RunGit("pull", waitForExit);
		}

		private bool RunGit(string args, bool waitForExit)
		{
			try
			{
#if TOOLS_MAME0168_UPDATE
				string usrBinFolder = Path.Combine(txtBuildToolsFolder.Text, @"usr\bin");
				string gitCoreFolder = Path.Combine(txtBuildToolsFolder.Text, @"usr\lib\git-core");
				string gitFileName = Path.Combine(usrBinFolder, "git.exe");
				string workingDirectory = txtBuildToolsFolder.Text;
#else
				string vendorFolder = Path.Combine(txtBuildToolsFolder.Text, "vendor");
				string msysgitBinFolder = Path.Combine(vendorFolder, @"msysgit\bin");
				string gitFileName = Path.Combine(msysgitBinFolder, "git.exe");
				string workingDirectory = txtBuildToolsFolder.Text;
#endif

				if (String.IsNullOrEmpty(gitFileName))
					return false;

				Process gitProcess = new Process();

				gitProcess.SynchronizingObject = this;
				gitProcess.EnableRaisingEvents = true;

				gitProcess.StartInfo.FileName = gitFileName;
				gitProcess.StartInfo.Arguments = args;
				gitProcess.StartInfo.ErrorDialog = false;
				gitProcess.StartInfo.UseShellExecute = false;
				gitProcess.StartInfo.CreateNoWindow = true;
				gitProcess.StartInfo.WorkingDirectory = workingDirectory;
				gitProcess.StartInfo.RedirectStandardOutput = true;
				gitProcess.StartInfo.RedirectStandardError = true;
				gitProcess.StartInfo.RedirectStandardInput = true;

				gitProcess.StartInfo.EnvironmentVariables["PATH"] = usrBinFolder;

				if (!waitForExit)
				{
					gitProcess.OutputDataReceived += new DataReceivedEventHandler(MyOutputDataHandler);
					gitProcess.ErrorDataReceived += new DataReceivedEventHandler(MyErrorDataHandler);
					gitProcess.Exited += new EventHandler(MAMEExited);
				}

				gitProcess.Start();

				OutputMessage(String.Format("Running '{0} {1}'", Path.GetFileName(gitProcess.StartInfo.FileName), gitProcess.StartInfo.Arguments), Color.Lime);

				if (waitForExit)
					gitProcess.WaitForExit();
				else
				{
					gitProcess.BeginOutputReadLine();
					gitProcess.BeginErrorReadLine();
				}

				return true;
			}
			catch (Exception ex)
			{
				OutputMessage("RunGit()", ex.Message, Color.Red);
			}

			return false;
		}

		private string GetMinGWFolder(out string minGW32Folder, out string minGW64Folder)
		{

#if TOOLS_MAME0168_UPDATE
			minGW32Folder = Path.Combine(txtBuildToolsFolder.Text, "mingw32");
			minGW64Folder = Path.Combine(txtBuildToolsFolder.Text, "mingw64");

#elif TOOLS_MAME0161_UPDATE
			string vendorFolder = Path.Combine(txtBuildToolsFolder.Text, "vendor");

			minGW32Folder = Path.Combine(vendorFolder, "mingw32");
			minGW64Folder = Path.Combine(vendorFolder, "mingw64");
			
#else
			minGW32Folder = Path.Combine(txtBuildToolsFolder.Text, "mingw64-w32");
			minGW64Folder = Path.Combine(txtBuildToolsFolder.Text, "mingw64-w64");
			
#endif
			CheckBox chk64BitTarget = null;
			bool is64BitTarget = false;

			if (TryGetBuildControl<CheckBox>("64Bit", out chk64BitTarget))
				is64BitTarget = chk64BitTarget.Checked;

			return (is64BitTarget ? minGW64Folder : minGW32Folder);
		}

		private string GetMinGWFolder()
		{
			string minGW32Folder = String.Empty;
			string minGW64Folder = String.Empty;

			return GetMinGWFolder(out minGW32Folder, out minGW64Folder);
		}

		private bool PatchDiff(PatchMode patchMode)
		{
			if (String.IsNullOrEmpty(txtDiffPatchFile.Text))
				return false;

			if (chkEOLConversion.Checked)
				EOLConversion(txtDiffPatchFile.Text);

			string actionString = String.Empty;
			string arguments = String.Format("--binary -Nru -p{0} -E -i \"{1}\"", cboPrefixStrip.SelectedIndex, txtDiffPatchFile.Text);
			string workingDirectory = txtMAMESourceFolder.Text;

			try
			{
				switch (patchMode)
				{
					case PatchMode.Test:
						actionString = "Testing";
						arguments += " --dry-run";
						break;
					case PatchMode.Reverse:
						actionString = "Reversing";
						arguments += " -R";
						break;
					case PatchMode.Apply:
						actionString = "Applying";
						break;
				}

				OutputMessage(String.Format("{0} Diff Patch...", actionString), Color.SkyBlue);

#if TOOLS_MAME0168_UPDATE
				string win32Folder = Path.Combine(txtBuildToolsFolder.Text, "win32");
				string patchFileName = Path.Combine(win32Folder, "patch.exe");
#elif TOOLS_MAME0161_UPDATE
				//CopyPatch();

				string vendorFolder = Path.Combine(txtBuildToolsFolder.Text, "vendor");
				string unixToolsFolder = Path.Combine(vendorFolder, "unixtools");
				string patchFileName = Path.Combine(unixToolsFolder, "patch.exe");
#else
				string minGWFolder = GetMinGWFolder();
				string binFolder = Path.Combine(minGWFolder, "bin");
				string patchFileName = Path.Combine(unixToolsFolder, "patch.exe");
#endif

				if (!File.Exists(patchFileName))
					CopyPatch();

				m_patchProcess = new Process();

				m_patchProcess.SynchronizingObject = this;
				m_patchProcess.EnableRaisingEvents = true;

				m_patchProcess.StartInfo.FileName = patchFileName;
				m_patchProcess.StartInfo.Arguments = arguments;
				m_patchProcess.StartInfo.ErrorDialog = false;
				m_patchProcess.StartInfo.UseShellExecute = false;
				m_patchProcess.StartInfo.CreateNoWindow = true;
				m_patchProcess.StartInfo.WorkingDirectory = workingDirectory;

				m_patchProcess.StartInfo.RedirectStandardOutput = true;
				m_patchProcess.StartInfo.RedirectStandardError = true;

				m_patchProcess.OutputDataReceived += new DataReceivedEventHandler(MyOutputDataHandler);
				m_patchProcess.ErrorDataReceived += new DataReceivedEventHandler(MyErrorDataHandler);
				m_patchProcess.Exited += new EventHandler(PatchExited);

				m_patchProcess.Start();
				StartTimer();

				m_patchProcess.BeginErrorReadLine();
				m_patchProcess.BeginOutputReadLine();
			}
			catch (Exception ex)
			{
				OutputMessage(String.Format("Error {0} Patch.", actionString), Color.Red);
				OutputMessage("PatchDiff()", ex.Message, Color.Red);
				return false;
			}

			return true;
		}

		private bool CreateDiff()
		{
			if (String.IsNullOrEmpty(txtOriginalSourceFolder.Text) || String.IsNullOrEmpty(txtModifiedSourceFolder.Text) || String.IsNullOrEmpty(txtOutputPatchFile.Text))
				return false;

			if (File.Exists(txtOutputPatchFile.Text))
				File.Delete(txtOutputPatchFile.Text);

			string workingDirectory = Path.GetDirectoryName(txtOriginalSourceFolder.Text);
			string originalSourceFolder = FileIO.GetRelativePath(workingDirectory, txtOriginalSourceFolder.Text, false);
			string modifiedSourceFolder = FileIO.GetRelativePath(workingDirectory, txtModifiedSourceFolder.Text, false);
			string arguments = String.Format("-Nru \"{0}\" \"{1}\"", originalSourceFolder, modifiedSourceFolder);

			try
			{
				OutputMessage("Creating Diff Patch...", Color.SkyBlue);

#if TOOLS_MAME0168_UPDATE
				string win32Folder = Path.Combine(txtBuildToolsFolder.Text, "win32");
				string diffFileName = Path.Combine(win32Folder, "diff.exe");
#elif TOOLS_MAME0161_UPDATE
				//CopyPatch();

				string vendorFolder = Path.Combine(txtBuildToolsFolder.Text, "vendor");
				string unixToolsFolder = Path.Combine(vendorFolder, "unixtools");
				string diffFileName = Path.Combine(unixToolsFolder, "diff.exe");
#else
				string minGWFolder = GetMinGWFolder();
				string binFolder = Path.Combine(minGWFolder, "bin");
				string diffFileName = Path.Combine(unixToolsFolder, "diff.exe");
#endif

				if (!File.Exists(diffFileName))
					CopyDiff();

				m_diffProcess = new Process();

				m_diffProcess.SynchronizingObject = this;
				m_diffProcess.EnableRaisingEvents = true;

				m_diffProcess.StartInfo.FileName = diffFileName;
				m_diffProcess.StartInfo.Arguments = arguments;
				m_diffProcess.StartInfo.ErrorDialog = false;
				m_diffProcess.StartInfo.UseShellExecute = false;
				m_diffProcess.StartInfo.CreateNoWindow = true;
				m_diffProcess.StartInfo.WorkingDirectory = workingDirectory;

				m_diffProcess.StartInfo.RedirectStandardOutput = true;
				m_diffProcess.StartInfo.RedirectStandardError = true;

				m_diffProcess.OutputDataReceived += new DataReceivedEventHandler(MyPatchOutputDataHandler);
				m_diffProcess.ErrorDataReceived += new DataReceivedEventHandler(MyErrorDataHandler);
				m_diffProcess.Exited += new EventHandler(PatchExited);

				m_diffProcess.Start();
				StartTimer();

				m_diffProcess.BeginErrorReadLine();
				m_diffProcess.BeginOutputReadLine();
			}
			catch (Exception ex)
			{
				OutputMessage("Error Creating Diff.", Color.Red);
				OutputMessage("CreateDiff()", ex.Message, Color.Red);
				return false;
			}

			return true;
		}

		private void PatchExited(object sender, EventArgs e)
		{
			try
			{
				Process process = (Process)sender;

				process.CancelOutputRead();
				process.CancelErrorRead();

				OutputMessage("Finished!", Color.SkyBlue);
				StopTimer();
				butGo.Text = "GO!";
			}
			catch (Exception ex)
			{
				OutputMessage("PatchExited()", ex.Message, Color.Red);
			}
		}

#if TOOLS_MAME0161_UPDATE

		private void DoCleanCompile()
		{
			try
			{
				if (!chkCleanCompile.Checked)
					return;

				OutputMessage("Deleting Build Folder...", Color.SkyBlue);

				string buildFolder = Path.Combine(txtMAMESourceFolder.Text, "build");

				if (Directory.Exists(buildFolder))
					Directory.Delete(buildFolder, true);
			}
			catch (Exception ex)
			{
				OutputMessage("DoCleanCompile()", ex.Message, Color.Red);
			}
			finally
			{
				chkCleanCompile.Checked = false;
			}
		}

#else

		private void DoCleanCompile()
		{
			try
			{
				if (!chkCleanCompile.Checked)
					return;

				OutputMessage("Deleting Object Folder...", Color.SkyBlue);

				string objFolder = Path.Combine(txtMAMESourceFolder.Text, "obj");

				if (Directory.Exists(objFolder))
					Directory.Delete(objFolder, true);
			}
			catch (Exception ex)
			{
				OutputMessage("DoCleanCompile()", ex.Message, Color.Red);
			}
			finally
			{
				chkCleanCompile.Checked = false;
			}
		}

#endif

		private void CompileMAME()
		{
			try
			{
				string mameParams = GetMAMEParameters();
				CheckBox chkForceDirectInput = null, chk64BitTarget = null;;
				bool forceDirectInput = false, is64BitTarget = false;

				if (TryGetBuildControl<CheckBox>("ForceDirectInput", out chkForceDirectInput))
					forceDirectInput = chkForceDirectInput.Checked;

				if (TryGetBuildControl<CheckBox>("64Bit", out chk64BitTarget))
					is64BitTarget = chk64BitTarget.Checked;

				CopyMake();
				MoveSh();
				DoCleanCompile();
				ForceDirectInput(forceDirectInput);				
				SetMakefileValues();

				OutputMessage("Compiling MAME...", Color.SkyBlue);

				if (mameParams != String.Empty)
				{
					rtbConsoleOutput.SelectionColor = Color.SkyBlue;
					rtbConsoleOutput.AppendText(String.Format("Using Parameters {0}", mameParams) + Environment.NewLine);
				}

#if TOOLS_MAME0168_UPDATE
				string win32Folder = Path.Combine(txtBuildToolsFolder.Text, "win32");
				string configBatch = Path.Combine(win32Folder, (is64BitTarget ? "config64.bat" : "config32.bat"));
				string makeFileName = Path.Combine(win32Folder, "make.exe");
#elif TOOLS_MAME0161_UPDATE
				string vendorFolder = Path.Combine(txtBuildToolsFolder.Text, "vendor");
				string minGWFolder = GetMinGWFolder();
				string binFolder = Path.Combine(minGWFolder, "bin");
				string setEnvBatch = Path.Combine(vendorFolder, "env.bat");
				string initBatch = Path.Combine(vendorFolder, "init.bat");
				string setupQtBatch = Path.Combine(vendorFolder, (chk64BitTarget.Checked ? @"qt\mingw64\setup-Qt.bat" : @"qt\mingw32\setup-Qt.bat"));
				string makeFileName = Path.Combine(binFolder, "make.exe");
#else
				string minGWFolder = GetMinGWFolder();
				string binFolder = Path.Combine(minGWFolder, "bin");
				string setEnvBatch = Path.Combine(minGWFolder, "setenv.bat");
				string setupPythonBatch = Path.Combine(minGWFolder, "setup-Python.bat");
				string setupQtBatch = Path.Combine(minGWFolder, "setup-Qt.bat");
				string makeFileName = Path.Combine(binFolder, "make.exe");
#endif

				m_mameProcess = new Process();

				m_mameProcess.SynchronizingObject = this;
				m_mameProcess.EnableRaisingEvents = true;

				m_mameProcess.StartInfo.FileName = makeFileName;
				m_mameProcess.StartInfo.Arguments = mameParams;
				m_mameProcess.StartInfo.ErrorDialog = false;
				m_mameProcess.StartInfo.UseShellExecute = false;
				m_mameProcess.StartInfo.CreateNoWindow = true;
				m_mameProcess.StartInfo.WorkingDirectory = txtMAMESourceFolder.Text;

				m_mameProcess.StartInfo.RedirectStandardOutput = true;
				m_mameProcess.StartInfo.RedirectStandardError = true;
				m_mameProcess.StartInfo.RedirectStandardInput = true;

				AddEnvironmentVariables(m_mameProcess.StartInfo.EnvironmentVariables);

#if TOOLS_MAME0168_UPDATE
				//RunBatchFile(configBatch);
#else
				RunBatchFile(setupQtBatch);
				RunBatchFile(setEnvBatch);
				//RunBatchFile(initBatch);
#endif

				m_mameProcess.OutputDataReceived += new DataReceivedEventHandler(MyOutputDataHandler);
				m_mameProcess.ErrorDataReceived += new DataReceivedEventHandler(MyErrorDataHandler);
				m_mameProcess.Exited += new EventHandler(MAMEExited);

				m_mameProcess.Start();
				StartTimer();

				m_mameProcess.BeginOutputReadLine();
				m_mameProcess.BeginErrorReadLine();
			}
			catch (Exception ex)
			{
				OutputMessage("Error Compiling MAME.", Color.Red);
				OutputMessage("CompileMAME()", ex.Message, Color.Red);
			}
		}

		private void MAMEExited(object sender, EventArgs e)
		{
			try
			{
				Process process = (Process)sender;

				process.CancelOutputRead();
				process.CancelErrorRead();

				OutputMessage("Finished!", Color.SkyBlue);
				StopTimer();

				/* if (m_mameProcess != null)
                {
					if (m_mameProcess.ExitCode == 0)
                        System.Diagnostics.Process.Start("explorer.exe", txtMAMESourceFolder.Text);
                } */

				butGo.Text = "GO!";
			}
			catch (Exception ex)
			{
				OutputMessage("MAMEExited()", ex.Message, Color.Red);
			}
		}

#if TOOLS_MAME0168_UPDATE

		private void AddEnvironmentVariables(StringDictionary environmentVariables)
		{
			System.Reflection.FieldInfo contentsField = typeof(System.Collections.Specialized.StringDictionary).GetField("contents", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			System.Collections.Hashtable environmentHash = (System.Collections.Hashtable)contentsField.GetValue(environmentVariables);

			string buildToolsFolder = txtBuildToolsFolder.Text;
			string win32Folder = Path.Combine(buildToolsFolder, "win32");
			string usrBinFolder = Path.Combine(buildToolsFolder, @"usr\bin");
			string minGW32Folder = String.Empty;
			string minGW64Folder = String.Empty;
			string minGWFolder = GetMinGWFolder(out minGW32Folder, out minGW64Folder);
			string minGW32BinFolder = Path.Combine(minGW32Folder, "bin");
			string minGW64BinFolder = Path.Combine(minGW64Folder, "bin");
			string minGWBinFolder = Path.Combine(minGWFolder, "bin");
			string pythonExe = Path.Combine(minGWBinFolder, "python.exe");
			//string conEmuFolder = Path.Combine(vendorFolder, @"conemu-maximus5\ConEmu");
			List<string> pathList = new List<string>();

			pathList.Add(win32Folder);
			pathList.Add(usrBinFolder);
			pathList.Add(minGWBinFolder);
			pathList.Add(Environment.SystemDirectory);
			pathList.Add(Environment.GetEnvironmentVariable("windir"));

			environmentHash.Add("PATH", String.Join(";", pathList.ToArray()));
			environmentHash.Add("MSYS2_ROOT", buildToolsFolder);
			environmentHash.Add("OS", "Windows_NT");
			environmentHash.Add("PYTHON_EXECUTABLE", pythonExe);
			environmentHash.Add("SHELLTYPE", "msdos");
			environmentHash.Add("MINGW32", minGW32Folder);
			environmentHash.Add("MINGW32_PATH", minGW32BinFolder);
			environmentHash.Add("MINGW64", minGW64Folder);
			environmentHash.Add("MINGW64_PATH", minGW64BinFolder);
			environmentHash.Add("MINGW", minGWFolder);
			environmentHash.Add("MINGW_PATH", minGWBinFolder);
			environmentHash.Add("CYGWIN", "nodosfilewarning");
			environmentHash.Add("TOOLCHAIN", minGWBinFolder + "/");
			environmentHash.Add("CC", "@gcc.exe");
		}

#elif TOOLS_MAME0161_UPDATE

		private void AddEnvironmentVariables(StringDictionary environmentVariables)
		{
			System.Reflection.FieldInfo contentsField = typeof(System.Collections.Specialized.StringDictionary).GetField("contents", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			System.Collections.Hashtable environmentHash = (System.Collections.Hashtable)contentsField.GetValue(environmentVariables);

			string cmderFolder = txtBuildToolsFolder.Text;
			string binFolder = Path.Combine(cmderFolder, "bin");
			string vendorFolder = Path.Combine(cmderFolder, "vendor");
			string msysgitFolder = Path.Combine(vendorFolder, "msysgit");
			string msysgitCmdFolder = Path.Combine(msysgitFolder, "cmd");
			string msysgitVimFolder = Path.Combine(msysgitFolder, @"share\vim\vim74");
			string minGW32Folder = String.Empty;
			string minGW64Folder = String.Empty;
			string minGWFolder = GetMinGWFolder(out minGW32Folder, out minGW64Folder);
			string minGW32BinFolder = Path.Combine(minGW32Folder, "bin");
			string minGW64BinFolder = Path.Combine(minGW64Folder, "bin");
			string minGWBinFolder = Path.Combine(minGWFolder, "bin");
			string minGWOptFolder = Path.Combine(minGWFolder, @"opt\bin");
			string qt32Folder = Path.Combine(vendorFolder, @"qt\mingw32\Qt");
			string qt64Folder = Path.Combine(vendorFolder, @"qt\mingw64\Qt");
			string qtFolder = (chk64BitTarget.Checked ? qt64Folder : qt32Folder);
			string qtBinFolder = Path.Combine(qtFolder, "bin");
			string qtPluginsFolder = Path.Combine(qtFolder, "plugins");
			string pythonFolder = Path.Combine(vendorFolder, "python");
			string unixTools = Path.Combine(vendorFolder, "unixtools");
			string dxSDKFolder = Path.Combine(vendorFolder, "dxsdk");
			//string conEmuFolder = Path.Combine(vendorFolder, @"conemu-maximus5\ConEmu");
			List<string> pathList = new List<string>();

			pathList.Add(binFolder);
			pathList.Add(unixTools);
			pathList.Add(msysgitCmdFolder);
			pathList.Add(msysgitVimFolder);
			pathList.Add(minGWBinFolder);
			pathList.Add(minGWOptFolder);
			pathList.Add(qtBinFolder);
			pathList.Add(pythonFolder);

			environmentHash.Add("PATH", String.Join(";", pathList.ToArray()));
			environmentHash.Add("CMDER_ROOT", cmderFolder);
			//environmentHash.Add("ConEmuDir", conEmuFolder);
			environmentHash.Add("git_install_root", msysgitFolder);
			environmentHash.Add("QT_PLUGIN_PATH", qtPluginsFolder);
			environmentHash.Add("OS", "Windows_NT");
			environmentHash.Add("PYTHON_AVAILABLE", "python");
			environmentHash.Add("SHELLTYPE", "msdos");
			environmentHash.Add("MINGW32_PATH", minGW32BinFolder);
			environmentHash.Add("MINGW32", minGW32Folder);
			environmentHash.Add("MINGW64_PATH", minGW64BinFolder);
			environmentHash.Add("MINGW64", minGW64Folder);
			environmentHash.Add("MINGW_PATH", binFolder);
			environmentHash.Add("DXSDK_DIR", dxSDKFolder);
			environmentHash.Add("CYGWIN", "nodosfilewarning");
		}
		
#else

		private void AddEnvironmentVariables(StringDictionary environmentVariables)
		{
			System.Reflection.FieldInfo contentsField = typeof(System.Collections.Specialized.StringDictionary).GetField("contents", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			System.Collections.Hashtable environmentHash = (System.Collections.Hashtable)contentsField.GetValue(environmentVariables);

			string minGWFolder = GetMinGWFolder();
			string binFolder = Path.Combine(minGWFolder, "bin");
			string optFolder = Path.Combine(minGWFolder, @"opt\bin");
			string qtFolder = Path.Combine(minGWFolder, @"Qt\bin");
			string qtPluginsFolder = Path.Combine(minGWFolder, @"Qt\plugins");
			List<string> pathList = new List<string>();

			pathList.Add(binFolder);
			pathList.Add(optFolder);
			pathList.Add(qtFolder);

			environmentHash.Add("PATH", String.Join(";", pathList.ToArray()));
			environmentHash.Add("QT_PLUGIN_PATH", qtPluginsFolder);
			environmentHash.Add("OS", "Windows_NT");
		}
		
#endif

		private bool RunBatchFile(string fileName)
		{
			if (String.IsNullOrEmpty(fileName))
				return false;

			try
			{
				if (!File.Exists(fileName))
					return false;

				Process batchProcess = new Process();

				batchProcess.SynchronizingObject = this;
				batchProcess.EnableRaisingEvents = true;

				batchProcess.StartInfo.FileName = "cmd.exe";
				batchProcess.StartInfo.Arguments = String.Format("/c \"{0}\"", fileName);
				batchProcess.StartInfo.ErrorDialog = false;
				batchProcess.StartInfo.UseShellExecute = false;
				batchProcess.StartInfo.CreateNoWindow = true;
				batchProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(fileName);

				OutputMessage(String.Format("Running '{0}'", Path.GetFileName(fileName)), Color.SkyBlue);

				batchProcess.Start();
				batchProcess.WaitForExit();

				return true;
			}
			catch (Exception ex)
			{
				OutputMessage("RunBatchFile()", ex.Message, Color.Red);
			}

			return false;
		}

		private void OutputMessage(string msg, Color color)
		{
			try
			{
				lblStatus.Text = msg;
				rtbConsoleOutput.SelectionColor = color;
				rtbConsoleOutput.AppendText(msg + Environment.NewLine);
			}
			catch (Exception ex)
			{
				OutputMessage("OutputMessage()", ex.Message, Color.Red);
			}
		}

		private void OutputMessage(string method, string msg, Color color)
		{
			try
			{
				lblStatus.Text = msg;
				rtbConsoleOutput.SelectionColor = color;
				rtbConsoleOutput.AppendText(String.Format("{0}: {1}", method, msg) + Environment.NewLine);
			}
			catch (Exception ex)
			{
				OutputMessage("OutputMessage()", ex.Message, Color.Red);
			}
		}

		private void StartTimer()
		{
			try
			{
				m_stopWatch.Reset();
				m_stopWatch.Start();

				m_compileTimer = new System.Windows.Forms.Timer();
				m_compileTimer.Interval = 1000;
				m_compileTimer.Tick += new EventHandler(timer_Tick);
				m_compileTimer.Start();
			}
			catch (Exception ex)
			{
				OutputMessage("StartTimer()", ex.Message, Color.Red);
			}
		}

		private void StopTimer()
		{
			try
			{
				m_stopWatch.Stop();
				TimeSpan time = m_stopWatch.Elapsed;

				if (m_compileTimer != null)
				{
					m_compileTimer.Stop();
					m_compileTimer.Dispose();
					m_compileTimer = null;

					rtbConsoleOutput.SelectionColor = Color.Yellow;
					rtbConsoleOutput.AppendText(String.Format("{0} Hours {1} Minutes and {2} Seconds Elapsed.", time.Hours, time.Minutes, time.Seconds) + Environment.NewLine);
				}
			}
			catch (Exception ex)
			{
				OutputMessage("StopTimer()", ex.Message, Color.Red);
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			try
			{
				TimeSpan timeSpan = m_stopWatch.Elapsed;
				DateTime dateTime = new DateTime(timeSpan.Ticks);
				lblElapsed.Text = dateTime.ToString("HH:mm:ss");
			}
			catch (Exception ex)
			{
				OutputMessage("timer_Tick()", ex.Message, Color.Red);
			}
		}

		private void KillProcesses()
		{
			try
			{
				if (m_mameProcess != null)
				{
					m_mameProcess.CancelOutputRead();
					m_mameProcess.CancelErrorRead();

					if (!m_mameProcess.HasExited)
						m_mameProcess.Kill();
				}
			}
			catch (Exception ex)
			{
				OutputMessage("KillProcesses()", ex.Message, Color.Red);
			}
			finally
			{
				if (m_mameProcess != null)
				{
					m_mameProcess.Close();
					m_mameProcess.Dispose();
					m_mameProcess = null;
				}
			}
			try
			{
				if (m_patchProcess != null)
				{
					m_patchProcess.CancelOutputRead();
					m_patchProcess.CancelErrorRead();

					if (!m_patchProcess.HasExited)
						m_patchProcess.Kill();
				}
			}
			catch (Exception ex)
			{
				OutputMessage("KillProcesses()", ex.Message, Color.Red);
			}
			finally
			{
				if (m_patchProcess != null)
				{
					m_patchProcess.Close();
					m_patchProcess.Dispose();
					m_patchProcess = null;
				}
			}
		}

		private void rtbConsoleOutput_TextChanged(object sender, EventArgs e)
		{
			try
			{
				rtbConsoleOutput.SelectionLength = 0;
				rtbConsoleOutput.SelectionStart = rtbConsoleOutput.Text.Length;
				rtbConsoleOutput.ScrollToCaret();
			}
			catch (Exception ex)
			{
				OutputMessage("rtbCompileOutput_TextChanged()", ex.Message, Color.Red);
			}
		}

		private void picBanner_Click(object sender, EventArgs e)
		{
			Process.Start("http://headsoft.com.au/redirect.php?url=https://www.gameex.com/evolution/");
		}

		private void copySelectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(rtbConsoleOutput.SelectedText))
				return;

			Clipboard.SetText(rtbConsoleOutput.SelectedText);
		}

		private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(rtbConsoleOutput.Text))
				return;

			Clipboard.SetText(rtbConsoleOutput.Text);
		}

		private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			rtbConsoleOutput.Text = String.Empty;
		}

		private void copyLinkStripMenuItem_Click(object sender, EventArgs e)
		{
			if (lvwDownloads.SelectedItems.Count == 0)
				return;

			string url = lvwDownloads.SelectedItems[0].SubItems[3].Text;

			Clipboard.SetText(url);
		}

		private bool Is64BitMode()
		{
			return Marshal.SizeOf(typeof(IntPtr)) == 8;
		}

		private T Clamp<T>(T val, T min, T max) where T : IComparable<T>
		{
			if (val.CompareTo(min) < 0) return min;
			else if (val.CompareTo(max) > 0) return max;
			else return val;
		}

		private void ReadMAMEFileList(out string mc64Version)
		{
			string mameFileListFileName = Path.GetFileName(Settings.Urls.MAMEFileListTxt);
			string tempFolder = Globals.GetTempFolder();
			string mameFileListPath = Path.Combine(tempFolder, mameFileListFileName);

			mc64Version = null;

			if (!File.Exists(mameFileListPath))
				return;

			lvwDownloads.Items.Clear();

			IniFile iniFile = new IniFile(mameFileListPath);

			mc64Version = iniFile.Read("General", "MC64Version");

			for (int i = 0; i < Globals.MaxDownloads; i++)
			{
				string sectionName = String.Format("Download{0}", i + 1);

				string name = iniFile.Read(sectionName, "Name");
				string fileName = iniFile.Read(sectionName, "FileName");
				string description = iniFile.Read(sectionName, "Description");
				string type = iniFile.Read(sectionName, "Type");
				string folder = iniFile.Read(sectionName, "Folder");
				string url = iniFile.Read(sectionName, "Url");

				if (name == null)
					continue;

				string[] itemArray = { name, description, type, url };
				ListViewItem listViewItem = new ListViewItem(itemArray);

				DownloadFileNode downloadFileNode = new DownloadFileNode(url, String.Empty);
				downloadFileNode.Name = name;
				downloadFileNode.Description = description;
				downloadFileNode.Category = type;
				downloadFileNode.Folder = folder;
				downloadFileNode.Tag = listViewItem;

				if (!String.IsNullOrEmpty(fileName))
					downloadFileNode.FileName = fileName;

				listViewItem.Tag = downloadFileNode;

				lvwDownloads.Items.Add(listViewItem);
			}

			for (int i = 0; i < lvwDownloads.Columns.Count; i++)
				lvwDownloads.Columns[i].Width = -2;
		}

		private void FixToolsFolder(string extractPath)
		{
			try
			{
				string toolsFolder = Path.Combine(extractPath, "buildtools");

				if (Directory.Exists(toolsFolder))
					FileIO.MoveDirectory(toolsFolder, extractPath);
			}
			catch (Exception ex)
			{
				OutputMessage("FixToolsFolder()", ex.Message, Color.Red);
			}
		}

		private void DownloadMAMEFileList()
		{
			string tempFolder = Globals.GetTempFolder();
			DownloadFileNode downloadFileNode = new DownloadFileNode(Settings.Urls.MAMEFileListTxt, tempFolder);
			List<DownloadFileNode> fileList = new List<DownloadFileNode>();

			fileList.Add(downloadFileNode);

			using (frmDownload frmDownload = new frmDownload(fileList, true, true))
			{
				frmDownload.OutputDataReceived += MyOutputDataHandler;
				frmDownload.ErrorDataReceived += MyErrorDataHandler;

				if (frmDownload.ShowDialog(this) == DialogResult.OK)
				{
					string mc64Version = null;

					ReadMAMEFileList(out mc64Version);

					if (mc64Version != null)
					{
                        var version = Assembly.GetExecutingAssembly().GetName().Version;

                        if (!mc64Version.Equals(version.ToString(3)))
						{
							if (MessageBox.Show(this, "Do you want to update MAME Compiler 64?", "Update MC64", MessageBoxButtons.YesNo) == DialogResult.Yes)
								UpdateMC64();
						}
					}
				}
			}
		}

#if TOOLS_MAME0161_UPDATE

		private void FixMAMEUIFolder(string extractPath)
		{
			try
			{
				string mameUIsFolder = Path.Combine(extractPath, "MAMEUIs");
				string mameUIsScriptsFolder = Path.Combine(mameUIsFolder, "scripts");
				string mameUIsSrcFolder = Path.Combine(mameUIsFolder, @"src\src");
				string mameUIsOsdFolder = Path.Combine(mameUIsFolder, @"src\osd");
				string mameScriptsFolder = Path.Combine(extractPath, "scripts");
				string mameSrcFolder = Path.Combine(extractPath, "src");
				string mameOsdFolder = Path.Combine(mameSrcFolder, "osd");

				if (Directory.Exists(mameUIsScriptsFolder))
					FileIO.CopyDirectory(mameUIsScriptsFolder, mameScriptsFolder);

				if (Directory.Exists(mameUIsSrcFolder))
					FileIO.CopyDirectory(mameUIsSrcFolder, mameSrcFolder);

				if (Directory.Exists(mameUIsOsdFolder))
					FileIO.CopyDirectory(mameUIsOsdFolder, mameOsdFolder);

				if (Directory.Exists(mameUIsFolder))
					FileIO.DeleteAll(mameUIsFolder);
			}
			catch (Exception ex)
			{
				OutputMessage("FixMAMEUIFolderNew()", ex.Message, Color.Red);
			}
		}

#else

		private void FixMAMEUIFolder(string extractPath)
		{
			try
			{
				string mameUIsFolder = Path.Combine(extractPath, "MAMEUIs");

				if (Directory.Exists(mameUIsFolder))
				{
					FileIO.CopyAll(mameUIsFolder, extractPath);

					Directory.Delete(mameUIsFolder, true);
				}
			}
			catch (Exception ex)
			{
				OutputMessage("FixMAMEUIFolder()", ex.Message, Color.Red);
			}
		}
		
#endif

		private void butDownloadFileList_Click(object sender, EventArgs e)
		{
			DownloadMAMEFileList();
		}

		private void butUpdateCompileTools_Click(object sender, EventArgs e)
		{
			//GitInit(true);
			//GitBuildToolsOrigin(true);
			GitCheckout(true);
			GitPull(false);
		}

		private void butUpdateMAMECompiler_Click(object sender, EventArgs e)
		{
			UpdateMC64();
		}

		private void butDownloadSelected_Click(object sender, EventArgs e)
		{
			List<DownloadFileNode> fileList = new List<DownloadFileNode>();
			string tempFolder = Globals.GetTempFolder();
			string unZipFolder = Path.Combine(tempFolder, FileIO.GetRandomFolderName());

			foreach (ListViewItem listViewItem in lvwDownloads.Items)
			{
				if (!listViewItem.Checked)
					continue;

				DownloadFileNode downloadFileNode = (DownloadFileNode)listViewItem.Tag;

				//downloadFileNode.SourceUrl = "http://headsoft.com.au/download/mame/Test.exe"
				//downloadFileNode.SourceUrl = "http://headsoft.com.au/download/mame/buildtools.7z";

				switch (downloadFileNode.Category)
				{
					case "Source":
						downloadFileNode.DestinationFolder = tempFolder;
						downloadFileNode.ExtractionFolder = unZipFolder;
						break;
					case "Patch":
						downloadFileNode.DestinationFolder = txtPatchFolder.Text;
						downloadFileNode.ExtractionFolder = txtPatchFolder.Text;
						break;
					case "Tools":
						downloadFileNode.DestinationFolder = tempFolder;
						downloadFileNode.ExtractionFolder = unZipFolder;
						break;
				}

				if (downloadFileNode.NeedsPostProcessing)
					downloadFileNode.DestinationFolder = tempFolder;

				fileList.Add(downloadFileNode);
			}

			if (fileList.Count == 0)
				return;

			using (frmDownload frmDownload = new frmDownload(fileList, true, true))
			{
				frmDownload.DownloadStatusChanged += OnDownloadStatusChanged;
				frmDownload.FileDownloadSucceeded += OnFileDownloadSucceeded;
				frmDownload.PostProcessSucceeded += OnPostProcessSucceeded;
				frmDownload.OutputDataReceived += MyOutputDataHandler;
				frmDownload.ErrorDataReceived += MyErrorDataHandler;

				if (frmDownload.ShowDialog(this) == DialogResult.OK)
				{
				}
			}
		}

		private void OnDownloadStatusChanged(object sender, DownloadStatusChangedEventArgs e)
		{
			if (e.Message.Equals("Idle."))
				return;

			OutputMessage(e.Message, e.IsError ? Color.Red : Color.Lime);
		}

		private void OnFileDownloadSucceeded(object sender, FileDownloadEventArgs e)
		{
			DownloadFileNode downloadFileNode = e.DownloadFile;

			if (downloadFileNode.Tag != null)
			{
				ListViewItem listViewItem = (ListViewItem)downloadFileNode.Tag;
				listViewItem.Checked = false;
			}
		}

		private void OnPostProcessSucceeded(object sender, FileDownloadEventArgs e)
		{
			frmDownload frmDownload = (frmDownload)sender;
			DownloadFileNode downloadFileNode = e.DownloadFile;

			switch (downloadFileNode.Category)
			{
				case "Source":
					{
						string extractionFolder = Path.Combine(txtSourceFolder.Text, downloadFileNode.Folder);

						txtMAMESourceFolder.Text = extractionFolder;

						string fileName = null;

						if (FileIO.TryFindFile(downloadFileNode.ExtractionFolder, "makefile", out fileName, true))
						{
							string sourceFolder = Path.GetDirectoryName(fileName);

							CopyDirectory(frmDownload, sourceFolder, extractionFolder, downloadFileNode.ExtractionFolder);
						}
						break;
					}
				case "Patch":
					break;
				case "Tools":
					{
						string extractionFolder = txtBuildToolsFolder.Text;

						string fileName = null;

						if (FileIO.TryFindFile(downloadFileNode.ExtractionFolder, "mingw32.exe", out fileName, true))
						{
							string sourceFolder = Path.GetDirectoryName(fileName);

							CopyDirectory(frmDownload, sourceFolder, extractionFolder, downloadFileNode.ExtractionFolder);
							
							txtSourceFolder.Text = Path.Combine(extractionFolder, "src");
							txtPatchFolder.Text = Path.Combine(extractionFolder, "patch");
						}
						break;
					}
				case "Update":
					frmDownload.Close();

					Application.Exit();
					break;
			}
		}

		private void CopyDirectory(Control control, string sourceFolder, string extractionFolder, string downloadFolder)
		{	
			frmProcessing frmProcessing = new frmProcessing();

			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.WorkerReportsProgress = true;
			backgroundWorker.ProgressChanged += new ProgressChangedEventHandler((sender, e) =>
			{
				frmProcessing.UpdateProgressBar(e.ProgressPercentage, (string)e.UserState);
			});

			backgroundWorker.DoWork += new DoWorkEventHandler((sender, e) =>
			{
				backgroundWorker.ReportProgress(25, "Copying files...");

				FileIO.CopyDirectory(sourceFolder, extractionFolder);

				backgroundWorker.ReportProgress(50, "Cleaning up...");

				FileIO.DeleteAll(downloadFolder);

				backgroundWorker.ReportProgress(100, "Done.");
			});

			backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender, e) =>
			{
				backgroundWorker.Dispose();

				frmProcessing.Close();
			});

			backgroundWorker.RunWorkerAsync();

			frmProcessing.ShowDialog(control);
		}

		private void UpdateMC64()
		{
			string tempFolder = Globals.GetTempFolder();
			string sourceUrl = Settings.Urls.MC64SetupExe;
			List<DownloadFileNode> fileList = new List<DownloadFileNode>();

			DownloadFileNode downloadFileNode = new DownloadFileNode(Settings.Urls.MC64SetupExe, Globals.GetTempFolder());

			downloadFileNode.Category = "Update";

			fileList.Add(downloadFileNode);

			using (frmDownload frmDownload = new frmDownload(fileList, true, true))
			{
				frmDownload.DownloadStatusChanged += OnDownloadStatusChanged;
				frmDownload.FileDownloadSucceeded += OnFileDownloadSucceeded;
				frmDownload.PostProcessSucceeded += OnPostProcessSucceeded;
				frmDownload.OutputDataReceived += MyOutputDataHandler;
				frmDownload.ErrorDataReceived += MyErrorDataHandler;

				if (frmDownload.ShowDialog(this) == DialogResult.OK)
				{
				}
			}
		}

		private void wbHelp_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			this.wbHelp.Document.Body.Style = "font-family: tahoma;";
		}

		private void wbHelp_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.ToString().Equals("about:blank", StringComparison.InvariantCultureIgnoreCase))
				return;

			e.Cancel = true;
			System.Diagnostics.Process.Start(e.Url.ToString());
		}

		private void tsslHeadsoftLogo_Click(object sender, EventArgs e)
		{
			Process.Start("http://headsoft.com.au/");
		}
	}

	public class BuildOptionNode
	{
		public string Name;
		public string IniSection;
		public string IniName;
		public object IniDefault;
		public string Type;
		public string Constant;
		public string ToolTip;
		public Control Control;

		public BuildOptionNode(string name, string iniSection, string iniName, object iniDefault, string type, string constant, string toolTip)
		{
			Name = name;
			IniSection = iniSection;
			IniName = iniName;
			IniDefault = iniDefault;
			Type = type;
			Constant = constant;
			ToolTip = toolTip;
		}
	}
}
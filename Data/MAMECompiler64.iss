; -- MAMECompiler64.iss --

[Setup]
AppName=MAME Compiler 64
AppVerName=MAME Compiler 64 {#AppVersion}
DefaultDirName={autopf}\MAME Compiler 64
DefaultGroupName=MAME Compiler 64
UninstallDisplayIcon={app}\MAMECompiler64.exe
Compression=lzma
SolidCompression=yes
OutputDir=Setup
OutputBaseFilename=MAMECompiler64Setup
WizardImageFile=WizardImage.bmp
WizardSmallImageFile=WizardSmallImage.bmp

[Files]
Source: "MAMECompiler64.exe"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "MAMECompiler64.dll"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "MAMECompiler64.runtimeconfig.json"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "make.exe"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "patch.exe"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "diff.exe"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "libiconv2.dll"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "libintl3.dll"; DestDir: "{app}"; CopyMode: alwaysoverwrite
Source: "7za.exe"; DestDir: "{app}"; CopyMode: alwaysoverwrite
;Source: "MinGW\*"; DestDir: "{sd}\MinGW"; CopyMode: alwaysoverwrite; Flags: recursesubdirs createallsubdirs
Source: "ReadMe.htm"; DestDir: "{app}"; CopyMode: alwaysoverwrite

[UninstallDelete]
Type: files; Name: "{app}\MAMECompiler64.exe"
Type: files; Name: "{app}\MAMECompiler64.dll"
Type: files; Name: "{app}\MAMECompiler64.runtimeconfig.json"
Type: files; Name: "{app}\MAMECompiler64.ini"
Type: files; Name: "{app}\make.exe"
Type: files; Name: "{app}\patch.exe"
Type: files; Name: "{app}\diff.exe"
Type: files; Name: "{app}\libiconv2.dll"
Type: files; Name: "{app}\libintl3.dll"
Type: files; Name: "{app}\7za.exe"
;Type: filesandordirs;  Name: "{sd}\MinGW"

[Registry]
;Root: HKCU; Subkey: "Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"; ValueType: string; ValueName: "{app}\MAMECompiler64.exe"; ValueData: RUNASADMIN; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"; ValueType: none; ValueName: "{app}\MAMECompiler64.exe"; Flags: deletekey

[Icons]
Name: "{group}\MAMECompiler64"; Filename: "{app}\MAMECompiler64.exe"
Name: "{group}\Uninstall MAMECompiler64"; Filename: "{app}\unins000.exe"

[Run]
Filename: "{app}\ReadMe.htm"; Description: "View ReadMe"; Flags: shellexec skipifdoesntexist postinstall skipifsilent unchecked
Filename: "{app}\MAMECompiler64.exe"; Description: "Launch MAME Compiler 64"; Flags: postinstall shellexec nowait
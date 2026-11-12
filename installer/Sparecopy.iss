; Inno Setup script for Sparecopy
; Build: run ..\build.ps1, or compile this file with Inno Setup 6 (ISCC.exe) after publishing.
;
; Installs per-user by default (no administrator prompt). The first page lets people choose
; "all users" instead, which installs under Program Files.
;
; The Bytehawk Studio Launcher can run this setup silently to install and update Sparecopy:
;   Sparecopy-<version>-Setup.exe /VERYSILENT /SUPPRESSMSGBOXES /NORESTART /SP- /NOICONS /DIR="<folder>"

#define MyAppName "Sparecopy"
#ifndef MyAppVersion
  #define MyAppVersion "0.1.0"
#endif
#define MyAppPublisher "Bytehawk Studio"
#define MyAppExeName "Sparecopy.exe"
#define MyAppUrl "https://github.com/adamglassner/sparecopy"
#ifndef PublishDir
  #define PublishDir "..\dist\publish"
#endif

[Setup]
AppId={{5E2B9C14-7A3F-4E81-B6D0-2C9F1A4E7B35}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppUrl}
AppSupportURL={#MyAppUrl}/issues
AppUpdatesURL={#MyAppUrl}/releases
AppCopyright=(c) 2026 Bytehawk Studio. Free and open source (MIT)
VersionInfoVersion={#MyAppVersion}.0
VersionInfoCompany={#MyAppPublisher}
VersionInfoDescription={#MyAppName} Setup
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion={#MyAppVersion}
DefaultDirName={autopf}\Sparecopy
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
MinVersion=10.0
OutputDir=..\dist
OutputBaseFilename=Sparecopy-{#MyAppVersion}-Setup
SetupIconFile=..\src\Sparecopy.Desktop\Assets\sparecopy.ico
UninstallDisplayIcon={app}\{#MyAppExeName}
UninstallDisplayName={#MyAppName}
LicenseFile=..\LICENSE
WizardStyle=modern
WizardImageFile=wizard-large-1.bmp,wizard-large-2.bmp
WizardSmallImageFile=wizard-small-55.bmp,wizard-small-110.bmp
Compression=lzma2/max
SolidCompression=yes
; Ask to close Sparecopy if it's running (Windows Restart Manager)
CloseApplications=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
Source: "{#PublishDir}\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LICENSE"; DestDir: "{app}"; DestName: "LICENSE.txt"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Uninstall {#MyAppName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

; NOTE: There is deliberately no [UninstallDelete] section.
; Uninstalling removes the program and its shortcuts only. Settings in %APPDATA%\Sparecopy\ are kept.

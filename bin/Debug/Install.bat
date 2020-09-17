%~dp0InstallUtil.exe %~dp0JcService.exe
Net Start aJcService
sc config aJcService start= auto
pause
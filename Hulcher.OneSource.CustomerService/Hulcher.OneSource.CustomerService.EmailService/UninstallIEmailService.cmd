@ECHO OFF

REM The following directory is for .NET 4
set DOTNETFX4=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX4%

echo Uninstalling Hulcher.OneSource.CustomerService.EmailService...
echo ---------------------------------------------------
InstallUtil /u Hulcher.OneSource.CustomerService.EmailService.exe 
echo ---------------------------------------------------
echo Done.
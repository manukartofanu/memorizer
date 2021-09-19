@echo off

set DEVENV_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv"
set BAT_PATH=%~dp0
set SOLUTION_PATH=%~dp0\wpf
set RELEASE_BUILD_FOLDER=%~dp0\Release
set RELEASE_SQUIRREL_FOLDER=Manurizer-%VersionNumber%-squirrel

rem Build Project
call %DEVENV_PATH% %SOLUTION_PATH%\Main.sln /rebuild Release

rem Get Current Version
call sigcheck -n %RELEASE_BUILD_FOLDER%\Manurizer.exe >version.info
set /p VersionNumber= < version.info
del version.info
echo Release Version: %VersionNumber%

rem Create Release Squirrel Files (Temp) Folder
if exist %RELEASE_SQUIRREL_FOLDER% rmdir /q /s %RELEASE_SQUIRREL_FOLDER%
mkdir %RELEASE_SQUIRREL_FOLDER%

rem Create Release Squirrel Files Folder Structure
mkdir %RELEASE_SQUIRREL_FOLDER%\lib
mkdir %RELEASE_SQUIRREL_FOLDER%\lib\net45
xcopy /y %RELEASE_BUILD_FOLDER%\Manurizer.exe %RELEASE_SQUIRREL_FOLDER%\lib\net45
xcopy /y %RELEASE_BUILD_FOLDER%\*.dll %RELEASE_SQUIRREL_FOLDER%\lib\net45
xcopy /y %RELEASE_BUILD_FOLDER%\*.db %RELEASE_SQUIRREL_FOLDER%\lib\net45
xcopy /y %RELEASE_BUILD_FOLDER%\*.config %RELEASE_SQUIRREL_FOLDER%\lib\net45
echo d | xcopy /y %RELEASE_BUILD_FOLDER%\x86 %RELEASE_SQUIRREL_FOLDER%\lib\net45\x86
echo d | xcopy /y %RELEASE_BUILD_FOLDER%\x64 %RELEASE_SQUIRREL_FOLDER%\lib\net45\x64

rem Copy NuGet Spec File
copy release.nuspec %RELEASE_SQUIRREL_FOLDER%

rem Set Version in NuGet Spec File
changeversion.exe %RELEASE_SQUIRREL_FOLDER%\release.nuspec %VersionNumber%

rem Create NuGet Package
cd %RELEASE_SQUIRREL_FOLDER%
nuget pack release.nuspec
cd %BAT_PATH%

rem Create Squirrel Exe
wpf\packages\squirrel.windows.1.9.1\tools\Squirrel.exe --releasify %RELEASE_SQUIRREL_FOLDER%\Manurizer.%VersionNumber%.nupkg --setupIcon wpf\src\Manurizer\icon.ico
if exist Releases\Manurizer.exe (
	del Releases\Manurizer.exe
)
rename Releases\Setup.exe Manurizer.exe

rem Remove Temp Files and Folder
rmdir %RELEASE_BUILD_FOLDER% /s /q
rmdir %RELEASE_SQUIRREL_FOLDER% /s /q
if exist Update.exe (
	del Update.exe
)
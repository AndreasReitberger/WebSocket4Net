@echo off

rem set msbuild="%ProgramFiles(x86)%\MSBuild\15.0\Bin\msbuild.exe"

set msbuild="%ProgramFiles%\Microsoft Visual Studio\2022\Preview\MSBuild\Current\Bin\msbuild.exe"
%msbuild% WebSocket4Net.build /t:Build

pause
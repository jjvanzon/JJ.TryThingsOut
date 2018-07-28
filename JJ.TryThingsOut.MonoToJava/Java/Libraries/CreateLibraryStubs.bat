@echo off
prompt $G

echo on
rem  This bat file creates .jar files that can be used as libraries in Java / Eclipse.
rem  The .jar files are stubs that can interoperate with .NET assemblies.
rem  Some .jar stubs for basic .NET assemblies are generated.
rem  And .jar stubs for assemblies for the application itself are created.
rem  Be sure to first build the Visual Studio projects.

pause

del *.jar

"..\..\External Components\ikvm-7.2.4630.5\bin\ikvmstub.exe" "..\..\External Components\Mono-3.2.3\lib\mono\2.0\mscorlib.dll"
"..\..\External Components\ikvm-7.2.4630.5\bin\ikvmstub.exe" "..\..\External Components\Mono-3.2.3\lib\mono\gtk-sharp-2.0\gtk-sharp.dll" -r:"..\..\External Components\Mono-3.2.3\lib\mono\2.0\Mono.Cairo.dll"
"..\..\External Components\ikvm-7.2.4630.5\bin\ikvmstub.exe" "..\..\External Components\Mono-3.2.3\lib\mono\gtk-sharp-2.0\glib-sharp.dll"
"..\..\External Components\ikvm-7.2.4630.5\bin\ikvmstub.exe" "..\..\External Components\Mono-3.2.3\lib\mono\gtk-sharp-2.0\atk-sharp.dll"
"..\..\External Components\ikvm-7.2.4630.5\bin\ikvmstub.exe" "..\..\DotNet\ClassLibrary1\ClassLibrary1\bin\Debug\ClassLibrary1.dll"

pause

rem Current directory is meant to be Visual Studio's ProjectDir variable.
set ProjectDir=%CD%
echo CURRENT DIRECTORY: %ProjectDir%

rem First parameter may be Visual Studio's OutDir variable, a relative path like "bin\Debug\".
set OutDir=%1

echo COPY HELPER FILES TO BIN (SUB)-FOLDER %OutDir%
rem (Giving project files the properties Build Action = Content and Copy to Output Directory = Always did not seem copy them to the bin (sub-)folder.)
copy CreateEmbedLangTransform.cmd %OutDir%
copy CreateLocalizedInstallerAllLanguages.cmd %OutDir%
copy MsiTran.exe %OutDir%
copy wilangid.vbs %OutDir%
copy wisubstg.vbs %OutDir%

cd %OutDir%
call CreateLocalizedInstallerAllLanguages.cmd
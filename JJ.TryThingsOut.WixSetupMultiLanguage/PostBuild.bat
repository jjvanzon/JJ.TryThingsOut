rem Current directory is meant to be Visual Studio's ProjectDir variable.
rem echo set ProjectDir=%CD%
rem set ProjectDir=%CD%

rem First parameter may be Visual Studio's OutDir variable, a relative path like "bin\Debug\".
echo "set OutDir=%1
set OutDir=%1

rem COPYING HELPER FILES
rem (Giving project files the properties Build Action = Content and Copy to Output Directory = Always did not seem copy them to the bin (sub-)folder.)
echo "copy HelperFiles\CreateEmbedLangTransform.cmd %OutDir%"
copy HelperFiles\CreateEmbedLangTransform.cmd %OutDir%

echo "copy HelperFiles\CreateLocalizedInstallerAllLanguages.cmd %OutDir%"
copy HelperFiles\CreateLocalizedInstallerAllLanguages.cmd %OutDir%

echo "copy HelperFiles\MsiTran.exe %OutDir%"
copy HelperFiles\MsiTran.exe %OutDir%

echo "copy HelperFiles\wilangid.vbs %OutDir%"
copy HelperFiles\wilangid.vbs %OutDir%

echo "copy HelperFiles\wisubstg.vbs %OutDir%"
copy HelperFiles\wisubstg.vbs %OutDir%

echo "cd %OutDir%"
cd %OutDir%

rem CALLING HELPER COMMAND

echo "call CreateLocalizedInstallerAllLanguages.cmd"
call CreateLocalizedInstallerAllLanguages.cmd

rem DELETING HELPER FILES

echo "del CreateEmbedLangTransform.cmd"
del CreateEmbedLangTransform.cmd

echo "del CreateLocalizedInstallerAllLanguages.cmd"
del CreateLocalizedInstallerAllLanguages.cmd

echo "del MsiTran.exe"
del MsiTran.exe

echo "del wilangid.vbs"
del wilangid.vbs

echo "del wisubstg.vbs"
del wisubstg.vbs

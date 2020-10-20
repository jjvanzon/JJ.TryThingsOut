rem Current directory is meant to be Visual Studio's ProjectDir variable.
rem echo set ProjectDir=%CD%
rem set ProjectDir=%CD%

rem First parameter may be Visual Studio's OutDir variable, a relative path like "bin\Debug\".
echo "set OutDir=%1
set OutDir=%1

rem COPYING HELPER FILES
rem (Giving project files the properties Build Action = Content and Copy to Output Directory = Always did not seem copy them to the bin (sub-)folder.)
echo "copy HelperFiles\AddSingleLanguageTransform.cmd %OutDir%"
copy HelperFiles\AddSingleLanguageTransform.cmd %OutDir%

echo "copy HelperFiles\AddAllLanguageTransforms.cmd %OutDir%"
copy HelperFiles\AddAllLanguageTransforms.cmd %OutDir%

echo "copy HelperFiles\MsiTran.exe %OutDir%"
copy HelperFiles\MsiTran.exe %OutDir%

echo "copy HelperFiles\wilangid.vbs %OutDir%"
copy HelperFiles\wilangid.vbs %OutDir%

echo "copy HelperFiles\wisubstg.vbs %OutDir%"
copy HelperFiles\wisubstg.vbs %OutDir%

echo "cd %OutDir%"
cd %OutDir%

rem CALLING HELPER COMMAND

echo "call AddAllLanguageTransforms.cmd"
call AddAllLanguageTransforms.cmd

rem DELETING HELPER FILES

echo "del AddSingleLanguageTransform.cmd"
del AddSingleLanguageTransform.cmd

echo "del AddAllLanguageTransforms.cmd"
del AddAllLanguageTransforms.cmd

echo "del MsiTran.exe"
del MsiTran.exe

echo "del wilangid.vbs"
del wilangid.vbs

echo "del wisubstg.vbs"
del wisubstg.vbs

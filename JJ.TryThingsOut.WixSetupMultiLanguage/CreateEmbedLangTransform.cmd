rem Current directory is meant to be Visual Studio's OutDir variable (like "bin\Debug\").

echo "set MsiFileName=%1"
set MsiFileName=%1

echo "set LangString=%2"
set LangString=%2

echo "set LangNumber=%3"
set LangNumber=%3

rem Modify the MSI so it contains a different ProductLanguage using WiLangId.vbs
rem echo "cscript WiLangId.vbs %LangString%\%MsiFileName% Product %LangNumber%
rem cscript WiLangId.vbs %LangString%\%MsiFileName% Product %LangNumber% 

rem Create a transform that captures the difference between the two MSIs using MSITran.exe
echo "MsiTran.exe -g %MsiFileName% %LangString%\%MsiFileName% %LangString%\Mst.mst
MsiTran.exe -g %MsiFileName% %LangString%\%MsiFileName% %LangString%\Mst.mst

rem Embed the transform in the final master installer using WiSubStg.vbs
echo "cscript wisubstg.vbs %MsiFileName% %LangString%\Mst.mst %LangNumber%
cscript wisubstg.vbs %MsiFileName% %LangString%\Mst.mst %LangNumber%

rem Not sure why this should be called. Reports which languages are in the install?
echo "cscript wisubstg.vbs %MsiFileName%
cscript wisubstg.vbs %MsiFileName% 

rem Deleting intermediate (transform/.mst) file
echo "del %LangString%\Mst.mst"
del %LangString%\Mst.mst
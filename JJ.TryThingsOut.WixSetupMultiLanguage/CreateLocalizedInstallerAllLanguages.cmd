rem Current directory is meant to be Visual Studio's OutDir variable (like "bin\Debug\").

rem "echo if not exist FinalMasterInstaller md FinalMasterInstaller"
rem if not exist FinalMasterInstaller md FinalMasterInstaller

echo "copy en-us\MyInstaller.msi MyInstaller.msi"
copy en-us\MyInstaller.msi MyInstaller.msi

echo "call CreateEmbedLangTransform.cmd MyInstaller nl_nl 1043"
call CreateEmbedLangTransform.cmd MyInstaller nl_nl 1043

rem call CreateEmbedLangTransform.cmd MyInstaller neutral 0
rem call CreateEmbedLangTransform.cmd MyInstaller nl 19
rem call CreateEmbedLangTransform.cmd MyInstaller en_us 1033
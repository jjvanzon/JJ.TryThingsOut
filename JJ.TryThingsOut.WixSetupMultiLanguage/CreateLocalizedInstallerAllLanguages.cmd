rem Current directory is meant to be Visual Studio's OutDir variable (like "bin\Debug\").
set OutDir=%CD%
echo CURRENT DIRECTORY: %OutDir%

if not exist FinalMasterInstaller md FinalMasterInstaller
copy MyInstaller.msi FinalMasterInstaller
call CreateEmbedLangTransform.cmd MyInstaller neutral 0
call CreateEmbedLangTransform.cmd MyInstaller nl_nl 1043
call CreateEmbedLangTransform.cmd MyInstaller nl 19
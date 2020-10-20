rem Current directory is meant to be Visual Studio's OutDir variable (like "bin\Debug\").

rem Copy en_us => base installer.
echo "copy en-us\MyInstaller.msi MyInstaller.msi"
copy en-us\MyInstaller.msi MyInstaller.msi

echo "call CreateEmbedLangTransform.cmd MyInstaller.msi nl-nl 1043"
call CreateEmbedLangTransform.cmd MyInstaller.msi nl-nl 1043

rem echo "call CreateEmbedLangTransform.cmd MyInstaller.msi nl 19"
rem call CreateEmbedLangTransform.cmd MyInstaller.msi nl 19

rem call CreateEmbedLangTransform.cmd MyInstaller.msi en-us 1033
rem call CreateEmbedLangTransform.cmd MyInstaller.msi neutral 0
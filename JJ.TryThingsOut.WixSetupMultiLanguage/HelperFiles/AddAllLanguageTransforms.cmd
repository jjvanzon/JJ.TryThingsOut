rem Current directory is meant to be Visual Studio's OutDir variable (like "bin\Debug\").

rem Copy en_us => base installer.
echo "copy en-us\MyInstaller.msi MyInstaller.msi"
copy en-us\MyInstaller.msi MyInstaller.msi

echo "call AddSingleLanguageTransform.cmd MyInstaller.msi nl-nl 1043"
call AddSingleLanguageTransform.cmd MyInstaller.msi nl-nl 1043
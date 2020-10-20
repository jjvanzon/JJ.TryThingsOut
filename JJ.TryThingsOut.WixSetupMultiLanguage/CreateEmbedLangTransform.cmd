rem Current directory is meant to be Visual Studio's OutDir variable (like "bin\Debug\").

echo "set MsiName=%1"
set MsiName=%1

echo "set lang=%2"
set lang=%2

echo "set langcode=%3"
set langcode=%3

echo "copy %MsiName%.msi %MsiName%_%lang%.msi"
copy %MsiName%.msi %MsiName%_%lang%.msi

echo "cscript WiLangId.vbs %MsiName%_%lang%.msi Product %langcode% > CreateLangTransform_%lang%.txt"
cscript WiLangId.vbs %MsiName%_%lang%.msi Product %langcode% > CreateLangTransform_%lang%.txt

echo "MsiTran.exe -g %MsiName%.msi %MsiName%_%lang%.msi %lang%.mst >> CreateLangTransform_%lang%.txt"
MsiTran.exe -g %MsiName%.msi %MsiName%_%lang%.msi %lang%.mst >> CreateLangTransform_%lang%.txt

echo "cscript wisubstg.vbs %MsiName%.msi %lang%.mst %langcode% >> CreateLangTransform_%lang%.txt"
cscript wisubstg.vbs %MsiName%.msi %lang%.mst %langcode% >> CreateLangTransform_%lang%.txt

echo "cscript wisubstg.vbs %MsiName%.msi >> CreateLangTransform_%lang%.txt"
cscript wisubstg.vbs %MsiName%.msi >> CreateLangTransform_%lang%.txt
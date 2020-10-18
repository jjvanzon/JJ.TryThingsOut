rem Current directory is meant to be Visual Studio's OutDir variable (like "bin\Debug\").
set OutDir=%CD%

set MsiName=%1
set lang=%2
set langcode=%3
copy %MsiName%.msi %MsiName%_%lang%.msi
cscript WiLangId.vbs %MsiName%_%lang%.msi Product %langcode% > CreateLangTransform_%lang%.txt
MsiTran.exe -g %MsiName%.msi %MsiName%_%lang%.msi %lang%.mst >> CreateLangTransform_%lang%.txt
cscript wisubstg.vbs FinalMasterInstaller\%MsiName%.msi %lang%.mst %langcode% >> CreateLangTransform_%lang%.txt
cscript wisubstg.vbs FinalMasterInstaller\%MsiName%.msi >> CreateLangTransform_%lang%.txt
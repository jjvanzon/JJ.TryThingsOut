rem Current directory is meant to be Visual Studio's OutDir variable (like "bin\Debug\").
set OutDir=%CD%

set MsiNameWithoutExtension=%1
set lang=%2
set langcode=%3
copy %MsiNameWithoutExtension%.msi %MsiNameWithoutExtension%_%lang%.msi
cscript WiLangId.vbs %MsiNameWithoutExtension%_%lang%.msi Product %langcode% > CreateLangTransform_%lang%.txt
MsiTran.exe -g %MsiNameWithoutExtension%.msi %MsiNameWithoutExtension%_%lang%.msi %lang%.mst >> CreateLangTransform_%lang%.txt
cscript wisubstg.vbs FinalMasterInstaller\%MsiNameWithoutExtension%.msi %lang%.mst %langcode% >> CreateLangTransform_%lang%.txt
cscript wisubstg.vbs FinalMasterInstaller\%MsiNameWithoutExtension%.msi >> CreateLangTransform_%lang%.txt
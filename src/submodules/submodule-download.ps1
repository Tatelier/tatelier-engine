Set-ExecutionPolicy RemoteSigned -Scope CurrentUser

$now = Get-Date -Format "hhmmss"
$DxLibCSDLLZipUrl = "https://dxlib.xsrv.jp/DxLib/DxLibDotNet3_23.zip"
$DxLibCSDllZipFilePath = "$env:TEMP\dxlib.zip"
$DxLibCSDllZipExpandFolder = "$env:TEMP\dxlib_$now"



Invoke-WebRequest -Uri $DxLibCSDLLZipUrl -OutFile $DxLibCSDllZipFilePath

Expand-Archive -Path $DxLibCSDllZipFilePath -DestinationPath $DxLibCSDllZipExpandFolder
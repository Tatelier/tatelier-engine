$now = Get-Date -Format "hhmmss"
$DxLibCSDLLZipUrl = "https://avatars.githubusercontent.com/u/91860836?v=4"
$DxLibCSDllZipFilePath = "$env:TEMP\dxlib.zip"
$DxLibCSDllZipExpandFolder = "$env:TEMP\dxlib_$now"

Invoke-WebRequest -Uri $DxLibCSDLLZipUrl -OutFile $DxLibCSDllZipFilePath

Expand-Archive -Path $DxLibCSDllZipFilePath -DestinationPath $DxLibCSDllZipExpandFolder
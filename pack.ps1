$defaultVersion="1.0.0"
$workingDirectory = Get-Location
$zip = "$workingDirectory\build\tools\7za.exe"
$nuget = "$workingDirectory\build\tools\nuget.exe"

function ZipCurrentModule
{
    Param ([String]$moduleName)
	
	$targetDirectory = "..\..\..\..\..\buildoutput\extended-content-copy"
	
	New-Item -type Directory -Force -Path $targetDirectory
	
	$versionOutputDirectory = $targetDirectory + "\" + $version
    Robocopy.exe $defaultVersion\ $versionOutputDirectory\ /S
	
	#$viewsOutputDirectory = $targetDirectory + "\Views"
	#Robocopy.exe "Views" $viewsOutputDirectory\ /S
	
	Robocopy.exe ".\Plugins" $targetDirectory\"Plugins" /S
	
	Robocopy.exe ".\" $targetDirectory\ "module.config"

    #((Get-Content -Path module.config -Raw).TrimEnd() -Replace $defaultVersion, $version ) | Set-Content -Path module.config
	
	Set-Location "$workingDirectory\buildoutput\extended-content-copy"
    Start-Process -NoNewWindow -Wait -FilePath $zip -ArgumentList "a", "$moduleName.zip", "$version", "Views", "module.config"
    #((Get-Content -Path module.config -Raw).TrimEnd() -Replace $version, $defaultVersion ) | Set-Content -Path module.config
    #Remove-Item $version -Force -Recurse
}

$oldPackageDirectory = "$workingDirectory\buildoutput\extended-content-copy";
if (Test-Path $oldPackageDirectory) {
	Remove-Item $oldPackageDirectory -Force -Recurse
}

Set-Location "$workingDirectory\src"
#msbuild /p:Configuration=Release

$fullVersion=[System.Reflection.Assembly]::LoadFrom("src\Alloy.Sample\bin\ExtendedContentCopy.dll").GetName().Version
$version="$($fullVersion.major).$($fullVersion.minor).$($fullVersion.build)"
Write-Host "Creating nuget with $version version"

Write-Host " - ensure buildoutput directory"
New-Item -type Directory -Force -Path $workingDirectory\buildoutput

Write-Host " - zip module files"
Set-Location Alloy.Sample\modules\_protected\extended-content-copy
ZipCurrentModule -moduleName extended-content-copy

Write-Host " - creating nuget"
Set-Location $workingDirectory
Start-Process -NoNewWindow -Wait -FilePath $nuget -ArgumentList "pack", "$workingDirectory\src\Alloy.Sample\ExtendedContentCopy.nuspec", "-Version $version"

$nugetName = "ExtendedContentCopy." + $version + ".nupkg";
Write-Host " - moving nuget" $nugetName "to buildoutput"
if (Test-Path "buildoutput\$nugetName") {
  Write-Host " - deleting existing nuget package"
  Remove-Item "buildoutput\$nugetName"
}
Move-Item -Path "$nugetName" -Destination "buildoutput\$nugetName"
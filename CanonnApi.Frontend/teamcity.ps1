# clean up dist folder from possible earlier build
$strFolderName="dist"
If (Test-Path $strFolderName){
	Remove-Item $strFolderName -Recurse
}

$version = (Get-Content version.txt)

# set build version based on branch (if set by teamcity)
$branch = [Environment]::GetEnvironmentVariable("teamcity.build.branch")
if (!$branch) {
	$branch = "localdev"
}
$branch = $branch.ToLower()
$buildCounter = [Environment]::GetEnvironmentVariable("build.counter")
if ($buildCounter) {
	$buildCounter = "-{0:D4}" -f [int]$buildCounter
}

if ($branch -eq "master") {
	$suffix = "";
} else {
	$suffix = "-$branch$buildCounter".replace('/','-')
}

$versionString = "$version$suffix";

# set new version tag to teamcity
Write-Host "##teamcity[buildNumber '$versionString']"

# write version number to files
(Get-Content .\package.json).replace('"version": "1.0.0",', '"version": "' + $versionString + '",') | Set-Content .\package.json

npm run ng -- build --target production --aot

# revert version number after build
(Get-Content .\package.json).replace('"version": "' + $versionString + '",', '"version": "1.0.0",') | Set-Content .\package.json

# required for packing
dotnet restore
dotnet pack --no-build --output dist/nupkg /p:Version=$versionString

# clean up dist folder from possible earlier build
$strFolderName="dist"
If (Test-Path $strFolderName){
	Remove-Item $strFolderName
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
	$buildCounter = "-$buildCounter".ToLower()
}

if ($branch -eq "refs/heads/master") {
	$suffix = "";
} else {
	$suffix = "-$branch$buildCounter"
}

$versionString = "$version$suffix";

# set new version tag to teamcity
Write-Host "##teamcity[buildNumber '$versionString']"

# write version number to files
(Get-Content .\package.json).replace('"version": "1.0.0",', '"version": "' + $versionString + '",') | Set-Content .\package.json

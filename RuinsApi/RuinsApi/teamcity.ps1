# clean up dist folder from possible earlier build
$strFolderName="dist"
If (Test-Path $strFolderName){
	Remove-Item $strFolderName
}

# set teamcity build version based on branch
$branch = [Environment]::GetEnvironmentVariable("teamcity.build.branch").ToLower()
if ($branch -eq "master" -or $branch -eq "<default>") {
	$branch = "";
} else {
	$branch = "-$branch"
}

$buildCounter = [Environment]::GetEnvironmentVariable("build.counter").ToLower()

Write-Host "##teamcity[buildNumber '1.0.0$branch-$buildCounter']"

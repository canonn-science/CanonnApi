$versionObj = [version](Get-Content version.txt)
$version = '{0}.{1}.{2}' -f $versionObj.Major, $versionObj.Minor, $versionObj.Build

# set build version based on branch (if set by teamcity)
$branch = [Environment]::GetEnvironmentVariable("teamcity.build.branch")
if (!$branch) {
	$branch = "localdev"
}
$branch = $branch.ToLower()
$buildCounter = [Environment]::GetEnvironmentVariable("build.counter")
if ($buildCounter) {
	$buildCounter = "-{0:D4}" -f $buildCounter
}

if ($branch -eq "master") {
	$suffix = "";
} else {
	$suffix = "-$branch$buildCounter"
}

$versionString = "$version$suffix";

# set new version tag to teamcity
Write-Host "##teamcity[buildNumber '$versionString']"

dotnet clean -c Release

dotnet restore

dotnet publish -c Release -r win8-x64 /p:Version=$versionString

dotnet pack --no-build --output bin/nupkg /p:Version=$versionString

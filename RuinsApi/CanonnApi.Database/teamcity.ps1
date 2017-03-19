# clean up dist folder from possible earlier build
$strFolderName="dist"
If (Test-Path $strFolderName){
	Remove-Item $strFolderName
}

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

# write version number to assemblyinfo
$path = ".\properties\AssemblyInfo.cs"
(Get-Content $path) | ForEach-Object{
    if($_ -match '\[assembly: AssemblyVersion\("(.*)"\)\]'){
        '[assembly: AssemblyVersion("{0}.{1}")]' -f $version, $versionObj.Revision
    } ElseIf ($_ -match '\[assembly: AssemblyFileVersion\("(.*)"\)\]') {
        '[assembly: AssemblyFileVersion("{0}")]' -f $version, $versionObj.Build
    } ElseIf ($_ -match '\[assembly: AssemblyInformationalVersion\("(.*)"\)\]') {
        '[assembly: AssemblyInformationalVersion("{0}")]' -f $versionString
    } Else {
        # Output line as is
        $_
    }
} | Set-Content $path

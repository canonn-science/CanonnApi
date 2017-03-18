Get-ChildItem Env:

$branch = [Environment]::GetEnvironmentVariable("teamcity.build.branch").ToLower()
$buildCounter = [Environment]::GetEnvironmentVariable("build.counter").ToLower()

Write-Host "##teamcity[buildNumber '1.0.0-$branch-$buildCounter']"

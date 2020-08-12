$ErrorActionPreference = "Stop";

New-Item -ItemType Directory -Force -Path ./nuget

dotnet tool restore

pushd ./src/AgencyPro
Invoke-Expression "./build.ps1 $args"
popd

pause
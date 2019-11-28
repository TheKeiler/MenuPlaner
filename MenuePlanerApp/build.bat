echo version %1

"tools\NuGet\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "tools" "-ExcludeVersion"
"tools\Fake\tools\Fake.exe" "\\\\\\MenuePlanerApp\build.fsx" version=%1

#r @packages/FAKE/tools/FakeLib.dll"

open Fake
open BuildHelpers

Target "common-build" (fun () ->
	RestorePackages "MenuePlaner.sln"
	
	MSBuild "MenuePlaner/bin/Debug" "Build" [ (
	"Configuration", "Debug"); ("Plattform", "Any CPU"
	) ] [ "MenuePlaner.sln" ] |> ignore
)

Target "common-tests" (fun () ->
	RunNUnitTests "MenuePlaner/bin/Debug/Menueplaner.Tests.dll"
	"MenuePlaner/bin/Debug/testresults.xml" |> ignore
)

"common-build"
 ==> "common-tests"
 
RunTargetOrDefault "common-tests"
 
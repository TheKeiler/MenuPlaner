#r @packages/FAKE/tools/FakeLib.dll"

open Fake
open BuildHelpers

Target "common-build" (fun () ->
	RestorePackages "MenuePlanerPCL.sln"
	
	MSBuild "MenuePlaner/bin/Debug" "Build" [ (
	"Configuration", "Debug"); ("Plattform", "Any CPU"
	) ] [ "MenuePlanerPCL.sln" ] |> ignore
)

Target "common-tests" (fun () ->
	RunNUnitTests "MenuePlaner/bin/Debug/MenueplanerPCL.Tests.dll"
	"MenuePlaner/bin/Debug/testresults.xml" |> ignore
)

"common-build"
 ==> "common-tests"
 
RunTargetOrDefault "common-tests"
 
#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.MSBuild
nuget Fake.DotNet.Testing.NUnit
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.DotNet
open Fake.DotNet.Testing
open Fake.Core

// Properties
let buildDir = "./build/"
let testDir  = "./test/"

// Targets
Target.create "Clean" (fun _ ->
    Shell.CleanDirs [buildDir; testDir]
)

Target.create "common-build" (fun _ ->
   !! "src/app/**/*.csproj"
     |> MSBuild.runRelease id buildDir "common-build"
     |> Trace.logItems "AppBuild-Output: "
)

Target.create "common-tests" (fun _ ->
    !! "src/test/**/*.csproj"
      |> MSBuild.runDebug id testDir "common-tests"
      |> Trace.logItems "TestBuild-Output: "
)

Target.create "Test" (fun _ ->
    !! (testDir + "/NUnit.Test.*.dll")
      |> NUnit3.run (fun p ->
          {p with
                ShadowCopy = false })
)

Target.create "Default" (fun _ ->
    Trace.trace "Hello World from FAKE"
)

 
// Dependencies
open Fake.Core.TargetOperators
"Clean"
  ==> "common-build"
  ==> "common-tests"
  ==> "Default"

// start build
RunTargetOrDefault "common-tests"
 
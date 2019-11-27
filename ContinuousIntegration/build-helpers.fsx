module BuildHelpers

open Fake
open Fake.XamarinHelper

let Exec command args =
	let result = Shell.Exec(command, args)
	
	if result <> 0 then failwith "%s exited with
	error %d" command result
	
let RunNUnitTests dllPath xmlPath =
	Exec "/Library/Frameworks/Mono.
	framework/Versions/Current/bin/nunit-console4" (
	dllPath + " -xml=" + xmlPath)
	TeamCityHelper.sendTeamCityNUnitImport xmlPath
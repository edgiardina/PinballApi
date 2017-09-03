#tool nuget:?package=NUnit.ConsoleRunner&version=3.5.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var version = AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Version : "0.0.1";
var releaseBinPath = "./StatusPageIo/StatusPageIo.Api/bin/Release";
var artifactsDirectory = "./artifacts";

Task("Restore-NuGet-Packages")
	.Does(() => {
		NuGetRestore("./StatusPageIo.sln");
	});

Task("Setup")
	.Does(() => { 
		CreateDirectory(artifactsDirectory);
	});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() => {
		MSBuild("./StatusPageIo.sln", settings =>
			settings.SetConfiguration(configuration));
	});

Task("UnitTest")
	.IsDependentOn("Build")
	.IsDependentOn("Setup")
	.Does(() => {
		var resultsFile = artifactsDirectory + "/NUnitResults.xml";
		NUnit3("./StatusPageIo/StatusPageIo.UnitTests/bin/Release/StatusPageIo.UnitTests.dll", new NUnit3Settings()
		{
			Results = resultsFile,
		});

		if(AppVeyor.IsRunningOnAppVeyor)
		{
			AppVeyor.UploadTestResults(resultsFile, AppVeyorTestResultsType.NUnit3);
		}
	});

Task("Pack")
	.IsDependentOn("Build")
	.IsDependentOn("Setup")
	.Does(() => {
		NuGetPack("./StatusPageIo/StatusPageIo.Api/StatusPageIo.Api.nuspec", new NuGetPackSettings()
		{
			Version = version,
			ArgumentCustomization = args => args.Append("-Prop Configuration=" + configuration),
			BasePath = releaseBinPath,
			OutputDirectory = artifactsDirectory,
		});
	});

Task("UploadNugetPackages")
	.IsDependentOn("Pack")
	.Does(() => {
		if(!AppVeyor.IsRunningOnAppVeyor)
		{
			return;
		}

		var files = GetFiles(artifactsDirectory + "/*.nupkg");
		foreach(var file in files)
		{
			AppVeyor.UploadArtifact(file);
		}
	});

Task("Default")
	.IsDependentOn("Build")
	.IsDependentOn("UnitTest")
	.IsDependentOn("Pack")
	.IsDependentOn("UploadNugetPackages");
  
RunTarget(target);
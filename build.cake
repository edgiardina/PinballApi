#tool nuget:?package=NUnit.ConsoleRunner&version=3.5.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var version = AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Version : "0.0.1";
var releaseBinPath = "./PinballApi/bin/Release";
var artifactsDirectory = "./artifacts";
var resultsFile = artifactsDirectory + "/NUnitResults.xml";
var wpprKey = Argument("wpprKey", "");

Task("Restore-NuGet-Packages")
	.Does(() => {
		NuGetRestore("./PinballApi.sln");
	});

Task("Setup")
	.Does(() => { 
		CreateDirectory(artifactsDirectory);
	});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() => {
		MSBuild("./PinballApi.sln", settings =>
			settings.SetConfiguration(configuration));
	});

	
Task("UnitTest")
	.IsDependentOn("Build")
	.IsDependentOn("Setup")
	.Does(() => {		
		var settings = new NUnit3Settings();
		settings.NoResults = false;		
		settings.Results = new[] { new NUnit3Result { FileName = resultsFile } };   		
		
		var config = File("./PinballApi.Tests/bin/Release/PinballApi.Tests.dll.config");	
		if(AppVeyor.IsRunningOnAppVeyor)
		{
			wpprKey = EnvironmentVariable("ifpa-key");
		}
		
		XmlPoke(config, "/configuration/appSettings/add[@key = 'WPPRKey']/@value", wpprKey);

		NUnit3("./PinballApi.Tests/bin/Release/PinballApi.Tests.dll", settings);

})
	.Finally(() => {  
		if(AppVeyor.IsRunningOnAppVeyor)
		{
			AppVeyor.UploadTestResults(resultsFile, AppVeyorTestResultsType.NUnit3);
		}
});

Task("Pack")
	.IsDependentOn("Build")
	.IsDependentOn("Setup")
	.Does(() => {
		NuGetPack("./PinballApi/PinballApi.nuspec", new NuGetPackSettings()
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
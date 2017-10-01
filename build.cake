﻿#tool nuget:?package=NUnit.ConsoleRunner&version=3.5.0
#addin "Cake.FileHelpers"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var version = AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Version : "0.0.1";
var releaseBinPath = "./PinballApi/bin/Release";
var artifactsDirectory = "./artifacts";
var resultsFile = artifactsDirectory + "/NUnitResults.xml";
var wpprKey = Argument("wpprKey", "");

Task("Restore-NuGet-Packages")
	.Does(() => {
		//DotNetCoreRestore("./PinballApi.sln");
		NuGetRestore("./PinballApi.sln");
	});

Task("Setup")
	.Does(() => { 
		CreateDirectory(artifactsDirectory);
	});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() => {
		 MSBuild("./PinballApi.sln", new MSBuildSettings 
			{
				Verbosity = Verbosity.Minimal,
				ToolVersion = MSBuildToolVersion.VS2017,
				Configuration = configuration,
				ArgumentCustomization = args => args.Append("/p:SemVer=" + version)
			});
	});

	
Task("UnitTest")
	.IsDependentOn("Build")
	.IsDependentOn("Setup")
	.Does(() => {			
		var config = File("./PinballApi.Tests/bin/Release/netcoreapp2.0/appsettings.json");	
		if(AppVeyor.IsRunningOnAppVeyor)
		{
			wpprKey = EnvironmentVariable("ifpa-key");
		}
		
		string text = TransformTextFile(config, "[", "]")
					   .WithToken("INSERT YOUR KEY HERE", wpprKey)
					   .ToString();

		FileWriteText(config, text);
		DotNetCoreTest("./PinballApi.Tests/", new  DotNetCoreTestSettings
		 {
			 Configuration = configuration,
			 NoBuild = true
		 });

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
		 var settings = new DotNetCorePackSettings
		 {
			 Configuration = configuration,
			 OutputDirectory = artifactsDirectory
		 };

		 //<PackageVersion>1.0.0</PackageVersion>
		 XmlPoke("./PinballApi/PinballApi.csproj", "Project/PropertyGroup/PackageVersion", version);
		DotNetCorePack("./PinballApi/", settings);
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
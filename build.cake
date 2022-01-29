#tool nuget:?package=NUnit.ConsoleRunner&version=3.5.0
#addin nuget:?package=Cake.FileHelpers&version=3.0.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var version = AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Version : "0.0.1";
var artifactsDirectory = "./artifacts";
var resultsFile = artifactsDirectory + "/NUnitResults.xml";
var wpprKey = Argument("wpprKey", "");
var OPDBToken = Argument("OPDBToken", "");

Task("Restore-NuGet-Packages")
	.Does(() => {
		//DotNetCoreRestore("./PinballApi.sln");
		//NuGetRestore("./PinballApi.sln");
		StartProcess("nuget", new ProcessSettings{ Arguments = "restore" });
	});

Task("Setup")
	.Does(() => { 
		CreateDirectory(artifactsDirectory);

		//commented out for now, patching happens on AppVeyor
		 //<PackageVersion>1.0.0</PackageVersion>
		 //XmlPoke("./PinballApi/PinballApi.csproj", "Project/PropertyGroup/PackageVersion", version);
		 //XmlPoke("./PinballApi/PinballApi.csproj", "Project/PropertyGroup/Version", version);
	});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() => {
		 DotNetCoreBuild("./PinballApi.sln", new DotNetCoreBuildSettings 
			{
				Configuration = configuration
			});
	});

	
Task("UnitTest")
	.IsDependentOn("Build")
	.IsDependentOn("Setup")
	.Does(() => {			
		var config = File("./PinballApi.Tests/bin/"+configuration+"/net6.0/appsettings.json");	
		if(AppVeyor.IsRunningOnAppVeyor)
		{
			wpprKey = EnvironmentVariable("ifpa-key");
			OPDBToken = EnvironmentVariable("opdb-token");
		}
		
		string text = TransformTextFile(config, "[", "]")
					   .WithToken("INSERT YOUR KEY HERE", wpprKey)
					   .WithToken("INSERT YOUR TOKEN HERE", OPDBToken)
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
			//AppVeyor.UploadTestResults(resultsFile, AppVeyorTestResultsType.NUnit3);
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
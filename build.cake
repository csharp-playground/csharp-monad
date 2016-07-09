#tool "nuget:?package=Fixie"
#addin "nuget:?package=Cake.Watch"

var target = Argument("target", "Default");

Task("test")
    .Does(() => {
            DotNetBuild("TryMonad.sln");
            Fixie("TryMonad/bin/Debug/TryMonad.dll");
    });

Task("just")
    .Does(() => {
            DotNetBuild("JustTryMonad/JustTryMonad.csproj");
            Fixie("JustTryMonad/bin/Debug/JustTryMonad.dll");
    });

Task("linq")
    .Does(() => {
            DotNetBuild("Linq.Tests/Linq.Tests.csproj");
            Fixie("Linq.Tests/bin/Debug/Linq.Tests.dll");
    });

Task("watch")
    .Does(() => {
        var settings = new WatchSettings {
            Recursive = true,
            Path = "./",
            Pattern = "*Tests.cs"
        };
        Watch(settings, (changed) => {
            RunTarget("test");
        });
    });

RunTarget(target);
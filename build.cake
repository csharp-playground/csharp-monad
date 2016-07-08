#tool "nuget:?package=Fixie"

var target = Argument("target", "Default");

Task("test")
    .Does(() => {
            DotNetBuild("TryMonad.sln");
            Fixie("TryMonad/bin/Debug/TryMonad.dll");
    });

RunTarget(target);
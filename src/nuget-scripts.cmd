dotnet pack --configuration release

dotnet nuget push .\DistributedLockManagerNet\bin\Release\DistributedLockManager.Net.1.0.5.nupkg -s https://www.nuget.org -k oy2azhhxxuq3fqdrza7af635kmuoi5dupae7tmx7ai5d34

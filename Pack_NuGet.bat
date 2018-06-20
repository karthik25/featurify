rmdir /Q /S src\Featurify.Contracts\NuGet
rmdir /Q /S src\Featurify\NuGet
dotnet pack src\Featurify.Contracts --configuration release --output NuGet
dotnet pack src\Featurify --configuration release --output NuGet

cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../src/Bin/netstandard/Release netstandard2.0 LogoFX.Server.Bootstrapping.* /E
cd ../../
nuget pack contents/LogoFX.Server.Bootstrapping.nuspec -OutputDirectory ../../output
nuget install Appveyor.TestLogger -Version 2.0.0
nuget install OpenCover -OutputDirectory packages -Version 4.6.519
nuget install coveralls.net -OutputDirectory packages -Version 0.412.0


.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -target:dotnet.exe -targetargs:"test "".\MPT.Math.UnitTests\MPT.Math.UnitTests.csproj""" -filter:"+[MPT.Math*]*" -oldStyle -output:opencoverCoverage.xml

.\packages\coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i .\opencoverCoverage.xml
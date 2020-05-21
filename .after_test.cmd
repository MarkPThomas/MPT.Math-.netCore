nuget install OpenCover -OutputDirectory packages -Version 4.6.519

.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -target:dotnet.exe -targetargs:"test "".\MPT.Math.UnitTests\MPT.Math.UnitTests.csproj"" " -filter:"+[MPT.Math*]*" -oldStyle -output:opencoverCoverage.xml

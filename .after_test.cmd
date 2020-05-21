.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user  -filter:"+[*]*" -target:".\tools\NUnit.ConsoleRunner.3.11.1\tools\nunit3-console.exe" -targetargs:".\MPT.Math.UnitTests\bin\Debug\netcoreapp2.1\MPT.Math.UnitTests.dll"
 
.\tools\coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i .\results.xml
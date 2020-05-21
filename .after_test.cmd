.\Math\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:".\Math\tools\NUnit.Runners.2.6.4\tools\nunit-console.exe" -targetargs:"/nologo /noshadow .\Math\MPT.Math.UnitTests\bin\Release\netcoreapp2.1\MPT.Math.UnitTests.dll" -filter:"+[*]* -[*.Tests]*" -register:user
 
.\Math\tools\coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i .\results.xml
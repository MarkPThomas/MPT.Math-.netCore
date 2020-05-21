SonarScanner.MSBuild.exe begin /k:"MarkPThomas_MPT.SE.CrossSection-.netCore" /n:"MarkPThomas_MPT.SE.CrossSection-.netCore" /v:"1.0" /o:"markpthomas-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="751c7e83861178a7af7cb29f1443f48f076ac5d0" /d:sonar.cs.opencover.reportsPaths="%CD%\opencover.xml"

nuget install NUnit.Runners -Version 2.6.4 -OutputDirectory %CD%/tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory %CD%/tools

REM cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319\
"C:\Program Files (x86)\MSBuild\14.0\Bin\MsBuild.exe" MPT.Math.sln /t:Rebuild
%CD%\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:"%CD%\tools\NUnit.Runners.2.6.4\tools\nunit-console.exe" -targetargs:"/nologo /noshadow %CD%\MPT.Math.UnitTests\bin\Debug\netcoreapp3.1\MPT.Math.UnitTests.dll" -filter:"+[*]* -[*.Tests]*" -output:"%CD%\opencover.xml" -register:user

SonarScanner.MSBuild.exe end /d:sonar.login="751c7e83861178a7af7cb29f1443f48f076ac5d0"
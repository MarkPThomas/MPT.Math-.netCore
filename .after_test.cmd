nuget install xunit.runner.console -OutputDirectory packages -Version 2.4.1  
    
nuget install OpenCover -OutputDirectory packages -Version 4.6.519
    
dotnet tool install coveralls.net --version 1.0.0-beta0002 --tool-path tools
    
	
.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -target:dotnet.exe "-targetargs:"".\packages\xunit.runner.console.2.4.1\tools\netcoreapp2.1\xunit.console.dll"" "".\MPT.Math.xUnitTests\bin\Debug\netcoreapp2.1\MPT.Math.xUnitTests.dll"" -noshadow -appveyor" -filter:"+[MPT.Math*]*" -oldStyle -output:opencoverCoverage.xml
    
.\tools\csmacnz.coveralls.exe --opencover -i opencoverCoverage.xml --repoToken $env:COVERALLS_REPO_TOKEN --commitId $env:APPVEYOR_REPO_COMMIT --commitBranch $env:APPVEYOR_REPO_BRANCH --commitAuthor $env:APPVEYOR_REPO_COMMIT_AUTHOR --commitEmail $env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL --commitMessage $env:APPVEYOR_REPO_COMMIT_MESSAGE --jobId $env:APPVEYOR_JOB_ID


#.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user  -filter:"+[*]*" -target:".\tools\NUnit.ConsoleRunner.3.11.1\tools\nunit3-console.exe" -targetargs:".\MPT.Math.UnitTests\bin\Debug\netcoreapp2.1\MPT.Math.UnitTests.dll" -output:coverage.xml
 
#.\tools\coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i .\coverage.xml


#.\tools\NUnit.ConsoleRunner.3.11.1\tools\nunit3-console.exe .\MPT.Math.UnitTests\bin\Debug\netcoreapp2.1\MPT.Math.UnitTests.dll

#%CD%\tools\NUnit.ConsoleRunner.3.11.1\tools\nunit3-console.exe -targetargs:"%CD%\MPT.Math.UnitTests\bin\Debug\netcoreapp2.1\MPT.Math.UnitTests.dll
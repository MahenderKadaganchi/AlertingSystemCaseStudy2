@echo off
set "BAT_PATH=%~dp0"



@echo off


call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\Tools\VsDevCmd.bat"

echo %BAT_PATH%

echo Executing batch file

:: param1 =     certificate hash for cash microservice : default no certificate

:: param2 =     Build Type: default Debug, Range: Debug or Release
::Win32
::              
:: To Make it to work open BranchConfiguration.json then set the value of "name": "BranchProfile" 

::              to as per Build Type like, "value": "Release" or "value": "Debug", By default the vlaue is debug
::X64

::              To Make it to work open BranchConfiguration64.json then set the value of "name": "BranchProfile" 

::              to as per Build Type like, "value": "Release" or "value": "Debug", By default the vlaue is debug

:: param3 =     Infra Compiling: default: build, Range: nobuild OR build
:: param4 =     Platform: default: x64 or Win32, Range: Win32 OR x64

devenv "%BAT_PATH%\PatientMonitorServerAPI.sln" /build Debug 
pause
mstest /TestContainer:"%BAT_PATH%JsonFormatValidatorLib.Tests\bin\Debug\JsonFormatValidatorLib.Tests.dll" /Test:Given_ValidJsonString_When_IsValidFormatInvoked_Expected_True /Test:Given_InvalidJsonString_When_IsValidFormatInvoked_Expected_False

mstest /TestContainer:"%BAT_PATH%PatientSpo2ValidatorLib.Tests\bin\Debug\PatientSpo2ValidatorLib.Tests.dll" /Test:Given_ValidSpo2Value_When_PatientSpo2Validator_Invoked_FalseExpected /Test:Given_PositiveInvalidSpo2Value_When_PatientSpo2Validator_Invoked_TrueExpected
/Test:Given_NegativeInvalidSpo2Value_When_PatientSpo2Validator_Invoked_TrueExpected

mstest /TestContainer:"%BAT_PATH%PatientTemperatureValidatorLib.Tests\bin\Debug\PatientTemperatureValidatorLib.Tests.dll" /Test:Given_ValidTemperatureValue_When_PatientTemperatureValidator_Invoked_FalseExpected /Test:Given_PositiveInvalidTemperatureValue_When_PatientTemperatureValidator_Invoked_TrueExpected
/Test:Given_NegativeInvalidTemperatureValue_When_PatientTemperatureValidator_Invoked_TrueExpected

mstest /TestContainer:"%BAT_PATH%PatientPulseRateValidatorLib.Tests\bin\Debug\PatientPulseRateValidatorLib.Tests.dll" /Test:Given_ValidPulseRateValue_When_PatientPulseRateValidator_Invoked_FalseExpected /Test:Given_PositiveInvalidPulseRateValue_When_PatientPulseRateValidator_Invoked_TrueExpected
/Test:Given_NegativeInvalidPulseRateValue_When_PatientPulseRateValidator_Invoked_TrueExpected

mstest /TestContainer:"%BAT_PATH%JsonPatientDataExtractorLib.Tests\bin\Debug\JsonPatientDataExtractorLib.Tests.dll" /Test:Given_ValidJsonString_When_PatientDataExtractorInvoked_Expected_PatientDataObject 
 

pause
 
set "SIM_PATH=C:\Users\320067390\Downloads\Simian\bin\"

cd "%SIM_PATH%"

simian-2.5.10.exe "%BAT_PATH%\**\*.cs"  

pause

start "" http://localhost:59372/api/BedMonitor


pause
using IPatientDataWriterContractsLib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PatientDataCsvWriterLib;
using PatientDataExtractorContractsLib;
using PatientDataFormatValidatorContractsLib;
using PatientDataParameterValidatorContractsLib;
using PatientDataTextWriterLib;

namespace PatientMonitorServerAPI.Helpers
{
    public static class Helper
    {
        #region Property
        
        public static IPatientDataFormatValidator PatientDataFormatValidator { get; set; }

        public static IPatientDataExtractor PatientDataExtractor { get; set; }

        public static IPatientDataParameterValidator Spo2ParameterValidator { get; set; }

        public static IPatientDataParameterValidator TemperatureParameterValidator { get; set; }

        public static IPatientDataParameterValidator PulseRateParameterValidator { get; set; }

        public static string[] Result { get; set; }
        #endregion





    }
}

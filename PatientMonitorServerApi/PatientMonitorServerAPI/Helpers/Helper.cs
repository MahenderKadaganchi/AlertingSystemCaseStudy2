using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PatientDataExtractorContractsLib;
using PatientDataFormatValidatorContractsLib;
using PatientDataParameterValidatorContractsLib;

namespace PatientMonitorServerAPI.Helpers
{
    public static class Helper
    {
        #region Fields

        private static IPatientDataFormatValidator _jsonFormatValidator;
        private static IPatientDataExtractor _jsonPatientDataExtractor;
        private static IPatientDataParameterValidator _spo2ParameterValidator;
        private static IPatientDataParameterValidator _temperatureParameterValidator;
        private static IPatientDataParameterValidator _pulseRateParameterValidator;
        private static string _dischargedPatientId;
        public static string[] Result = new string[4];


        #endregion

        #region Property
        
        public static IPatientDataFormatValidator PatientDataFormatValidator
        {
            get { return _jsonFormatValidator; }
            set { _jsonFormatValidator = value; }
        }

        public static IPatientDataExtractor PatientDataExtractor
        {
            get { return _jsonPatientDataExtractor; }
            set { _jsonPatientDataExtractor = value; }
        }

        public static IPatientDataParameterValidator Spo2ParameterValidator
        {
            get { return _spo2ParameterValidator; }
            set { _spo2ParameterValidator = value; }
        }

        public static IPatientDataParameterValidator TemperatureParameterValidator
        {
            get { return _temperatureParameterValidator; }
            set { _temperatureParameterValidator = value; }
        }

        public static IPatientDataParameterValidator PulseRateParameterValidator
        {
            get { return _pulseRateParameterValidator; }
            set { _pulseRateParameterValidator = value; }
        }

        public static string patientDischarged
        {
            get { return _dischargedPatientId; }
            set { _dischargedPatientId = value; }
        }

        
        #endregion





    }
}

using System;
using PatientDataFormatValidatorContractsLib;
using Newtonsoft.Json.Linq;

namespace JsonFormatValidatorLib
{
    public class JsonFormatValidator : IPatientDataFormatValidator
    {
        
        public bool IsValidFormat(string patData)
        {
            try
            {
                JToken.Parse(patData);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            
        }
    }
}

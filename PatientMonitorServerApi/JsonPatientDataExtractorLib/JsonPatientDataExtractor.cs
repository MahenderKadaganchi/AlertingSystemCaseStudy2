using Newtonsoft.Json;
using PatientDataExtractorContractsLib;
using PatientDataModule;


namespace JsonPatientDataExtractorLib
{
    public class JsonPatientDataExtractor : IPatientDataExtractor
    {
        public PatientData PatientDataExtractor(string patientData)
        {
            PatientData pd = JsonConvert.DeserializeObject<PatientData>(patientData);
            return pd;
        }
    }
}

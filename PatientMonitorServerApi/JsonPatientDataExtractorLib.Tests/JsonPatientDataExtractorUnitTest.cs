using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PatientDataExtractorContractsLib;
using PatientDataModule;

namespace JsonPatientDataExtractorLib.Tests
{
    [TestClass]
    public class JsonPatientDataExtractorUnitTest
    {
        [TestMethod]
        public void Given_ValidJsonString_When_PatientDataExtractorInvoked_Expected_PatientDataObject()
        {
            PatientData dummy = new PatientData
                {PatientId = "GBZFRONG", Spo2 = 66, PulseRate = 119, Temperature = 90.35};
            string pat = JsonConvert.SerializeObject(dummy);
            IPatientDataExtractor jsonDataExtractor = new JsonPatientDataExtractor();
            var patient = new PatientData{PatientId = "GBZFRONG", Spo2 = 66,PulseRate = 119,Temperature = 90.35};
            var actual = jsonDataExtractor.PatientDataExtractor(pat);
                    
            Assert.AreEqual(patient.GetType(),actual.GetType());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientDataFormatValidatorContractsLib;

namespace JsonFormatValidatorLib.Tests
{
    public static class Initialization
    {
        public static bool Expected = true;

    }

    [TestClass]
    public class JsonFormatValidatorUnitTest
    {
        [TestMethod]
        public void Given_ValidJsonString_When_IsValidFormatInvoked_Expected_True()
        {
            IPatientDataFormatValidator dataFormatValidator = new JsonFormatValidator();
            Initialization.Expected = true;
            string patData = "{'PatientID':'BKAFVFBU','SPO2':91.0,'PulseRate':72.0,'Temperature':97}";
            bool actual = dataFormatValidator.IsValidFormat(patData);
            Assert.AreEqual(Initialization.Expected, actual);
        }
        [TestMethod]
        public void Given_InvalidJsonString_When_IsValidFormatInvoked_Expected_False()
        {
            IPatientDataFormatValidator dataFormatValidator = new JsonFormatValidator();
            Initialization.Expected = false;
            string patData = "['PatientID':'BKAFVFBU','SPO2':91.0,'PulseRate':72.0,'Temperature':97]";
            bool actual = dataFormatValidator.IsValidFormat(patData);
            Assert.AreEqual(Initialization.Expected, actual);
        }

    }
}

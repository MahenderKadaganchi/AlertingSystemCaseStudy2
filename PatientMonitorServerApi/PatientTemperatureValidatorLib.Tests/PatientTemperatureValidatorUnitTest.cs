using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientDataParameterValidatorContractsLib;

namespace PatientTemperatureValidatorLib.Tests
{
    public static class Initialization
    {
        public static bool Expected = true;

    }
    [TestClass]
    public class PatientTemperatureValidatorUnitTest
    {
        [TestMethod]
        public void Given_ValidTemperatureValue_When_PatientTemperatureValidator_Invoked_FalseExpected()
        {
            IPatientDataParameterValidator temperatureValidator = new PatientTemperatureValidator();
            Initialization.Expected = false;
            bool actual = temperatureValidator.ParameterValidate(97.8);
            Assert.AreEqual(Initialization.Expected, actual);
        }

        [TestMethod]
        public void Given_PositiveInvalidTemperatureValue_When_PatientTemperatureValidator_Invoked_FalseExpected()
        {
            IPatientDataParameterValidator temperatureValidator = new PatientTemperatureValidator();
            Initialization.Expected = true;
            bool actual = temperatureValidator.ParameterValidate(99.1);
            Assert.AreEqual(Initialization.Expected, actual);
        }

        [TestMethod]
        public void Given_NegativeInvalidTemperatureValue_When_PatientTemperatureValidator_Invoked_FalseExpected()
        {
            IPatientDataParameterValidator temperatureValidator = new PatientTemperatureValidator();
            Initialization.Expected = true;
            bool actual = temperatureValidator.ParameterValidate(96.9);
            Assert.AreEqual(Initialization.Expected, actual);
        }
    }
}

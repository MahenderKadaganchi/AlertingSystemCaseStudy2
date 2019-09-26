using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientDataParameterValidatorContractsLib;

namespace PatientPulseRateValidatorLib.Tests
{
    public static class Initialization
    {
        public static bool Expected = true;

    }

    [TestClass]
    public class PatientPulseRateValidatorUnitTest
    {
        [TestMethod]
        public void Given_ValidPulseRateValue_When_PatientPulseRateValidator_Invoked_FalseExpected()
        {
            IPatientDataParameterValidator pulseRateValidator = new PatientPulseRateValidator();
            Initialization.Expected = false;
            bool actual = pulseRateValidator.ParameterValidate(72);
            Assert.AreEqual(Initialization.Expected, actual);
        }

        [TestMethod]
        public void Given_PositiveInvalidPulseRateValue_When_PatientPulseRateValidator_Invoked_TrueExpected()
        {
            IPatientDataParameterValidator pulseRateValidator = new PatientPulseRateValidator();
            Initialization.Expected = true;
            bool actual = pulseRateValidator.ParameterValidate(110);
            Assert.AreEqual(Initialization.Expected, actual);
        }
        [TestMethod]
        public void Given_NegativeInvalidPulseRateValue_When_PatientPulseRateValidator_Invoked_TrueExpected()
        {
            IPatientDataParameterValidator pulseRateValidator = new PatientPulseRateValidator();
            Initialization.Expected = true;
            bool actual = pulseRateValidator.ParameterValidate(59);
            Assert.AreEqual(Initialization.Expected, actual);
        }



    }
}

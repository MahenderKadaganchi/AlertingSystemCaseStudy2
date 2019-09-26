using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientDataParameterValidatorContractsLib;

namespace PatientSpo2ValidatorLib.Tests
{
    public static class Initialization
    {
        public static bool Expected = true;

    }
    [TestClass]
    public class PatientSpo2ValidatorUnitTest
    {
        [TestMethod]
        public void Given_ValidSpo2Value_When_PatientSpo2Validator_Invoked_FalseExpected()
        {
            IPatientDataParameterValidator spo2Validator = new PatientSpo2Validator();
            Initialization.Expected = false;
            bool actual = spo2Validator.ParameterValidate(98);
            Assert.AreEqual(Initialization.Expected, actual);

        }
        [TestMethod]
        public void Given_PositiveInvalidSpo2Value_When_PatientSpo2Validator_Invoked_TrueExpected()
        {
            IPatientDataParameterValidator spo2Validator = new PatientSpo2Validator();
            Initialization.Expected = true;
            bool actual = spo2Validator.ParameterValidate(80);
            Assert.AreEqual(Initialization.Expected, actual);

        }

        [TestMethod]
        public void Given_NegativeInvalidSpo2Value_When_PatientSpo2Validator_Invoked_TrueExpected()
        {
            IPatientDataParameterValidator spo2Validator = new PatientSpo2Validator();
            Initialization.Expected = true;
            bool actual = spo2Validator.ParameterValidate(80);
            Assert.AreEqual(Initialization.Expected, actual);

        }

    }
}

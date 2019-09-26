namespace PatientDataParameterValidatorContractsLib
{
    public interface IPatientDataParameterValidator
    {
        bool ParameterValidate<T>(T parameter);
    }
}

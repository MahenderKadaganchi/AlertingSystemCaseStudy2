using PatientDataModule;

namespace PatientDataExtractorContractsLib
{
    public interface IPatientDataExtractor
    {
        PatientData PatientDataExtractor(string patientData);
    }
}

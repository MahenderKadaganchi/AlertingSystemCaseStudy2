using System.Collections.Generic;
using System.IO;
using JsonFormatValidatorLib;
using JsonPatientDataExtractorLib;
using Microsoft.AspNetCore.Mvc;
using PatientDataModule;
using PatientMonitorServerAPI.Helpers;
using PatientPulseRateValidatorLib;
using PatientSpo2ValidatorLib;
using PatientTemperatureValidatorLib;

namespace PatientMonitorServerAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class BedMonitorController : ControllerBase
    {

        // GET: api/BedMonitor
        [HttpGet]
        public ActionResult<string> GetPatientIds()
        {
            return UpdatePatientIdDetails();
        }

        [HttpGet]
        [Route("discharge")]
        public ActionResult<string> GetDischargeInformation()
        {

            return Helper.patientDischarged;
        }
        

        [HttpPost]
        public ActionResult<string[]> PostPatientId([FromBody] string patientId)
        {
            return Given_ActionResult(patientId);
        }

        private static ActionResult<string[]> Given_ActionResult(string patientId)
        {
            string vitals = RandomValueGenerator.RandomDataGenerator();
            Helper.PatientDataFormatValidator = new JsonFormatValidator();
            Helper.PatientDataExtractor = new JsonPatientDataExtractor();
            Helper.Spo2ParameterValidator = new PatientSpo2Validator();
            Helper.PulseRateParameterValidator = new PatientPulseRateValidator();
            Helper.TemperatureParameterValidator = new PatientTemperatureValidator();
            Helper.Result[3] = patientId;
            if (Helper.PatientDataFormatValidator.IsValidFormat(vitals))
            {
                PatientData patientData = Helper.PatientDataExtractor.PatientDataExtractor(vitals);
                if (Helper.Spo2ParameterValidator.ParameterValidate(patientData.Spo2))
                {
                    Helper.Result[0] = "Abnormal Spo2";
                }
                else
                {
                    Helper.Result[0] = "Normal Spo2";
                }

                if (Helper.PulseRateParameterValidator.ParameterValidate(patientData.PulseRate))
                {
                    Helper.Result[1] = "Abnormal PulseRate";
                }
                else
                {
                    Helper.Result[1] = "Normal PulseRate";
                }

                if (Helper.TemperatureParameterValidator.ParameterValidate(patientData.Temperature))
                {
                    Helper.Result[2] = "Abnormal Temperature";
                }
                else
                {
                    Helper.Result[2] = "Normal Temperature";
                }


                return Helper.Result;
            }
            else
            {
                return Helper.Result;
            }
        }

        private static void Given_NewMethod(List<string> patientList)
        {
            using (StreamReader sr =
                new StreamReader(@Path.GetFullPath(Directory.GetCurrentDirectory() + @"\serverdata.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    patientList.Add(line);
                }

                // Read the stream to a string, and append to list of patientId.
            }
        }

        private static string UpdatePatientIdDetails()
        {
            List<string> patientList = new List<string>();
            string patientId = "";
            bool flag = false;
            using (StreamReader sr =
                new StreamReader(Path.GetFullPath(Directory.GetCurrentDirectory() + @"\serverdata.txt")))
            {
                while (!sr.EndOfStream)
                {
                    var ln = sr.ReadLine();
                    if (!ln.Contains("Assigned") && !flag)
                    {
                        flag = true;
                        patientId = ln;
                        patientList.Add(ln + "Assigned");
                    }
                    else
                    {
                        patientList.Add(ln);
                    }

                }

            }

            using (StreamWriter sw =
                new StreamWriter(
                    Path.GetFullPath(Directory.GetCurrentDirectory() + @"\serverdata.txt")))
            {
                foreach (var patient in patientList)
                {
                    sw.WriteLine(patient);
                }
            }

            return patientId;
        }
    }
}

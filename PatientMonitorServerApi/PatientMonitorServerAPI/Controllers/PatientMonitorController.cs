using System.Collections.Generic;
using System.IO;
using IPatientDataWriterContractsLib;
using Microsoft.AspNetCore.Mvc;
using PatientDataCsvWriterLib;
using PatientDataTextWriterLib;
using PatientMonitorServerAPI.Helpers;

namespace PatientMonitorServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMonitorController : ControllerBase
    {
        private static readonly IPatientDataWriter TextDataWriter= new PatientDataTextWriter();
        private static readonly IPatientDataWriter CsvDataWriter = new PatientDataCsvWriter();
        // GET: api/PatientMonitor
        [HttpGet]
        public string[] GetPatientVitals()
        {
            return Helper.Result;
        }

        // POST: api/PatientMonitor/Discharge
        [HttpPost]
        [Route("Discharge")]
        public string PostPatientId_ToDischarge([FromBody] string value)
        {
            return DischargePatient(value);
        }

        [HttpPost]
        [Route("StoreData")]
        public string PostPatientDetails_ToAdmit([FromBody] string value)
        {
            string patId = value;
            if (value.Contains(','))
            {
                patId = value.Substring(0, value.IndexOf(','));
            }
            
            bool status = CheckForPatientExistence(patId);
            if (!status)
            {
                TextDataWriter.WriteData(patId);
               CsvDataWriter.WriteData(value);
            }
            return (!status).ToString();
        }




        private static string DischargePatient(string value)
        {
            string patId = value;
            if (UpdatePatientIdDetails(patId))
                return "Patient Discharged successfully";
            else
            {
                return "Cannot find required PatientId to discharge";
            }
        }

        private bool CheckForPatientExistence(string patient)
        {
            bool status = false;
            using (StreamReader sr =
                new StreamReader(Path.GetFullPath(Directory.GetCurrentDirectory() + @"\serverdata.txt")))
            {
                while (!sr.EndOfStream)
                {
                    var ln = sr.ReadLine();
                    if (ln==patient||ln==patient+"Assigned")
                    {
                        status = true;
                    }
                }
            }
            return status;
        }

        private static bool UpdatePatientIdDetails(string patId)
        {
            List<string> patientList = new List<string>();
            using (StreamReader sr =
                new StreamReader(Path.GetFullPath(Directory.GetCurrentDirectory() + @"\serverdata.txt")))
            {
                while (!sr.EndOfStream)
                    {
                        var ln = sr.ReadLine();
                        if (ln != null && !ln.Contains(patId))
                            patientList.Add(ln);
                    }             
            }
            using (StreamWriter sw =
                    new StreamWriter(
                        Path.GetFullPath(Directory.GetCurrentDirectory() + @"\serverdata.txt")))
                {
                    foreach (var patientId in patientList)
                    {
                        sw.WriteLine(patientId);
                    }
                    return true;
            }            
        }
    }
}

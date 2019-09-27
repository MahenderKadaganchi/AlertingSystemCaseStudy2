using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPatientDataWriterContractsLib;

namespace PatientDataCsvWriterLib
{
    public class PatientDataCsvWriter : IPatientDataWriter
    {
#region Private Fields
        private readonly string _path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\PatientDataBase.csv");
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        #endregion

#region WriteData Method
        public bool WriteData(string input)
        {
            bool status = false;
            try
            {
                WritePatientDetails(input);
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return status;
        }
#endregion

        #region Private Methods
        private void WritePatientDetails(string data)
        {
            if (!File.Exists(_path))
            {
                File.Create(_path).Close();
                WriteHeader();
            }
            _stringBuilder.AppendLine(data);
            File.AppendAllText(_path, _stringBuilder.ToString());
            _stringBuilder.Clear();
        }


        private void WriteHeader()
        {
            const string header = "PatientId,PatientName,DateOfBirth,Gender,Mobile";
            _stringBuilder.AppendLine(header);
        }
        #endregion
    }
}

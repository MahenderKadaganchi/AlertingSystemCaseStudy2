using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPatientDataWriterContractsLib;

namespace PatientDataTextWriterLib
{
    public class PatientDataTextWriter : IPatientDataWriter
    {
        #region Private Fields

        private readonly string _path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\serverdata.txt");
        private int _count = 0;
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        #endregion

        #region WriteData Method
        public bool WriteData(string input)
        {
            bool status = false;
            try
            {
                WritePId(input);
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
        private void WritePId(string id)
        {
            if ((_count == 0) && File.Exists(_path))
            {
                File.Delete(_path);
            }

            if (CheckFileExistence())
            {
                _stringBuilder.AppendLine(id);

                _count++;
            }
            File.AppendAllText(_path, _stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        
        private bool CheckFileExistence()
        {
            bool status = false;
            if (!File.Exists(_path))
            {
                File.CreateText(_path).Close();
                status = true;
            }
            if (File.Exists(_path) && (_count != 0))
            {
                status = true;

            }
            return status;
        }
        #endregion
    }
}

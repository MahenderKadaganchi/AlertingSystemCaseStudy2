using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataBaseCollectorLib
{
    class WritePatientId
    {
        private string _path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\..\PatientMonitorServerAPI\serverdata.txt");
        int count = 0;
        StringBuilder _stringBuilder = new StringBuilder();

        #region Methods
        public void WritePId(string Id)
        {
            if ((count == 0) && File.Exists(_path))
            {
                File.Delete(_path);
            }

            if (CheckFileExistence())
            {
                _stringBuilder.AppendLine(Id);

                count++;
            }
            File.AppendAllText(_path, _stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        #endregion

        #region Private Methods
        private bool CheckFileExistence()
        {
            bool status = false;
            if (!File.Exists(_path))
            {
                File.CreateText(_path).Close();
                status = true;
            }
            if (File.Exists(_path) && (count != 0))
            {
                status = true;

            }

            return status;
        }
        #endregion

    }
}

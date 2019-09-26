using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataBaseCollectorLib
{
    public class PatientDataWriter
    {
        #region Fields
        readonly StringBuilder _primaryStringBuilder = new StringBuilder();
        readonly StringBuilder _secondaryStringBuilder = new StringBuilder();
        private string _path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\..\PatientMonitorServerAPI\DataBase.csv");
        //public List<AdmissionDetails> details = new List<AdmissionDetails>();
        int count = 0;
        #endregion
        #region Method
        public void Write(List<string> PatientDataDetails)
        {
            //details.Add(admissionDetails);
            if ((count == 0) && File.Exists(_path))
            {
                File.Delete(_path);
            }

            if (CheckFileExistence())
            {
                WriteHeader();

                //Type type = admissionDetails.GetType();
                //System.Reflection.PropertyInfo[] properties = type.GetProperties();
                //GetPropertyValues(properties, admissionDetails);
                //_primaryStringBuilder.AppendLine(_secondaryStringBuilder.ToString());
                //_secondaryStringBuilder.Clear();
                foreach (var items in PatientDataDetails)
                {
                    _secondaryStringBuilder.Append(items + ",");


                }
                _primaryStringBuilder.AppendLine(_secondaryStringBuilder.ToString());
                _secondaryStringBuilder.Clear();



                File.AppendAllText(_path, _primaryStringBuilder.ToString());
                _primaryStringBuilder.Clear();
                count++;

            }

        }

        #endregion

        #region PrivateMethods

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

        private bool WriteHeader()
        {
            bool status = false;

            if (new FileInfo(_path).Length == 0)
            {
                //Type type = admissionDetails.GetType();
                //System.Reflection.PropertyInfo[] properties = type.GetProperties();
                //GetPropertyNames(properties);
                //_primaryStringBuilder.AppendLine(_secondaryStringBuilder.ToString());
                //_secondaryStringBuilder.Clear();
                //status = true;
                List<string> headers = new List<string>();
                headers.Add("PatientId");
                headers.Add("PatientName");
                headers.Add("DateOfBirth");
                headers.Add("Gender");
                headers.Add("Mobile");
                foreach (var items in headers)
                    _secondaryStringBuilder.Append(items + ",");

                _primaryStringBuilder.AppendLine(_secondaryStringBuilder.ToString());
                _secondaryStringBuilder.Clear();
                status = true;

            }
            return status;
        }
        private void GetPropertyValues(PropertyInfo[] properties, AdmissionDetails admissionDetails)
        {
            foreach (var property in properties)
            {
                if (property.GetValue(admissionDetails) == null)
                {
                    _secondaryStringBuilder.Append("NA" + ",");
                }
                else
                {
                    var tempValue = property.GetValue(admissionDetails).ToString();
                    tempValue = tempValue.Replace(',', ';');
                    _secondaryStringBuilder.Append(tempValue + ",");
                }
            }
        }



        private void GetPropertyNames(PropertyInfo[] properties)
        {
            foreach (var property in properties)
            {
                _secondaryStringBuilder.Append(property.Name + ",");
            }

        }
        #endregion
    }
}


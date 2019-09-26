using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PatientDataBaseCollectorLib
{
    public class AdmissionDetails
    {
       

        #region Properties
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Mobile { get; set; }
        #endregion


    }

}


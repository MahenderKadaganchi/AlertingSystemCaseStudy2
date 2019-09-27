using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPatientDataWriterContractsLib
{
    public interface IPatientDataWriter
    {
        bool WriteData(string input);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCI_SERIAL_CHECK_BIT.Models
{
    internal class MRes
    {
        public string serial {  get; set; }
        public string endDigit { get; set; }
        public string serialNew { get; set; }
        public string serialOld { get; set; }
        public string barcode { get; set; }
    }
}

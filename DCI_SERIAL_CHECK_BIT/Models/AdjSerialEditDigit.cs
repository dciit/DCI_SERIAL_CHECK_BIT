using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DCI_SERIAL_CHECK_BIT.Models
{
    public partial class AdjSerialEditDigit
    {
        public string Serial { get; set; }
        public string SerialOld { get; set; }
        public string SerialNew { get; set; }
        public bool? SerialStatus { get; set; }
    }
}

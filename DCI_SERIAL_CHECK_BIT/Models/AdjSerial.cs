using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DCI_SERIAL_CHECK_BIT.Models
{
    public partial class AdjSerial
    {
        public int? AdjId { get; set; }
        public string SerialOld { get; set; }
        public string ModelCodeOld { get; set; }
        public string ModelNameOld { get; set; }
        public string LineOld { get; set; }
        public string SerialNew { get; set; }
        public string ModelCodeNew { get; set; }
        public string ModelNameNew { get; set; }
        public string LineNew { get; set; }
        public string RefNo { get; set; }
        public int? AdjStatus { get; set; }
        public DateTime? UpdateDt { get; set; }
        public string EmCode { get; set; }
        public string Remark { get; set; }
    }
}

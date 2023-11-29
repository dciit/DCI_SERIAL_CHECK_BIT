using DCI_SERIAL_CHECK_BIT.Contexts;
using DCI_SERIAL_CHECK_BIT.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCI_SERIAL_CHECK_BIT
{
    public partial class Form1 : Form
    {
        DBSCM _DBSCM = new DBSCM();
        DBIOT _DBIOT = new DBIOT();
        OraConnectDB _DBALPHAPD = new OraConnectDB("ALPHAPD");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<MRes> resDiff = new List<MRes>();

            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.CommandText = "SELECT * FROM FH001 WHERE NWC = 'RPK' AND  CDATE >= '01-OCT-23' AND MODEL = '2Y147AKCX1A#A'";
            DataTable dt = _DBALPHAPD.Query(oracleCommand);
            foreach (DataRow dr in dt.Rows)
            {
                string serial = dr["SERIAL"].ToString();
                try
                {
                    if (!serial.Contains("GFB") && !serial.Contains("GF") && !serial.Contains("GT") && !serial.Contains("%"))
                    {
                        string barcode = dr["BARCODE"].ToString();
                        string endDigitOfFN = genDigit(serial);
                        string endDigitOfBarcode = barcode.Substring(barcode.Length - 1);
                        if (endDigitOfFN != endDigitOfBarcode)
                        {
                            resDiff.Add(new MRes()
                            {
                                serial = serial,
                                endDigit = endDigitOfFN,
                                serialOld = barcode,
                                serialNew = $"{serial}{endDigitOfFN}",
                                barcode = barcode
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message.ToString()} {serial}");
                }
            }
            //foreach (MRes item in resDiff)
            //{
            //    OracleCommand ora = new OracleCommand();
            //    ora.CommandText = "UPDATE FH001 SET BARCODE = '" + item.serialNew + "' WHERE SERIAL = '" + item.serial + "' AND NWC = 'RPK' AND BARCODE = '"  + item.barcode + "'";
            //   _DBALPHAPD.Query(ora);
            //    _DBSCM.AdjSerialEditDigit.Add(new AdjSerialEditDigit()
            //    {
            //        Serial = item.serial,
            //        SerialOld = item.barcode,
            //        SerialNew = item.serialNew,
            //        SerialStatus = true
            //    });
            //}
            //int insertLog = _DBSCM.SaveChanges();
            Console.WriteLine("123");



            //List<MRes> resDontUpdate = new List<MRes>();
            //List<AdjSerial> list = _DBSCM.AdjSerial.Where(x => x.UpdateDt >= DateTime.Parse("2023-10-29")).OrderByDescending(x => x.UpdateDt).ToList();
            //List<AdjSerialEditDigit> listUnWareHouse = _DBSCM.AdjSerialEditDigit.ToList();
            //foreach (AdjSerial item in list)
            //{
            //    var contextDontHaveInWarehouse = listUnWareHouse.FirstOrDefault(x => x.Serial == item.SerialNew);
            //    if (contextDontHaveInWarehouse != null)
            //    {
            //        try
            //        {
            //            if (!item.SerialNew.Contains("GFB") && !item.SerialNew.Contains("GF") && !item.SerialNew.Contains("GT") && !item.SerialNew.Contains("%"))
            //            {
            //                string endDigit = genDigit(item.SerialNew);
            //                res.Add(new MRes()
            //                {
            //                    serial = item.SerialNew,
            //                    endDigit = endDigit,
            //                    serialNew = $"{item.SerialNew}{endDigit}"
            //                });
            //            }
            //            else
            //            {
            //                resDontUpdate.Add(new MRes()
            //                {
            //                    serial = item.SerialNew,
            //                    endDigit = "",
            //                    serialNew = ""
            //                });
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.ToString());
            //        }

            //    }
            //}
            //foreach (MRes item in res.Take(500











            //    ))
            //{
            //    CheckBitAlphaPDFH001(item.serial, item.endDigit);
            //}
        }

        public void CheckBitAlphaPDFH001(string serial, string endDigitOfRef)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = @"SELECT * FROM FH001 WHERE NWC = 'RPK' AND SERIAL = '" + serial + "'";
                DataTable dt = _DBALPHAPD.Query(cmd);
                if (dt.Rows.Count > 0)
                {
                    string endDigitOfDit = dt.Rows[0]["BARCODE"].ToString();
                    endDigitOfDit = endDigitOfDit.Substring(endDigitOfDit.Length - 1);
                    if (endDigitOfDit != endDigitOfRef)
                    {
                        Console.WriteLine($"{endDigitOfDit},{endDigitOfRef}");
                    }
                    else
                    {
                        //MessageBox.Show($"{endDigitOfDit},{endDigitOfRef}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private string genDigit(string serialNew)
        {
            char[] subStr = serialNew.ToCharArray();
            //string m = subStr[2].ToString();
            //if (m == "A")
            //{
            //    subStr[2] = $"{"10".ToCharArray()[0]}"   "10".ToCharArray()[2];
            //}
            //else if (m == "B")
            //{
            //    subStr[2] = "11".ToCharArray()[0];
            //}
            //else if (m == "C")
            //{
            //    subStr[2] =  "12".ToCharArray()[0];
            //}
            int sum = 0;
            foreach (var oChar in serialNew)
            {
                string charLoop = oChar.ToString();
                if (charLoop.Equals("A"))
                {
                    charLoop = "10";
                }
                else if (charLoop.Equals("B"))
                {
                    charLoop = "11";
                }
                else if (charLoop.Equals("C"))
                {
                    charLoop = "12";
                }
                sum += int.Parse(charLoop);
            }
            int numDigit = (sum % 43);
            var cMasterDigit = _DBIOT.MasterDigit.Where(x => x.DigitValue == numDigit.ToString());
            return cMasterDigit != null ? cMasterDigit.FirstOrDefault().Digit : "";
        }
    }
}

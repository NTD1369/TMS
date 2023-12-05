using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class TimeEntryModel
    {
        public int Id { get; set; }
        public string UserCode { get; set; }
        public DateTime Date { get; set; }
        public float Hour { get; set; }
        public string PrjCode { get; set; }
        public string PrjName { get; set; }
        public string Bilable { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string SAPB1DB { get; set; }
        public string Task { get; set; }
        public string Comments { get; set; }
        public string Posted { get; set; }
        public string Phase { get; set; }
        public string Country { get; set; }
        public string EBMName { get; set; }
        public string ProspectName { get; set; }

        public string ContractLineId { get; set; }


        //hien thi them khong phai tu table TimeEntry:
        public string WBSName { get; set; }
        public string UserType { get; set; }
    }
    public class ViewTimeEntryModel : TimeEntryModel
    {
        //public DateTime? DayOfWeek { get; set; }
        public int STT { get; set; }
        public string CommentsDB { get; set; }

        public string DayOfWeek { get; set; }
        public float Mon { get; set; }
        public float Tue { get; set; }
        public float Wed { get; set; }
        public float Thu { get; set; }
        public float Fri { get; set; }
        public float Sat { get; set; }
        public float Sun { get; set; }
        public string MoreInfo { get; set; }
    }
}

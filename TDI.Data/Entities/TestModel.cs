using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class TestModel
    {
        public int Id { get; set; }
        public string UserCode { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public string PrjCode { get; set; }
        public string PrjName { get; set; }
        public string Bilable { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string SAPB1DB { get; set; }
        public string Task { get; set; }
        public string Comments { get; set; }
        public string Posted { get; set; }
        public string Phase { get; set; }
        public string Country { get; set; }
        public string EBMName { get; set; }
        public string ProspectName { get; set; }
    }
    public class ViewTestModel :TestModel
    {
        //public DateTime? DayOfWeek { get; set; }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class ContractLineModel
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Additional { get; set; }
        public string ContractCode { get; set; }
        public string ContractName { get; set; }
        public string ProjectCode { get; set; }
        public int LineId { get; set; }
        public string LineCode { get; set; }
        public string PM { get; set; }
        public string EmailPM { get; set; }

        public string ContractLineId { get; set; }
        public string EmployeeId { get; set; }
        public string Bill { get; set; }
        public string BaseLine { get; set; }


        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Mandays { get; set; }
        public float MandaysUsed { get; set; }
        public float MandaysRemain { get; set; }
        public int MandaysUpdate { get; set; }
        public string Status { get; set; }
        public string CustomF1 { get; set; }
        public string CustomF2 { get; set; }
        public string CustomF3 { get; set; }
        public string CustomF4 { get; set; }
        public string CustomF5 { get; set; }
        public string CustomF6 { get; set; }
        public string CustomF7 { get; set; }
        public string CustomF8 { get; set; }
        public string CustomF9 { get; set; }
        public string CustomF10 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

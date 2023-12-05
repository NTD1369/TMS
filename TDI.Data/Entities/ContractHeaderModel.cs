using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class ContractHeaderModel
    { 
        public string ContractCode { get; set; }
        public string EmployeeId { get; set; }

        public string ContractName { get; set; }
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime CustomDate { get; set; }
        public string ContractType { get; set; }
        public DateTime CustomDate2 { get; set; }
        public string Token { get; set; }
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
        public string Cost { get; set; }
        public string CostType { get; set; }
        public string Priority { get; set; }
        public string ProgressPercent { get; set; }
        public int Mandays { get; set; }
        public float MandaysUsed { get; set; }
        public float MandaysRemain { get; set; }
        public int MandaysUpdate { get; set; }
        public string PM { get; set; }

        public string ProjectCodeOnly { get; set; }
        

        // more info not in table db:
        public string ProjectName { get; set; }
        public string UserName { get; set; }

    }
}

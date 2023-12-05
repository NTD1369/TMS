using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class ContractModel
    {
        public string UserCode { get; set; }
        public string ContractCode { get; set; }
        public string ContractName { get; set; }
        public string ProjectCode { get; set; }
       // public string PrjName { get; set; } 
        public string Description { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Mandays { get; set; }
        public int MandaysUpdate { get; set; }
        public string PM { get; set; } 
        public string WBSCode { get; set; } 
    }
}

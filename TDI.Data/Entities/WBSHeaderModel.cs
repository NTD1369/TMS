using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class WBSHeaderModel
    {
        public string UserCode { get; set; }
        public string Additional { get; set; }
        public string ContractCode { get; set; }
        public string ContractName { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string LineCode { get; set; }

        public string PrjCode { get; set; }
        public string PrjName { get; set; }
        public int ContractLineId { get; set; }
        public string Description { get; set; }
        public int Mandays { get; set; }
        public int MandaysUpdate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string EmailPM { get; set; }
        public string PM { get; set; }
        public string Bill { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }


    }


}

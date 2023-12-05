using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class ContractAssignMemberModel
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Id { get; set; }
        public string ContractCode { get; set; }
        public string ContractName { get; set; }
        public string LineCode { get; set; }
        public string ContractLineId { get; set; }
        public int BaseLine { get; set; }
        public int LineId { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Mandays { get; set; }
        public int MandaysUpdate { get; set; }
        public string Status { get; set; }
        public string CustomF1 { get; set; }
        public string CustomF2 { get; set; }
        public string CustomF3 { get; set; }
        public string CustomF4 { get; set; }
        public string Email { get; set; }



        public string FullName { get; set; }

    }
}

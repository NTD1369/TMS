using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class ContractMemberModel
    {
        public string UserCode { get; set; }
        public string LineCode { get; set; }
        public int ContractMemberID { get; set; }
        public string ContractCode { get; set; }
        public string Name { get; set; }
        public string OptionCode { get; set; }
        public string ContractName { get; set; }
        public string EmployeeId { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class ProjectModel 
    {
        public string PrjCode { get; set; }
        public string PrjName { get; set; }
        public string Status { get; set; }
        public string UserCode { get; set; }
        public string ProjectCodeOnly { get; set; }


        //public ctrModel ctrModel { get; set; }
        //public string ContractCode { get; set; }
        //public string ContractName { get; set; }
        //public string ProjectCode { get; set; }
        //public string Description { get; set; }
        //public string Remark { get; set; }
    }
    public class ctrModel
    {

        public string ContractCode { get; set; }
        public string ContractName { get; set; }
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
    }
}

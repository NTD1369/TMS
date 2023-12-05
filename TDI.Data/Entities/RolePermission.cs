using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
	public partial class RolePermission
	{
		public Guid Id { get; set; }
		public Guid RoleId { get; set; }
		public string Permission { get; set; }
		public string FunctionId { get; set; } 
		public string Status { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		//public string CustomF1 { get; set; }
		//public string CustomF2 { get; set; }
		//public string CustomF3 { get; set; }
		//public string CustomF4 { get; set; }
		//public string CustomF5 { get; set; }
	}
}

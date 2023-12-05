using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
	public partial class MPermission
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		//public DateTime HolidayDateFr { get; set; }
		//public DateTime? HolidayDateTo { get; set; }
		//public string CreatedBy { get; set; }
		//public DateTime? CreatedOn { get; set; }
		//public string ModifiedBy { get; set; }
		//public DateTime? ModifiedOn { get; set; } 
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
	public partial class MFunctionModel
	{
		public string Id { get; set; }
		public string FunctionId { get; set; }
		public int NumOfList { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public string ParentId { get; set; }
		public string IconCss { get; set; }
		public int? SortOrder { get; set; }
		public string Type { get; set; }
		public string Status { get; set; }
		//public DateTime HolidayDateFr { get; set; }
		//public DateTime? HolidayDateTo { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public string CustomF1 { get; set; }
		public string CustomF2 { get; set; }
		public string CustomF3 { get; set; }
		public string CustomF4 { get; set; }
		public string CustomF5 { get; set; }
		public List<string> AvailablePermission { get; set; } = new List<string>();

        public List<MFunctionModel> Childrens { get; set; } = new List<MFunctionModel>();
    }
}

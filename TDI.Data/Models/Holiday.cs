using Dapper;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class HolidayModel
    {
        public int Id { get; set; }
        public string ProjectCode { get; set; }
        public string ContractName { get; set; }
        public string ContractCode { get; set; }
        public string WBSCode { get; set; }
        //[Required]
        //[Required(ErrorMessage = "First id is required")]          
        public string Title { get; set; }
        public DateTime HolidayDateFr { get; set; } = DateTime.Now;
        //[Display(Name = "Holiday from")]
        public DateTime HolidayDateTo { get; set; } = DateTime.Now;
        //[Display(Name = "Holiday to")]
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        //[Display(Name = "Country")]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

       
    }
}

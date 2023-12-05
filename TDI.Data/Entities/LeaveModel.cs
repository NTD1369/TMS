using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDI.Data.Entities
{
    public class LeaveModel
    {
        public int Id { get; set; }
        public string Username { get; set; }

        //[System.ComponentModel.DataAnnotations.Required]
        //[Range(typeof(int), "-100", "2100", ErrorMessage = "The LeaveType field is required.")]
        public int LeaveTypeId { get; set; }
        //[System.ComponentModel.DataAnnotations.Required]

        public string LeaveType { get; set; }

        //[System.ComponentModel.DataAnnotations.Required]
        //[Range(typeof(DateTime), "01/01/2023", "31/12/2100", ErrorMessage = "LeaveFrom must be between {1:d} and {2:d}")]
        public DateTime LeaveFrom { get; set; }

        //[System.ComponentModel.DataAnnotations.Required]
        //[Range(typeof(DateTime), "01/01/2023", "31/12/2100", ErrorMessage = "LeaveTo must be between {1:d} and {2:d}")]
        public DateTime LeaveTo { get; set; }

        public string Name { get; set; }

        public decimal NumberOfDays { get; set; }
        public string LeaveStatus { get; set; }
        public string Reason { get; set; }
        public bool AllDay { get; set; }
        public bool IsMorning { get; set; } = true;
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime StatusUpdated { get; set; }
        public string Remark { get; set; }
        public string IsMorning2 { get; set; }
        public string HREmail { get; set; }
        public string LeaveCountryName { get; set; }
        public string FullName { get; set; }
        public int CountryId { get; set; }

       

    }

    public partial class LeaveTypeModel
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsMorning { get; set; } 

        public bool Active { get; set; }
        public bool CarryForward { get; set; }
        public int MaxDay { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string ProjectCode { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string WBSCode { get; set; }
        public bool ExcludeDash { get; set; }
    }

    public class LeaveBalanceModel
    {
        public int Id { get; set; }
        //[System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string CountryName { get; set; }
        public int CountryId { get; set; }
        //public int Country { get; set; }




        [System.ComponentModel.DataAnnotations.Required]
        [Range(typeof(int), "2020", "2100", ErrorMessage = "Year must be between {1:d} and {2:d}")]
        public int Year { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Range(typeof(int), "-100", "2100", ErrorMessage = "The LeaveType field is required.")]
        public int TypeId { get; set; }
        //[System.ComponentModel.DataAnnotations.Required]
        public string LeaveType { get; set; }
        //[System.ComponentModel.DataAnnotations.Required]
        public decimal LeaveQuota { get; set; }
        public decimal BringLeave { get; set; }
        public decimal AdjustDay { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class OffInLieuModel
    {
        public Guid ID { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public decimal Days { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Range(typeof(int), "1", "12", ErrorMessage = "Month must be between {1:d} and {2:d}")]
        public int Month { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Range(typeof(int), "2020", "2100", ErrorMessage = "Year must be between {1:d} and {2:d}")]
        public int Year { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class LeaveBalanceReportModel : LeaveBalanceModel
    {
        public decimal TakenDays { get; set; }
        public decimal? M1 { get; set; }
        public decimal? M2 { get; set; }
        public decimal? M3 { get; set; }
        public decimal? M4 { get; set; }
        public decimal? M5 { get; set; }
        public decimal? M6 { get; set; }
        public decimal? M7 { get; set; }
        public decimal? M8 { get; set; }
        public decimal? M9 { get; set; }
        public decimal? M10 { get; set; }
        public decimal? M11 { get; set; }
        public decimal? M12 { get; set; }
        public decimal? LeaveSummary { get; set; }
        public decimal? LeaveRemain { get; set; }
        public decimal? LeaveTotal { get; set; }
        public decimal? OffInLieu { get; set; }
        public string Team { get; set; }
        public string LeaveCountryName { get; set; }
        public decimal? AnotherLeave { get; set; }

    }

    public class LeaveOfDayModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Reason { get; set; }
        public string Team { get; set; }

        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }

        public bool AllDay { get; set; }
        public bool IsMorning { get; set; }

        public string AllDayText
        {
            get
            {
                return this.AllDay ? "Yes" : "No";
            }
        }

        public string IsMorningText
        {
            get
            {
                string r = this.IsMorning ? "Morning" : "Afternoon";
                if (this.AllDay)
                    r = "Both";
                return r;
            }
        }
    }
}

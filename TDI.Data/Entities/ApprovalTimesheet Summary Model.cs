using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace TDI.Data.Entities
{
    public class RPT_ApprovalTimesheetSummaryModel
    {
         
        public string Country { get; set; }
        public string Department { get; set; }
        public string PrjCode { get; set; }
        public string ProjectManager { get; set; }
        public float HoursPending { get; set; }
        public float HoursApproved { get; set; }
        public float Total { get; set; }


        public string PrjName { get; set; }
        public string UserCode { get; set; }
    }


    //Country Department  UserCode ResourcesType   HoursMissing HoursPending    HoursApproved Total
    public class RPT_TimeEntrySummaryModel
    {
        public string Country { get; set; }
        public string Comments { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserCode { get; set; }
        public string ResourcesType { get; set; }
        public float UnSumitted { get; set; }
        public float HoursMissing { get; set; }
        public float HoursPending { get; set; }
        public float HoursApproved { get; set; }
        public float Total { get; set; }

        
        //t1.PrjCode, t1.PrjName, t2.PM, t1.Date, DATEPART(weekday, t1.date) as DayOfWeek => them vao cho bao cao chi tiet

        public string PrjCode { get; set; }
        public string PrjName { get; set; }
        public string PM { get; set; }

        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; }



    }

    //Country Department  ResourcesType ContractCode    EmployeeNo UserCode    FullName ContractName    PrjMandays WBSManday
    //Billable1A Presales1B  Leave1C Other1D Billable2W Presales2X  Leave2Y Other2Z Total EmployeeCategory 
    public class RPT_Billable_ReportModel
    {

        public string Country { get; set; }
        public string Department { get; set; }
        public string ResourcesType { get; set; }
        public string ContractCode { get; set; }
        public string EmployeeNo { get; set; }
        public string UserCode { get; set; }
        public string FullName { get; set; }
        public string ContractName { get; set; }
        public float PrjMandays { get; set; }
        public float WBSManday { get; set; } 

        public float Billable1A { get; set; }
        public float Presales1B { get; set; }
        public float Leave1C { get; set; }
        public float Other1D { get; set; }
        public float Billable2W { get; set; }
        public float Presales2X { get; set; }
        public float Leave2Y { get; set; }
        public float Other2Z { get; set; }
         
        public float Total { get; set; } 
        public string EmployeeCategory { get; set; }
    }
    public class RPT_BillableByProject_ReportModel
    {

        public string Country { get; set; }
        public string Comments { get; set; }
        public string Department { get; set; }
        public string ResourcesType { get; set; }
        public string ContractCode { get; set; }
        public string EmployeeNo { get; set; }
        public string UserCode { get; set; }
        public string FullName { get; set; }
        public string ContractName { get; set; }
        public float PrjMandays { get; set; }
        public float WBSManday { get; set; }

        public float Billable1A { get; set; }
        public float Presales1B { get; set; }
        public float Leave1C { get; set; }
        public float Other1D { get; set; }
        public float Billable2W { get; set; }
        public float Presales2X { get; set; }
        public float Leave2Y { get; set; }
        public float Other2Z { get; set; }

        public float Total { get; set; }
        public string EmployeeCategory { get; set; }
        public string PrjCountry { get; set; }
        public string LineCode { get; set; } //WBS name

        public string PM { get; set; } //Project Manager


    }
    
    public class RPT_ProjectPlannedActual_ReportModel
    {         
        public string Department { get; set; } 
        public string ContractCode { get; set; }
        public string ContractName { get; set; }
        public string PrjCountry { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Mandays { get; set; }
        public float MandaysUpdate { get; set; }
        public float MandaysUsed { get; set; }
        public float MandaysRemaining { get; set; }
        public float MandaysUsed2 { get; set; }
        public float MandaysRemaining2 { get; set; }          
         
    }

    public class rpt_TimeSummaryByProjectReportModel
    {
        public string UserCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string Name { get; set; }
        public float Hour { get; set; }
        public string PrjCode { get; set; }
        public string PrjName { get; set; }
        public string WBS { get; set; }
        public string Team { get; set; }
        public string Country { get; set; }
        public string PrjCountry { get; set; }
        public string Bill { get; set; }
         
    }

    public class ApprovalTimesheetEntryModel
    {
        public int IDS { get; set; }
        public string Status { get; set; }
        public string UserCode { get; set; }
        public string PrjName { get; set; }
        public string Billable { get; set; }
        public DateTime? Date { get; set; }
        public float Hour { get; set; }
        public string Comments { get; set; }
        public string ProjectCode { get; set; }
        
        public string StatusName { get; set; }
        public string LineCode { get; set; }

        public string Description { get; set; }


    }
    
}

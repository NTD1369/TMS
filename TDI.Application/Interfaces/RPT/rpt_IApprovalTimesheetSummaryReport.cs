using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces.RPT
{
    public interface rpt_IApprovalTimesheetSummaryReport
    {
        Task<GenericResult> GetAll(DateTime FromDate, DateTime ToDate, string UserCode);
        


        Task<GenericResult> Pending4ApprovalTimeEntry(DateTime FromDate, DateTime ToDate, string UserCode, string PM, int Status, bool MyEntry);

        Task<GenericResult> ApprovalTimeEntryByID(int ID);
        Task<GenericResult> RejectTimeEntryByID(int ID, string Remarks);

        //report:
        Task<GenericResult> RPT_TimeEntry(DateTime FromDate, DateTime ToDate, string UserCode);
        Task<GenericResult> RPT_TimeEntrySummary(DateTime FromDate, DateTime ToDate, string UserCode);
        Task<GenericResult> rpt_TimeSummaryByProjectReport(DateTime FromDate, DateTime ToDate, string UserCode,   string Country, int Status);
        Task<GenericResult> RPT_Billable_Report(DateTime FromDate, DateTime ToDate, string UserCode, string Country, string ResourcesType, string Department);
        Task<GenericResult> RPT_BillableByProject_Report(DateTime FromDate, DateTime ToDate, string UserCode, string Country, string ResourcesType, string Department);
        Task<GenericResult> RPT_ProjectPlannedActual_Report(DateTime FromDate, DateTime ToDate, string UserCode, string Country, string ResourcesType, string Department);

        GenericResult RPT_TimeEntrySummaryReport_Email();
    }
}

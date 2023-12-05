using TDI.Application.Interfaces;
using TDI.Application.Interfaces.RPT;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class ApprovalTimeSheetSummaryController
    {
        rpt_IApprovalTimesheetSummaryReport _approvalTimesheetSummaryService;
        public ApprovalTimeSheetSummaryController(rpt_IApprovalTimesheetSummaryReport approvalTimesheetSummaryService)
        {
            _approvalTimesheetSummaryService = approvalTimesheetSummaryService;
        }

        public async Task<GenericResult> GetAll(DateTime FromDate,DateTime ToDate, string UserCode) 
        {
            var result = await _approvalTimesheetSummaryService.GetAll(FromDate, ToDate, UserCode);
            return result; //la  1 report 1 approval 
        }

        public async Task<GenericResult> Pending4ApprovalTimeEntry(DateTime FromDate, DateTime ToDate, string UserCode, string PM, int Status, bool MyEntry)
        {
            var result = await _approvalTimesheetSummaryService.Pending4ApprovalTimeEntry(FromDate, ToDate, UserCode, PM, Status, MyEntry);
            return result;
        }
        public async Task<GenericResult> ApprovalTimeEntryByID(int ID)
        {
            var result = await _approvalTimesheetSummaryService.ApprovalTimeEntryByID(ID);
            return result;
        }
        public async Task<GenericResult> RejectTimeEntryByID(int ID, string Remarks)
        {
            var result = await _approvalTimesheetSummaryService.RejectTimeEntryByID(ID, Remarks);
            return result;
        }


        //Reports:
        public async Task<GenericResult> RPT_TimeEntry(DateTime FromDate, DateTime ToDate, string UserCode)
        {
            var result = await _approvalTimesheetSummaryService.RPT_TimeEntry(FromDate, ToDate, UserCode);
            return result;
        }
        public async Task<GenericResult> RPT_TimeEntrySummary(DateTime FromDate, DateTime ToDate, string UserCode)
        {
            var result = await _approvalTimesheetSummaryService.RPT_TimeEntrySummary(FromDate, ToDate, UserCode);
            return result;
        }

        public async Task<GenericResult> rpt_TimeSummaryByProjectReport(DateTime FromDate, DateTime ToDate,  string UserCode, string Country, int Status)
        {
            var result = await _approvalTimesheetSummaryService.rpt_TimeSummaryByProjectReport(FromDate, ToDate, UserCode, Country , Status);
            return result;
        }
        public async Task<GenericResult> RPT_Billable_Report(DateTime FromDate, DateTime ToDate, string UserCode, string Country, string ResourcesType, string Department)
        {
            var result = await _approvalTimesheetSummaryService.RPT_Billable_Report(FromDate, ToDate, UserCode, Country, ResourcesType, Department);
            return result;
        }
        public async Task<GenericResult> RPT_BillableByProject_Report(DateTime FromDate, DateTime ToDate, string UserCode, string Country, string ResourcesType, string Department)
        {
            var result = await _approvalTimesheetSummaryService.RPT_BillableByProject_Report(FromDate, ToDate, UserCode, Country, ResourcesType, Department);
            return result;
        }
        public async Task<GenericResult> RPT_ProjectPlannedActual_Report(DateTime FromDate, DateTime ToDate, string UserCode, string Country, string ResourcesType, string Department)
        {
            var result = await _approvalTimesheetSummaryService.RPT_ProjectPlannedActual_Report(FromDate, ToDate, UserCode, Country, ResourcesType, Department);
            return result;
        }
    }
}

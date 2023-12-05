using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TDI.Application.Interfaces.RPT;
using TDI.Data.Entities;
using TDI.Data.Repositories;
using TDI.Utilities.Dtos;

namespace TDI.Application.Implements.RPT
{
    public class rpt_ApprovalTimesheetSummary : rpt_IApprovalTimesheetSummaryReport
    {
        private readonly IGenericRepository<ApprovalTimesheetEntryModel> _ApprovalTimeEntryByIDModel;
        private readonly IGenericRepository<ApprovalTimesheetEntryModel> _ApprovalTimesheetEntryModel;
        private readonly IGenericRepository<RPT_TimeEntrySummaryModel> _timeEntrySummaryService;

        private readonly IGenericRepository<RPT_TimeEntrySummaryModel> _RPT_TimeEntrySummaryModel;
        private readonly IGenericRepository<rpt_TimeSummaryByProjectReportModel> _rpt_TimeSummaryByProjectReportModel;
        private readonly IGenericRepository<RPT_ApprovalTimesheetSummaryModel> _RPT_approvalTimesheetSummaryRepository;
        private readonly IGenericRepository<RPT_Billable_ReportModel> _RPT_Billable_ReportModelRepository;
        private readonly IGenericRepository<RPT_BillableByProject_ReportModel> _RPT_BillableByProject_ReportModelRepository;
        private readonly IGenericRepository<RPT_ProjectPlannedActual_ReportModel> _RPT_ProjectPlannedActual_ReportModelRepository;
        private readonly IMapper _mapper;

        public rpt_ApprovalTimesheetSummary(IGenericRepository<RPT_ApprovalTimesheetSummaryModel> RPT_approvalTimesheetSummaryRepository, 
            IGenericRepository<RPT_TimeEntrySummaryModel> RPT_TimeEntrySummaryRepository, 

            IGenericRepository<rpt_TimeSummaryByProjectReportModel> rpt_TimeSummaryByProjectReportModelRepository,
            IGenericRepository<RPT_Billable_ReportModel> RPT_Billable_ReportModelRepository,
            IGenericRepository<RPT_BillableByProject_ReportModel> RPT_BillableByProject_ReportModelRepository, 
            IGenericRepository<RPT_TimeEntrySummaryModel> timeEntrySummaryService,
        IGenericRepository<RPT_ProjectPlannedActual_ReportModel> RPT_ProjectPlannedActual_ReportModelRepository,

            IGenericRepository<ApprovalTimesheetEntryModel> ApprovalTimesheetEntryRepository,
            IGenericRepository<ApprovalTimesheetEntryModel> ApprovalTimeEntryByID,
            IMapper mapper)
        {
            _RPT_approvalTimesheetSummaryRepository = RPT_approvalTimesheetSummaryRepository;
            _RPT_TimeEntrySummaryModel = RPT_TimeEntrySummaryRepository;
            _rpt_TimeSummaryByProjectReportModel = rpt_TimeSummaryByProjectReportModelRepository;
            _RPT_Billable_ReportModelRepository = RPT_Billable_ReportModelRepository;
            _RPT_BillableByProject_ReportModelRepository = RPT_BillableByProject_ReportModelRepository;
            _RPT_ProjectPlannedActual_ReportModelRepository = RPT_ProjectPlannedActual_ReportModelRepository;
            _timeEntrySummaryService = timeEntrySummaryService;
            _ApprovalTimesheetEntryModel = ApprovalTimesheetEntryRepository;
            _ApprovalTimeEntryByIDModel = ApprovalTimeEntryByID;

            _mapper = mapper;

        }

        //add new =0 = reject, submit (pending ) = 1, approval =2
        public async Task<GenericResult> Pending4ApprovalTimeEntry(DateTime FromDate, DateTime ToDate, string UserCode, string PM, int Status, bool MyEntry )
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);

                parameter.Add("PM", PM);
                parameter.Add("Status", Status);
                if (MyEntry == true)
                {
                    var data = _ApprovalTimesheetEntryModel.GetAll($"Usp_Pending4ApproveTimesheetByPeriod_MyEntry", parameter, commandType: CommandType.StoredProcedure);
                    resulGetAll.Success = true;
                    resulGetAll.Data = data as List<ApprovalTimesheetEntryModel>;
                }   
                else                        
                {
                    var data = _ApprovalTimesheetEntryModel.GetAll($"Usp_Pending4ApproveTimesheetByPeriod", parameter, commandType: CommandType.StoredProcedure);
                    resulGetAll.Success = true;
                    resulGetAll.Data = data as List<ApprovalTimesheetEntryModel>;
                }
                 
            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }

        public async Task<GenericResult> ApprovalTimeEntryByID(int ID)
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("ID", ID); 
             
                var data = _ApprovalTimesheetEntryModel.GetAll($"Usp_Pending4ApproveTimesheetByID", parameter, commandType: CommandType.StoredProcedure);
                 resulGetAll.Success = true;
                resulGetAll.Data = data as List<ApprovalTimesheetEntryModel>;
            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }

        public GenericResult RPT_TimeEntrySummaryReport_Email()
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                //parameter.Add("ID", ID);

                var data = _timeEntrySummaryService.GetAll($"USP_RPT_TimeEntrySummaryReport_Email", parameter, commandType: CommandType.StoredProcedure);
                resulGetAll.Success = true;
                resulGetAll.Data = data as List<RPT_TimeEntrySummaryModel>;
            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }
        public async Task<GenericResult> RejectTimeEntryByID(int ID, string Remarks)
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("ID", ID);
                parameter.Add("Remarks", Remarks);

                //var data = _ApprovalTimesheetEntryModel.GetAll($"Usp_Pending4ApproveTimesheetByID_Reject", parameter, commandType: CommandType.StoredProcedure);
                //resulGetAll.Success = true;
                //resulGetAll.Data = data as List<ApprovalTimesheetEntryModel>;

                var affectedRows = _ApprovalTimesheetEntryModel.Update("Usp_Pending4ApproveTimesheetByID_Reject", parameter, commandType: CommandType.StoredProcedure);
                resulGetAll.Success = true;

            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }



//--------------REPORTS:
        public async Task<GenericResult> GetAll(DateTime FromDate,DateTime ToDate, string UserCode)
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);

                var data = _RPT_approvalTimesheetSummaryRepository.GetAll($"USP_rpt_ApprovalTimesheetSummaryReport", parameter, commandType: CommandType.StoredProcedure);
                resulGetAll.Success = true;
                resulGetAll.Data = data as List<RPT_ApprovalTimesheetSummaryModel>;
            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }
        public async Task<GenericResult> RPT_TimeEntry(DateTime FromDate, DateTime ToDate, string UserCode)
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);

                var data = _RPT_TimeEntrySummaryModel.GetAll($"USP_RPT_TimeEntryReport", parameter, commandType: CommandType.StoredProcedure);
                resulGetAll.Success = true;
                resulGetAll.Data = data as List<RPT_TimeEntrySummaryModel>;
            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }


        public async Task<GenericResult> RPT_TimeEntrySummary(DateTime FromDate, DateTime ToDate, string UserCode)
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);

                var data = _RPT_TimeEntrySummaryModel.GetAll($"USP_RPT_TimeEntrySummaryReport", parameter, commandType: CommandType.StoredProcedure);
                resulGetAll.Success = true;
                resulGetAll.Data = data as List<RPT_TimeEntrySummaryModel>;
            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }

        public async Task<GenericResult> rpt_TimeSummaryByProjectReport(DateTime FromDate, DateTime ToDate, string UserCode, string Country , int Status)
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                parameter.Add("Country", Country);
                parameter.Add("Status", Status);

                var data = _rpt_TimeSummaryByProjectReportModel.GetAll($"USP_rpt_TimeSummaryByProjectReport", parameter, commandType: CommandType.StoredProcedure);
                resulGetAll.Success = true;
                resulGetAll.Data = data as List<rpt_TimeSummaryByProjectReportModel>;
            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }


        public async Task<GenericResult> RPT_Billable_Report(DateTime FromDate, DateTime ToDate, string UserCode, string Country, string ResourcesType, string Department)
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                parameter.Add("Country", Country);
                parameter.Add("ResourcesType", ResourcesType);
                parameter.Add("Department", Department);

                var data = _RPT_Billable_ReportModelRepository.GetAll($"USP_RPT_Billable_Report", parameter, commandType: CommandType.StoredProcedure);
                resulGetAll.Success = true;
                resulGetAll.Data = data as List<RPT_Billable_ReportModel>;
            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }
        public async Task<GenericResult> RPT_BillableByProject_Report(DateTime FromDate, DateTime ToDate, string UserCode, string Country, string ResourcesType, string Department)
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                parameter.Add("Country", Country);
                parameter.Add("ResourcesType", ResourcesType);
                parameter.Add("Department", Department);

                var data = _RPT_BillableByProject_ReportModelRepository.GetAll($"USP_RPT_BillableByProject_Report", parameter, commandType: CommandType.StoredProcedure);
                resulGetAll.Success = true;
                resulGetAll.Data = data as List<RPT_BillableByProject_ReportModel>;
            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }
        public async Task<GenericResult> RPT_ProjectPlannedActual_Report(DateTime FromDate, DateTime ToDate, string UserCode, string Country, string ResourcesType, string Department)
        {
            GenericResult resulGetAll = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                parameter.Add("Country", Country);
                parameter.Add("ResourcesType", ResourcesType);
                parameter.Add("Department", Department);

                var data = _RPT_ProjectPlannedActual_ReportModelRepository.GetAll($"USP_RPT_ProjectPlannedActual_Report", parameter, commandType: CommandType.StoredProcedure);
                resulGetAll.Success = true;
                resulGetAll.Data = data as List<RPT_ProjectPlannedActual_ReportModel>;
            }
            catch (Exception ex)
            {
                resulGetAll.Success = false;
                resulGetAll.Message = ex.Message;
            }
            return resulGetAll;
        }


    }
}

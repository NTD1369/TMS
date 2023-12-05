using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Data.Models;
using TDI.Data.Repositories;
using TDI.Utilities.Dtos;

namespace TDI.Application.Implements
{
    public class LeaveService : ILeaveService
    {
        private readonly IGenericRepository<LeaveModel> _leaveRepository;
        private readonly IGenericRepository<LeaveBalanceReportModel> _leaveReportRepository;
        private readonly IGenericRepository<LeaveOfDayModel> _leaveOfDayRepository;
        //private readonly IMapper _mapper;

        public LeaveService(IGenericRepository<LeaveModel> leaveRepository, IGenericRepository<LeaveBalanceReportModel> leaveReportRepository, IGenericRepository<LeaveOfDayModel> leaveOfDayRepository)//, IMapper mapper)
        {
            _leaveRepository = leaveRepository;
            _leaveReportRepository = leaveReportRepository;
            _leaveOfDayRepository = leaveOfDayRepository;
            //_mapper = mapper;
        }

        #region Leave Employee
        
        public async Task<GenericResult> GetAllLeaveByUser(string userCode)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", userCode);

                var data = await _leaveRepository.GetAllAsync($"USP_S_Leave", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                if (data.Any())
                {
                    result.Data = data.ToList();
                }
                else
                {
                    result.Message = "Data not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> InsertLeaveAsync(LeaveModel leave)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Username", leave.Username);
                parameters.Add("LeaveTypeId", leave.LeaveTypeId);
                parameters.Add("LeaveFrom", leave.LeaveFrom);
                parameters.Add("LeaveTo", leave.LeaveTo);
                parameters.Add("NumberOfDays", leave.NumberOfDays);
                parameters.Add("LeaveStatus", leave.LeaveStatus);
                parameters.Add("Reason", leave.Reason);
                parameters.Add("AllDay", leave.AllDay);
                parameters.Add("IsMorning", leave.IsMorning);
                parameters.Add("CreatedBy", leave.CreatedBy);
                parameters.Add("Remark", leave.Remark);
                //if (leave.NumberOfDays > 0)
                //{
                //    leave.IsMorning = "All day";
                //}
                _leaveRepository.Execute($"USP_I_Leave", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Insert Leave success.";
               
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> UpdateLeaveAsync(LeaveModel leaveOld, LeaveModel leaveNew)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", leaveOld.Id);
                //parameters.Add("Username", leaveOld.Username);
                parameters.Add("LeaveTypeId", leaveNew.LeaveTypeId);
                parameters.Add("LeaveFrom", leaveNew.LeaveFrom);
                parameters.Add("LeaveTo", leaveNew.LeaveTo);
                parameters.Add("NumberOfDays", leaveNew.NumberOfDays);
                parameters.Add("LeaveStatus", leaveNew.LeaveStatus);
                parameters.Add("Reason", leaveNew.Reason);
                parameters.Add("AllDay", leaveNew.AllDay);
                parameters.Add("IsMorning", leaveNew.IsMorning);
                parameters.Add("ModifiedBy", leaveNew.ModifiedBy);
                //parameters.Add("Remark", leaveNew.Remark);

                _leaveRepository.Execute($"USP_U_Leave", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Update Leave success.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public GenericResult DeleteLeaveAsync(LeaveModel leave)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", leave.Id);
                parameters.Add("UserName", leave.Username);

                _leaveRepository.Execute($"USP_D_Leave", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Delete Leave success.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        #endregion

        #region Leave Approval

        public async Task<GenericResult> GetAllLeaveForApproval(string userCode, DateTime fromDate, DateTime toDate)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Approver", userCode);
                parameters.Add("FromDate", fromDate);
                parameters.Add("ToDate", toDate);

                var data = await _leaveRepository.GetAllAsync($"USP_S_LeaveApproval", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                if (data.Any())
                {
                    result.Data = data.ToList();
                }
                else
                {
                    result.Message = "Data not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> UpdateLeaveApprovalAsync(LeaveModel leaveOld, LeaveModel leaveNew)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", leaveOld.Id);
                parameters.Add("Username", leaveOld.Username);
                parameters.Add("LeaveStatus", leaveNew.LeaveStatus);
                parameters.Add("ApprovedBy", leaveNew.ApprovedBy);
                parameters.Add("Remark", leaveNew.Remark);

                _leaveRepository.Execute($"USP_U_LeaveApproval", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Update Leave success.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> CreateFromLeave(LeaveModel leave)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", leave.Username);
                parameters.Add("Date", leave.LeaveFrom);
                parameters.Add("Hour", leave.NumberOfDays);
                parameters.Add("LeaveTypeId", leave.LeaveTypeId);
                parameters.Add("LeaveStatus", leave.LeaveStatus);
                parameters.Add("Reason", leave.Reason);
                var affectedRows = _leaveRepository.Execute("USP_I_TimeEntryFromLeave", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        #endregion

        #region Leave Dashboard

        public async Task<GenericResult> GetLeaveDashboardByUser(string userName, int year)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserName", userName);
                parameters.Add("Year", year);

                var data = await _leaveReportRepository.GetAllAsync($"USP_S_LeaveDashboard", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                if (data.Any())
                {
                    result.Data = data.ToList();
                }
                else
                {
                    result.Message = "Data not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        #endregion

        #region Leave Report

        public async Task<GenericResult> GetLeaveBalanceReport(string userCode, string userName, int year)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", userCode);
                parameters.Add("UserName", userName);
                parameters.Add("Year", year);

                var data = await _leaveReportRepository.GetAllAsync($"USP_RPT_LeaveBalance", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                if (data.Any())
                {
                    result.Data = data.ToList();
                }
                else
                {
                    result.Message = "Data not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> GetWhoLeaveOfDay()
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();

                var data = await _leaveOfDayRepository.GetAllAsync($"USP_RPT_LeaveOfDay", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                if (data.Any())
                {
                    result.Data = data.ToList();
                }
                else
                {
                    result.Message = "Data not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> GetWhoLeaveOfMonth()
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();

                var data = await _leaveOfDayRepository.GetAllAsync($"USP_RPT_LeaveOfMonth", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                if (data.Any())
                {
                    result.Data = data.ToList();
                }
                else
                {
                    result.Message = "Data not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        #endregion
    }
}

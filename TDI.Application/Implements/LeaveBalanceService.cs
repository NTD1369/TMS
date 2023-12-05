using Dapper;
using RMS.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Data.Repositories;
using TDI.Utilities.Dtos;

namespace TDI.Application.Implements
{
    public class LeaveBalanceService : ILeaveBalanceService
    {
        private readonly IGenericRepository<LeaveBalanceModel> _leaveBalanceRepository;

        public LeaveBalanceService(IGenericRepository<LeaveBalanceModel> leaveBalanceRepository)
        {
            _leaveBalanceRepository = leaveBalanceRepository;
        }

        public async Task<GenericResult> GetAllLeaveBalance(int year)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Year", year);

                var data = await _leaveBalanceRepository.GetAllAsync($"USP_S_LeaveBalance", parameters, commandType: CommandType.StoredProcedure);
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

        public GenericResult InsertLeaveBalance(string userCode, LeaveBalanceModel leaveBalance)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserName", leaveBalance.UserName);
                parameters.Add("FullName", leaveBalance.FullName);
                parameters.Add("Year", leaveBalance.Year);
                parameters.Add("TypeId", leaveBalance.TypeId);
                parameters.Add("LeaveQuota", leaveBalance.LeaveQuota);
                parameters.Add("CountryId", leaveBalance.CountryId);
                //parameters.Add("BringLeave", leaveBalance.BringLeave);
                parameters.Add("AdjustDay", leaveBalance.AdjustDay);
                parameters.Add("Remark", leaveBalance.Remark);
                parameters.Add("CreatedBy", userCode);

                _leaveBalanceRepository.Execute($"USP_I_LeaveBalance", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Insert LeaveBalance success.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Insert LeaveBalance failed: " + ex.Message;
            }
            return result;
        }

        public GenericResult UpdateLeaveBalance(string userCode, LeaveBalanceModel leaveBalanceOld, LeaveBalanceModel leaveBalanceNew)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", leaveBalanceOld.Id);
                parameters.Add("UserName", leaveBalanceNew.UserName);
                parameters.Add("FullName", leaveBalanceNew.FullName);
                parameters.Add("Year", leaveBalanceNew.Year);
                parameters.Add("TypeId", leaveBalanceNew.TypeId);
                parameters.Add("LeaveQuota", leaveBalanceNew.LeaveQuota);
                //parameters.Add("BringLeave", leaveBalanceNew.BringLeave);
                parameters.Add("AdjustDay", leaveBalanceNew.AdjustDay);
                parameters.Add("Remark", leaveBalanceNew.Remark);
                parameters.Add("ModifiedBy", userCode);

                _leaveBalanceRepository.Execute($"USP_U_LeaveBalance", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Update LeaveBalance success.";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Update LeaveBalance failed: " + ex.Message;
            }
            return result;
        }

        public GenericResult DeleteLeaveBalance(LeaveBalanceModel leaveBalance)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", leaveBalance.Id);
                parameters.Add("UserName", leaveBalance.UserName);

                _leaveBalanceRepository.Execute($"USP_D_LeaveBalance", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Delete LeaveBalance success.";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Delete LeaveBalance failed: " + ex.Message;
            }
            return result;
        }

        public GenericResult ImportLeaveBalance(string userCode, DataTable leaveBalanceData)
        {
            GenericResult result = new GenericResult();

            try
            {
                for (int i = 0; i < leaveBalanceData.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(leaveBalanceData.Rows[i]["TypeId"].ToString()))
                    {
                        leaveBalanceData.Rows[i]["TypeId"] = 1;
                    }

                    if (string.IsNullOrEmpty(leaveBalanceData.Rows[i]["LeaveQuota"].ToString()))
                    {
                        leaveBalanceData.Rows[i]["LeaveQuota"] = 0;
                    }

                    //if (string.IsNullOrEmpty(leaveBalanceData.Rows[i]["BringLeave"].ToString()))
                    //{
                    //    leaveBalanceData.Rows[i]["BringLeave"] = 0;
                    //}
                }

                var leaveBalances = leaveBalanceData.ToListOf<LeaveBalanceModel>();

                foreach (LeaveBalanceModel leaveBalance in leaveBalances)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("Year", leaveBalance.Year);
                    parameters.Add("UserName", leaveBalance.UserName.Trim());

                    var check = _leaveBalanceRepository.GetAll($"USP_S_LeaveBalance", parameters, commandType: CommandType.StoredProcedure);

                    parameters.Add("FullName", leaveBalance.FullName.Trim());
                    parameters.Add("TypeId", leaveBalance.TypeId);
                    parameters.Add("LeaveQuota", leaveBalance.LeaveQuota);
                    //parameters.Add("BringLeave", leaveBalance.BringLeave);
                    parameters.Add("AdjustDay", leaveBalance.AdjustDay);
                    parameters.Add("Remark", leaveBalance.Remark);

                    if (check.Any())
                    {
                        parameters.Add("Id", check.FirstOrDefault().Id);
                        parameters.Add("ModifiedBy", userCode);

                        _leaveBalanceRepository.Execute($"USP_U_LeaveBalance", parameters, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        parameters.Add("CreatedBy", userCode);

                        _leaveBalanceRepository.Execute($"USP_I_LeaveBalance", parameters, commandType: CommandType.StoredProcedure);
                    }
                }

                result.Success = true;
                result.Message = "Import LeaveBalance success.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Import LeaveBalance failed: " + ex.Message;
            }
            return result;
        }
    }
}

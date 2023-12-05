using Dapper;
using Org.BouncyCastle.Asn1.Crmf;
using StackExchange.Redis;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TDI.Application.Implements
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly IGenericRepository<LeaveTypeModel> _leaveTypeRepository;
        private readonly IGenericRepository<ContractModel> _contractRepository;

        public LeaveTypeService(IGenericRepository<LeaveTypeModel> leaveTypeRepository, IGenericRepository<ContractModel> contractRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _contractRepository = contractRepository;
        }

        public async Task<GenericResult> GetAllLeaveType(int id)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                if (id > 0)
                {
                    parameters.Add("Id", id);
                }
                else
                {
                    parameters.Add("Id", null);
                }

                var data = await _leaveTypeRepository.GetAllAsync($"USP_S_LeaveType", parameters, commandType: CommandType.StoredProcedure);
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

        public GenericResult InsertLeaveType(string userCode, LeaveTypeModel leaveType)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Name", leaveType.Name);
                parameters.Add("Description", leaveType.Description);
                parameters.Add("Active", leaveType.Active);
                parameters.Add("CarryForward", leaveType.CarryForward);
                parameters.Add("MaxDay", leaveType.MaxDay);
                parameters.Add("CreatedBy", userCode);
                parameters.Add("CountryId", leaveType.CountryId);
                parameters.Add("ProjectCode", leaveType.ProjectCode);
                parameters.Add("WBSCode", leaveType.WBSCode);
                parameters.Add("ExcludeDash", leaveType.ExcludeDash);

                _leaveTypeRepository.Execute($"USP_I_LeaveType", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Insert LeaveType success.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Insert LeaveType failed: " + ex.Message;
            }
            return result;
        }

        public GenericResult UpdateLeaveType(string userCode, LeaveTypeModel leaveTypeOld, LeaveTypeModel leaveTypeNew)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", leaveTypeOld.Id);
                parameters.Add("Name", leaveTypeNew.Name);
                parameters.Add("Description", leaveTypeNew.Description);
                parameters.Add("Active", leaveTypeNew.Active);
                parameters.Add("CarryForward", leaveTypeNew.CarryForward);
                parameters.Add("MaxDay", leaveTypeNew.MaxDay);
                parameters.Add("CreatedBy", userCode);
                parameters.Add("CountryId", leaveTypeNew.CountryId);
                parameters.Add("ProjectCode", leaveTypeNew.ProjectCode);
                parameters.Add("WBSCode", leaveTypeNew.WBSCode);
                parameters.Add("ExcludeDash", leaveTypeNew.ExcludeDash);

                _leaveTypeRepository.Execute($"USP_U_LeaveType", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Update LeaveType success.";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Update LeaveType failed: " + ex.Message;
            }
            return result;
        }

        public GenericResult DeleteLeaveType(LeaveTypeModel leaveType)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", leaveType.Id);

                _leaveTypeRepository.Execute($"USP_D_LeaveType", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Delete LeaveType success.";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Delete LeaveType failed: " + ex.Message;
            }
            return result;
        }
        
        public async Task<GenericResult> GetAllLeaveTypeByUser(string userCode)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", userCode);

                var data = await _leaveTypeRepository.GetAllAsync($"USP_S_LeaveTypeByUser", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<GenericResult> GetContractData(string contractCode)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ContractCode", contractCode);

                var data = await _contractRepository.GetAllAsync($"USP_S_Contract", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<GenericResult> GetLeaveTypeList(string UserName)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserName", UserName);

                var data = await _leaveTypeRepository.GetAllAsync($"USP_S_LeaveTypeList", parameters, commandType: CommandType.StoredProcedure);
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
    }
}

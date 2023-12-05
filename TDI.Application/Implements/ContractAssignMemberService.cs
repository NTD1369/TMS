using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;
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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace TDI.Application.Implements
{
    public class ContractAssignMemberService : IContractAssignMemberService
    {
        private readonly IGenericRepository<ContractAssignMemberModel> _contractRespository;
        private readonly IMapper _mapper;

        public ContractAssignMemberService(IGenericRepository<ContractAssignMemberModel> contractRespository, IMapper mapper)
        {
            _contractRespository = contractRespository;
            _mapper = mapper;

        }

        public async Task<GenericResult> GetContractById(string ContractCode, string ContractLineId, int LineId)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ContractCode", ContractCode);
                parameters.Add("ContractLineId", ContractLineId);
                parameters.Add("LineId", LineId);

                var data = await _contractRespository.GetAsync($"USP_S_ContractAssignMemberByID", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data = data as ContractAssignMemberModel;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> GetContractAssign(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            GenericResult resulGetWBS = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode",UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                var data = _contractRespository.GetAll($"USP_S_ContractAssignMember", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as List<ContractAssignMemberModel>;
            }
            catch (Exception ex)
            {
                resulGetWBS.Success = false;
                resulGetWBS.Message = ex.Message;
            }
            return resulGetWBS;
        }
        public async Task<GenericResult> Create(ContractAssignMemberModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                //   
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", model.UserCode);
                parameters.Add("ContractCode", model.ContractCode);
                parameters.Add("ContractLineId", model.ContractLineId);
                parameters.Add("LineId", model.LineId);
                parameters.Add("EmployeeId", model.EmployeeId);
                parameters.Add("Description", model.Description);
                parameters.Add("StartDate", model.StartDate);
                parameters.Add("EndDate", model.EndDate);
                parameters.Add("mandays", model.Mandays);
                parameters.Add("mandaysUpdate", model.MandaysUpdate);
                //parameters.Add("CreateBy",model.UserCode);
                //parameters.Add("CreateOn", DateTime.Now);
                //parameters.Add("@ModifiedBy", "");
                //parameters.Add("@ModifiedOn", DateTime.Now);
                var affectedRows = _contractRespository.Insert("USP_I_ContractAssignMember", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }




        public async Task<GenericResult> Update(ContractAssignMemberModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                //   
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", model.UserCode);
                parameters.Add("ContractCode", model.ContractCode);
                parameters.Add("ContractLineId", model.ContractLineId);
                parameters.Add("LineId", model.LineId);
                parameters.Add("EmployeeId", model.EmployeeId);
                parameters.Add("Description", model.Description);
                parameters.Add("StartDate", model.StartDate);
                parameters.Add("EndDate", model.EndDate);
                parameters.Add("mandays", model.Mandays);
                parameters.Add("mandaysUpdate", model.MandaysUpdate);
                //parameters.Add("CreateBy",model.UserCode);
                //parameters.Add("CreateOn", DateTime.Now);
                //parameters.Add("@ModifiedBy", "");
                //parameters.Add("@ModifiedOn", DateTime.Now);
                var affectedRows = _contractRespository.Update("USP_I_ContractAssignMember", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }


        public async Task<GenericResult> WBSUpdateDateForAllMember(string ContractCode,DateTime StartDate, DateTime Endate)
        {
            GenericResult result = new GenericResult();
            try
            {
                //   
                var parameters = new DynamicParameters();
                parameters.Add("ContractCode", ContractCode);
                parameters.Add("StartDate", StartDate);
                parameters.Add("Endate", Endate);
               
                var affectedRows = _contractRespository.Update("USP_U_WBSUpdateDateForAllMember", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> Delete(string ContractCode, string contractLineId, int LineId)
        {

            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ContractCode", ContractCode);
                parameters.Add("contractLineId", contractLineId);
                parameters.Add("LineId", LineId);


                _contractRespository.Execute("USP_D_ContractAssignMember", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }


        public async Task<GenericResult> GetContractByContractLineId(string ContractCode, string ContractLineId)
        {
            GenericResult resulGetWBS = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("ContractCode", ContractCode);
                parameter.Add("ContractLineId", ContractLineId);

                var data = _contractRespository.GetAll($"USP_S_ContractAssignMemberByContractLineId", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as List<ContractAssignMemberModel>;
            }
            catch (Exception ex)
            {
                resulGetWBS.Success = false;
                resulGetWBS.Message = ex.Message;
            }
            return resulGetWBS;
        }

         

    }
}

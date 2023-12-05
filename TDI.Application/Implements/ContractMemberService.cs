using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Data.Repositories;
using TDI.Utilities.Dtos;

namespace TDI.Application.Implements
{
    public class ContractMemberService : IContractMemberService
    {
        private readonly IGenericRepository<ContractMemberModel> _contractMemberRepository;
        private readonly IGenericRepository<SystemOptionModel> _systemOptionMemberRepository;

        private readonly IMapper _mapper;
        public ContractMemberService(IGenericRepository<ContractMemberModel> contractMemberRepository, IGenericRepository<SystemOptionModel> systemOptionMemberRepository, IMapper mapper)
        {
            _contractMemberRepository = contractMemberRepository;
            _systemOptionMemberRepository = systemOptionMemberRepository;

            _mapper = mapper;
        }


        public async Task<GenericResult> GetSytemOptionByPosition()
        {
            GenericResult resulGetContractMember = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
              
                var data = _systemOptionMemberRepository.GetAll($"USP_S_SystemOption", parameter, commandType: CommandType.StoredProcedure);
                resulGetContractMember.Success = true;
                resulGetContractMember.Data = data as List<SystemOptionModel>;
            }
            catch (Exception ex)
            {
                resulGetContractMember.Success = false;
                resulGetContractMember.Message = ex.Message;
            }
            return resulGetContractMember;
        }


        public async Task<GenericResult> Create(ContractMemberModel model)
        {
            GenericResult result = new GenericResult();
            try
            {   
        
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", model.UserCode);
                parameters.Add("ContractCode", model.ContractCode); 
                parameters.Add("ContractMemberID", model.ContractMemberID);

                parameters.Add("EmployeeId", model.EmployeeId);
                parameters.Add("Position", model.Position);
                parameters.Add("StartDate", model.StartDate);
                parameters.Add("EndDate", model.EndDate);
                parameters.Add("Status", model.Status);
                //parameters.Add("CreateBy", model.UserCode);
                //parameters.Add("CreateOn", DateTime.Now);
                //parameters.Add("@ModifiedBy", "");
                //parameters.Add("@ModifiedOn", DateTime.Now);
                var affectedRows = _contractMemberRepository.Insert("USP_I_ContractMember", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }


        public async Task<GenericResult> Delete(string ContractCode, int ContractMemberId)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ContractCode", ContractCode);
                parameters.Add("ContractMemberId", ContractMemberId);
                _contractMemberRepository.Execute("USP_D_ContractMember", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<GenericResult> GetContractMember(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            GenericResult resulGetContractMember = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                var data = _contractMemberRepository.GetAll($"USP_S_ContractMember", parameter, commandType: CommandType.StoredProcedure);
                resulGetContractMember.Success = true;
                resulGetContractMember.Data = data as List<ContractMemberModel>;
            }
            catch (Exception ex)
            {
                resulGetContractMember.Success = false;
                resulGetContractMember.Message = ex.Message;
            }
            return resulGetContractMember;
        }

        public async Task<GenericResult> GetContractMemberById(string ContractCode,int ContractMemberId)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ContractCode", ContractCode);
                parameters.Add("ContractMemberId", ContractMemberId);

                var data = await _contractMemberRepository.GetAsync($"USP_S_ContractMemberByID", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data = data as ContractMemberModel;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> Update(ContractMemberModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                //   
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", model.UserCode);
                parameters.Add("ContractCode", model.ContractCode);
                parameters.Add("ContractMemberID", model.ContractMemberID);
                parameters.Add("EmployeeId", model.EmployeeId);
                parameters.Add("Position", model.Position);
                parameters.Add("StartDate", model.StartDate);
                parameters.Add("EndDate", model.EndDate);
                parameters.Add("Status", model.Status);
                var affectedRows = _contractMemberRepository.Insert("USP_I_ContractMember", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
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

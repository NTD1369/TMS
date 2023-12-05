using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata;
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
    public class ContractLineService : IContractLineService
    {
        private readonly IGenericRepository<WBSHeaderModel> _wbsRespository;
        private readonly IGenericRepository<ContractLineModel> _contractLineRepository;

        private readonly IMapper _mapper;

        public ContractLineService(IGenericRepository<WBSHeaderModel> wbsRespository, IGenericRepository<ContractLineModel> contractLineRespository, IMapper mapper)
        {
            _wbsRespository = wbsRespository;
            _contractLineRepository = contractLineRespository;
            _mapper = mapper;

        }
        public async Task<GenericResult> GetById(int ContractLineId)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ContractLineId", ContractLineId);

                var data = await _wbsRespository.GetAsync($"USP_S_ContractId", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data = data as WBSHeaderModel;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> GetContractLineByContractCode(string ContractCode)
        {
            GenericResult resulGetWBS = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("ContractCode", ContractCode);
                var data = _contractLineRepository.GetAll($"USP_S_ContractLineByContractCode", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as List<ContractLineModel>;
            }
            catch (Exception ex)
            {
                resulGetWBS.Success = false;
                resulGetWBS.Message = ex.Message;
            }
            return resulGetWBS;
        }
        public async Task<GenericResult> GetContractLine(string UserCode, DateTime FromDate, DateTime ToDate, string ContractCode)
        {
            GenericResult resulGetWBS = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", null);
                parameter.Add("ToDate", null);
                parameter.Add("ContractCode", ContractCode);
                var data = _contractLineRepository.GetAll($"USP_S_ContractLine", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as List<ContractLineModel>;
            }
            catch (Exception ex)
            {
                resulGetWBS.Success = false;
                resulGetWBS.Message = ex.Message;
            }
            return resulGetWBS;
        }
        public async Task<GenericResult> GetWBSHeader(string UserCode, DateTime FromDate, DateTime ToDate,string ContractCode)
        {
            GenericResult resulGetWBS = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                parameter.Add("ContractCode", ContractCode);

                var data = _wbsRespository.GetAll($"USP_S_ContractLine", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as List<WBSHeaderModel>;
            }
            catch (Exception ex)
            {
                resulGetWBS.Success = false;
                resulGetWBS.Message = ex.Message;
            }
            return resulGetWBS;
        }
        public async Task<GenericResult> Create(WBSHeaderModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                //   
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", model.UserCode); 
                parameters.Add("ContractCode", model.PrjCode);
                parameters.Add("ContractLineId", model.ContractLineId);
                parameters.Add("LineCode", model.LineCode);
                parameters.Add("Description", model.Description);
                parameters.Add("StartDate", model.StartDate);
                parameters.Add("EndDate", model.EndDate);
                //parameters.Add("PM", model.PM);
                parameters.Add("Mandays", model.Mandays); 
                parameters.Add("MandaysUpdate", model.MandaysUpdate); 
                parameters.Add("Bill", model.Bill);
                parameters.Add("Status", model.Status);
                parameters.Add("Additional", model.Additional);

                //parameters.Add("CreateBy",model.UserCode);
                //parameters.Add("CreateOn", DateTime.Now);
                //parameters.Add("@ModifiedBy", "");
                //parameters.Add("@ModifiedOn", DateTime.Now);
                var affectedRows = _wbsRespository.Insert("USP_I_ContractLine", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        } 

       
      

        public async Task<GenericResult> Update(WBSHeaderModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                //   
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", model.UserCode);
                parameters.Add("ContractCode", model.ContractCode);
                parameters.Add("ContractLineId", model.ContractLineId);
                parameters.Add("LineCode", model.LineCode);
                parameters.Add("Description", model.Description);
                parameters.Add("StartDate", model.StartDate);
                parameters.Add("EndDate", model.EndDate);
                //parameters.Add("PM", model.PM);
                parameters.Add("Mandays", model.Mandays);
                parameters.Add("MandaysUpdate", model.MandaysUpdate);
                parameters.Add("Bill", model.Bill);
                parameters.Add("Status", model.Status);
                parameters.Add("Additional", model.Additional);

                //parameters.Add("CreateBy",model.UserCode);
                //parameters.Add("CreateOn", DateTime.Now);
                //parameters.Add("@ModifiedBy", "");
                //parameters.Add("@ModifiedOn", DateTime.Now);
                var affectedRows = _wbsRespository.Update("USP_I_ContractLine", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        } 
        
        public async Task<GenericResult> WBSUpdateDateForAllMember(WBSHeaderModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                //   
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", model.UserCode);
                parameters.Add("ContractCode", model.ContractCode);
                parameters.Add("ContractLineId", model.ContractLineId);
                parameters.Add("LineCode", model.LineCode);
                parameters.Add("Description", model.Description);
                parameters.Add("StartDate", model.StartDate);
                parameters.Add("EndDate", model.EndDate);
                //parameters.Add("PM", model.PM);
                parameters.Add("Mandays", model.Mandays);
                parameters.Add("MandaysUpdate", model.MandaysUpdate);
                parameters.Add("Bill", model.Bill);
                parameters.Add("Status", model.Status);
                parameters.Add("Additional", model.Additional);

                //parameters.Add("CreateBy",model.UserCode);
                //parameters.Add("CreateOn", DateTime.Now);
                //parameters.Add("@ModifiedBy", "");
                //parameters.Add("@ModifiedOn", DateTime.Now);
                var affectedRows = _wbsRespository.Update("USP_U_WBSUpdateDateForAllMember", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> Delete(int ContractLineId)
        {

            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ContractLineId", ContractLineId);

                _wbsRespository.Execute("USP_D_ContractLine", parameters, commandType: CommandType.StoredProcedure);
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

    }

}

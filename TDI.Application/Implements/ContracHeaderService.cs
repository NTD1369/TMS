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
    public class ContracHeaderService : IContractHeaderService
    {
        private readonly IGenericRepository<ContractHeaderModel> _contractRespository;
        private readonly IGenericRepository<ctrModel> _ctrRespository;

        private readonly IMapper _mapper;

        public ContracHeaderService(IGenericRepository<ContractHeaderModel> contractRespository, IGenericRepository<ctrModel> ctrRespository, IMapper mapper)
        {
            _contractRespository = contractRespository;
            _ctrRespository = ctrRespository;

            _mapper = mapper;

        }

      
        public async Task<GenericResult> GetContractHeader(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            GenericResult resulGetWBS = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate); 
                parameter.Add("Status", "ALL");
                var data = _contractRespository.GetAll($"USP_S_ContractHeader", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as List<ContractHeaderModel>;
            }
            catch (Exception ex)
            {
                resulGetWBS.Success = false;
                resulGetWBS.Message = ex.Message;
            }
            return resulGetWBS;
        }

        public async Task<GenericResult> GetContractHeader2(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            GenericResult resulGetWBS = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                parameter.Add("Status", "ALL");

                var data = _contractRespository.GetAll($"USP_S_ContractHeader", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as List<ContractHeaderModel>;
            }
            catch (Exception ex)
            {
                resulGetWBS.Success = false;
                resulGetWBS.Message = ex.Message;
            }
            return resulGetWBS;
        }

        public async Task<GenericResult> GetContractHeader3ByID(string UserCode,string ContractCode)
        {
            GenericResult resulGetWBS = new GenericResult();
            try           
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("ContractCode", ContractCode); 
                var data = _contractRespository.Get($"USP_S_ContractHeaderByID", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as ContractHeaderModel;
            }
            catch (Exception ex)
            {
                resulGetWBS.Success = false;
                resulGetWBS.Message = ex.Message;
            }
            return resulGetWBS;
        }

        public async Task<GenericResult> Delete(string UserCode, string ContractCode)
        {
            GenericResult resulGetWBS = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("ContractCode", ContractCode);
                var data = _contractRespository.GetAll($"USP_D_ContractHeader", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as List<ContractHeaderModel>;
            }
            catch (Exception ex)
            {
                resulGetWBS.Success = false;
                resulGetWBS.Message = ex.Message;
            }
            return resulGetWBS;
        }

        public async Task<GenericResult> Create(ContractHeaderModel model, string UserCode)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", UserCode);
                parameters.Add("ContractCode", model.ContractCode); //
                parameters.Add("ContractName", model.ContractName);
                parameters.Add("ProjectCode", model.ProjectCode);
                parameters.Add("Description", model.Description);
                parameters.Add("Status", model.Status);

                parameters.Add("Startdate", model.StartDate);
                parameters.Add("Enddate", model.EndDate);
                parameters.Add("Mandays", model.Mandays);
                parameters.Add("MandaysUpdate", model.MandaysUpdate);
                parameters.Add("PM", model.PM); 
                  

                var affectedRows = _contractRespository.Insert("USP_I_ContractHeader", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                //result.Message = key;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> Update(ContractHeaderModel model, string UserCode)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", UserCode);
                parameters.Add("ContractCode", model.ContractCode);
                parameters.Add("ContractName", model.ContractName);
                parameters.Add("ProjectCode", model.ProjectCode);
                parameters.Add("Description", model.Description);
                parameters.Add("Status", model.Status);

                parameters.Add("Startdate", model.StartDate);
                parameters.Add("Enddate", model.EndDate);
                parameters.Add("Mandays", model.Mandays);
                parameters.Add("MandaysUpdate", model.MandaysUpdate);
                parameters.Add("PM", model.PM);


                var affectedRows = _contractRespository.Insert("USP_I_ContractHeader", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                //result.Message = key;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }



        public async Task<GenericResult> GetContractLineContractCode(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            GenericResult resulGetWBS = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                parameter.Add("Status", "A");
                //sp_ContractList

                var data = _ctrRespository.GetAll($"USP_S_ContractHeader", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as List<ctrModel>;
            }
            catch (Exception ex)
            {
                resulGetWBS.Success = false;
                resulGetWBS.Message = ex.Message;
            }
            return resulGetWBS;
        }

        public async Task<GenericResult> GetContractLineContractCode_byUserWBS(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            GenericResult resulGetWBS = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                //sp_ContractList

                var data = _ctrRespository.GetAll($"USP_S_ContractHeader_byUserWBS", parameter, commandType: CommandType.StoredProcedure);
                resulGetWBS.Success = true;
                resulGetWBS.Data = data as List<ctrModel>;
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

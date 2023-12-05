using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;
using TDITimeSheet.Pages;

namespace TDITimeSheet.Data
{
    public class ContractHeaderController
    {

        IContractHeaderService _contractService;

        public ContractHeaderController(IContractHeaderService contractService)
        {
            _contractService = contractService;
        }
        
        public async Task<GenericResult> GetContractHeader(string UserCode,DateTime FromDate, DateTime ToDate)
        {
            var result = await _contractService.GetContractHeader(UserCode, FromDate, ToDate);
            return result;
        }
        public async Task<GenericResult> GetContractLineContractCode(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            var result = await _contractService.GetContractLineContractCode(UserCode, FromDate, ToDate);
            return result;
        }

        public async Task<GenericResult> GetContractLineContractCode_byUserWBS(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            var result = await _contractService.GetContractLineContractCode_byUserWBS(UserCode, FromDate, ToDate);
            return result;
        }

        public async Task<GenericResult> GetContractHeader2(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            var result = await _contractService.GetContractHeader2(UserCode, FromDate, ToDate);
            return result;
        }
       
         public async Task<GenericResult> GetContractHeader3ByID(string UserCode,string ContractCode)
        {
            var result = await _contractService.GetContractHeader3ByID(UserCode, ContractCode );
            return result;
        }



        public async Task<GenericResult> Create(ContractHeaderModel  model, string UserCode)
        {
            var result = await _contractService.Create(model,   UserCode);
            return result;
        }

        public async Task<GenericResult> Update(ContractHeaderModel model, string UserCode)
        {
            var result = await _contractService.Update(model, UserCode);
            return result;
        }

        public async Task<GenericResult> Delete(string UserCode, string ContractCode)
        {
            var result = await _contractService.Delete ( UserCode, ContractCode );
            return result;
        }

    }
}

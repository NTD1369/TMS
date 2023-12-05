using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;
using TDITimeSheet.Pages;

namespace TDITimeSheet.Data
{
    public class ContractLineController
    {

        IContractLineService _wbsService;

        public ContractLineController(IContractLineService wbsService)
        {
            _wbsService = wbsService;
        }
        public async Task<GenericResult> GetById(int ContractLineId)
        {
            var result = await _wbsService.GetById(ContractLineId);
            return result;
        }
        public async Task<GenericResult> GetWBSHeader(string UserCode, DateTime? FromDate, DateTime? ToDate, string ContractCode)
        {
            var result = await _wbsService.GetWBSHeader(UserCode, FromDate.Value, ToDate.Value, ContractCode);
            return result;
        }
        public async Task<GenericResult> GetContractLine(string UserCode, DateTime? FromDate, DateTime? ToDate, string ContractCode)
        {
            var result = await _wbsService.GetContractLine(UserCode, FromDate.Value, ToDate.Value,ContractCode);
            return result;
        } 
        public async Task<GenericResult> GetContractLineByContractCode(string ContractCode)
        {
            var result = await _wbsService.GetContractLineByContractCode(ContractCode);
            return result;
        }
        public async Task<GenericResult> Create(WBSHeaderModel model)
        {
            var result = await _wbsService.Create(model);
            return result;
        }

        public async Task<GenericResult> Update(WBSHeaderModel model)
        {
            var result = await _wbsService.Update(model);
            return result;
        }  
        public async Task<GenericResult> WBSUpdateDateForAllMember(WBSHeaderModel model)
        {
            var result = await _wbsService.WBSUpdateDateForAllMember(model);
            return result;
        }
        public async Task<GenericResult> Delete(int ContractLineId)
        {
            var result = await _wbsService.Delete(ContractLineId);
            return result;
        }
    }
}

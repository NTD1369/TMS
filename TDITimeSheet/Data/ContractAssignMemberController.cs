using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;
using TDITimeSheet.Pages;

namespace TDITimeSheet.Data
{
    public class ContractAssignMemberController
    {

        IContractAssignMemberService _contractAssign;


        public ContractAssignMemberController(IContractAssignMemberService contractAssignService)
        {
            _contractAssign = contractAssignService;
        }
        public async Task<GenericResult> GetContractById(string ContractCode,string ContractLineId, int LineId)
        {
            var result = await _contractAssign.GetContractById(ContractCode,ContractLineId, LineId);
            return result;
        }
        public async Task<GenericResult> GetContractByContractLineId(string ContractCode, string ContractLineId)
        {
            var result = await _contractAssign.GetContractByContractLineId(ContractCode, ContractLineId);
            return result;
        }

        public async Task<GenericResult> GetContractAssign(string UserCode,DateTime FromDate, DateTime ToDate)
        {
            var result = await _contractAssign.GetContractAssign(UserCode, FromDate, ToDate);
            return result;
        }

        public async Task<GenericResult> Create(ContractAssignMemberModel model)
        {

            var result = await _contractAssign.Create(model);
            return result;
        }

        public async Task<GenericResult> Update(ContractAssignMemberModel model)
        {
            var result = await _contractAssign.Update(model);
            return result;
        }
        public async Task<GenericResult> Delete(string ContractCode, string contractLineId, int LineId)
        {
            var result = await _contractAssign.Delete(ContractCode, contractLineId, LineId);
            return result;
        }

    }
}

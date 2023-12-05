using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class ContractMemberController
    {
        IContractMemberService _contractMemberService;

        public ContractMemberController(IContractMemberService contractMemberService)
        {
            _contractMemberService = contractMemberService;
        }
        public async Task<GenericResult> GetSytemOptionByPosition()
        {
            var result = await _contractMemberService.GetSytemOptionByPosition();
            return result;
        }
        public async Task<GenericResult> GetContractMemberById(string ContractCode, int ContractMemberId)
        {
            var result = await _contractMemberService.GetContractMemberById(ContractCode, ContractMemberId);
            return result;
        }
        public async Task<GenericResult> GetContractMember(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            var result = await _contractMemberService.GetContractMember(UserCode, FromDate, ToDate);
            return result;
        }
        public async Task<GenericResult> Create(ContractMemberModel model)
        {

            var result = await _contractMemberService.Create(model);
            return result;
        }

        public async Task<GenericResult> Update(ContractMemberModel model)
        {
            var result = await _contractMemberService.Update(model);
            return result;
        }
        public async Task<GenericResult> Delete(string ContractCode, int ContractMemberId)
        {
            var result = await _contractMemberService.Delete(ContractCode, ContractMemberId);
            return result;
        }
    }
}

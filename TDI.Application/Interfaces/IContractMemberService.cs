using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IContractMemberService
    {
        Task<GenericResult> GetContractMember(string UserCode,DateTime FromDate,DateTime ToDate);
        Task<GenericResult> GetContractMemberById(string ContractCode,int ContractMemberId);
        Task<GenericResult> GetSytemOptionByPosition();
        Task<GenericResult> Create(ContractMemberModel model);
        Task<GenericResult> Update(ContractMemberModel model);
        Task<GenericResult> Delete(string ContractCode, int ContractMemberId);

    }
}

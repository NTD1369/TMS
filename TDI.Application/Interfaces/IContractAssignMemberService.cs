using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IContractAssignMemberService
    {
        Task<GenericResult> GetContractById(string ContractCode, string ContractLineId, int LineId);
        Task<GenericResult> GetContractByContractLineId(string ContractCode, string ContractLineId);
        Task<GenericResult> GetContractAssign(string UserCode, DateTime FromDate, DateTime ToDate);
        Task<GenericResult> Create(ContractAssignMemberModel model);
        Task<GenericResult> Update(ContractAssignMemberModel model);
        Task<GenericResult> Delete(string ContractCode, string contractLineId, int LineId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IContractLineService
    {

        //Task<GenericResult> GetProject();
        Task<GenericResult> GetById(int ContractLineId);
        Task<GenericResult> GetWBSHeader(string UserCode, DateTime FromDate, DateTime ToDate, string ContractCode);
        Task<GenericResult> GetContractLine(string UserCode, DateTime FromDate, DateTime ToDate, string contractCode);
        Task<GenericResult> GetContractLineByContractCode(string ContractCode);
        Task<GenericResult> Create(WBSHeaderModel model);
        Task<GenericResult> Update(WBSHeaderModel model);
        Task<GenericResult> WBSUpdateDateForAllMember(WBSHeaderModel model);
        Task<GenericResult> Delete(int ContractLineId);
    }
}

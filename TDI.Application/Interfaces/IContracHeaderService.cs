using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IContractHeaderService
    {

        //Task<GenericResult> GetContractHeader();

        
        Task<GenericResult> GetContractHeader2(string UserCode, DateTime FromDate, DateTime ToDate);
        Task<GenericResult> GetContractHeader3ByID(string UserCode, string ContractCode);

        Task<GenericResult> Create(ContractHeaderModel model, string UserCode);
        Task<GenericResult> Update(ContractHeaderModel model, string UserCode); 
        Task<GenericResult> Delete(string UserCode, string ContractCode);


        Task<GenericResult> GetContractHeader(string UserCode,DateTime FromDate,DateTime ToDate);
        Task<GenericResult> GetContractLineContractCode(string UserCode,DateTime FromDate,DateTime ToDate);
        Task<GenericResult> GetContractLineContractCode_byUserWBS(string UserCode, DateTime FromDate, DateTime ToDate);


    }
}

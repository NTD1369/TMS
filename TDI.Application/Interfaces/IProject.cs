using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IProjectService
    {
        Task<GenericResult> GetProject();
        Task<GenericResult> Create(ProjectModel model);
        Task<GenericResult> Update(ProjectModel model);
        Task<GenericResult> Delete(int ProjectId);


        
        Task<GenericResult> sp_projectlist_byUser(string UserCode);
        Task<GenericResult> GetProject_ContractHeader_ForWBS(string UserCode);
    }
}

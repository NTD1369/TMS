using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IRolesService
    {
        Task<GenericResult> GetByRoleId(string id);
        Task<GenericResult> GetAll();
        Task<GenericResult> GetAllMfuction();
        
        Task<GenericResult> Create(RolesModel model);
        Task<GenericResult> Update(RolesModel model);
        Task<GenericResult> Delete(string id);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IPermissionService
    {

        Task<GenericResult> GetPermission(string RoleId);
        Task<GenericResult> GetRolePermission(string RoleId, string Permission, string FunctionId);
        Task<GenericResult> GetMPermission();
        Task<GenericResult> GetPermissionByRole(string roleId);
        Task<GenericResult> Insert(string RoleId, string Permission, string FunctionId, string Status);
        Task<GenericResult> Update(RolePermission model);
        Task<GenericResult> Delete(string RoleId, string Permission, string FunctionId);

    }
    
}

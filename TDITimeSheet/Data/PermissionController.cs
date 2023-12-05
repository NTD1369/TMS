using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class PermissionController
    {
        IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        
        public async Task<GenericResult> GetPermission(string RoleId)
        {
            var result = await _permissionService.GetPermission(RoleId);
            return result;
        }
        public async Task<GenericResult> GetMPermission()
        {
            var result = await _permissionService.GetMPermission();
            return result;
        }
        public async Task<GenericResult> GetPermissionByRole(string roleId)
        {
            var result = await _permissionService.GetPermissionByRole(roleId);
            return result;
        }public async Task<GenericResult> GetRolePermission(string RoleId, string Permission, string FunctionId)
        {
            var result = await _permissionService.GetRolePermission(RoleId, Permission, FunctionId);
            return result;
        }
        public async Task<GenericResult> Insert(string RoleId, string Permission, string FunctionId, string Status)
        {
            var result = await _permissionService.Insert(RoleId, Permission, FunctionId, Status);
            return result;
        } public async Task<GenericResult> Update(RolePermission model)
        {
            var result = await _permissionService.Update(model);
            return result;
        } public async Task<GenericResult> Delete(string RoleId, string Permission, string FunctionId)
        {
            var result = await _permissionService.Delete(RoleId, Permission, FunctionId);
            return result;
        }
       
    }
}

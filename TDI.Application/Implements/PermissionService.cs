using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Data.Repositories;
using TDI.Utilities.Dtos;

namespace TDI.Application.Implements
{
    public class PermissionService : IPermissionService
    {
        private readonly IGenericRepository<RolePermission> _rolePermissionRepository;
        private readonly IGenericRepository<MPermission> _mPermissionRepository;


        private readonly IMapper _mapper;


        public PermissionService(IGenericRepository<RolePermission> rolePermissionRepository, IGenericRepository<MPermission> mPermissionRepository, IMapper mapper)
        {
           
            _rolePermissionRepository = rolePermissionRepository;
            _mPermissionRepository = mPermissionRepository;

            _mapper = mapper;
        }
        public async Task<GenericResult> GetPermission(string RoleId)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("RoleId", RoleId);
               
                var data = _rolePermissionRepository.GetAll($"USP_S_RolePermissionByRoleId", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<RolePermission>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> GetPermissionByRole(string roleId)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("RoleId", roleId);
                var data = _rolePermissionRepository.GetAll($"USP_S_RolePermissionByRoleId", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<RolePermission>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> GetMPermission()
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
            
                var data = _mPermissionRepository.GetAll($"USP_S_MPermission", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<MPermission>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> GetRolePermission(string RoleId,string Permission, string FunctionId)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("RoleId", RoleId);
                parameter.Add("Permission", Permission);
                parameter.Add("FunctionId", FunctionId);

                var data = _rolePermissionRepository.Get($"USP_S_RolePermission", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as RolePermission;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        //update Permission
        public async Task<GenericResult> Insert(string RoleId, string Permission, string FunctionId,string Status)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("RoleId", RoleId);
                parameters.Add("Permission", Permission);
                parameters.Add("FunctionId", FunctionId);
                parameters.Add("Status", Status);

                var affectedRows = _rolePermissionRepository.Insert("USP_I_RolePermission", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                //result.Message = key;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> Update(RolePermission model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("RoleId", model.RoleId);
                parameters.Add("Permission", model.Permission);
                parameters.Add("FunctionId", model.FunctionId);
                parameters.Add("Status", model.Status);
                parameters.Add("@ModifiedBy", model.ModifiedBy);
                parameters.Add("@ModifiedOn", DateTime.Now);
                var affectedRows = _rolePermissionRepository.Update("USP_U_RolePermissionById", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                //result.Message = key;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> Delete(string RoleId, string Permission, string FunctionId)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("RoleId", RoleId);
                parameters.Add("Permission", Permission);
                parameters.Add("FunctionId", FunctionId);

                var affectedRows = _rolePermissionRepository.Execute("USP_D_RolePermission", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                //result.Message = key;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}

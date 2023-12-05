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
    public class RolesService : IRolesService
    {
        private readonly IGenericRepository<RolesModel> _rolesRepository;
        private readonly IGenericRepository<MFunctionModel> _mFunctionRepository;
        private readonly IGenericRepository<RolePermission> _rolePermissionRepository;


        private readonly IMapper _mapper;


        public RolesService(IGenericRepository<RolesModel> rolesRepository, IGenericRepository<MFunctionModel> mFunctionRepository,
            IGenericRepository<RolePermission> rolePermissionRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mFunctionRepository = mFunctionRepository;
            _rolePermissionRepository = rolePermissionRepository;

            _mapper = mapper;
        }
        public async Task<GenericResult> GetByRoleId(string Id)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("Id", Id);

                var data = _rolesRepository.Get($"USP_S_RolesById", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as RolesModel;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> GetAll()
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                var data = _rolesRepository.GetAll($"USP_S_Roles", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<RolesModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }

        //public async Task<GenericResult> GetPermissionByRole()
        //{
        //    GenericResult resulGetTimeEntry = new GenericResult();
        //    try
        //    {
        //        var parameter = new DynamicParameters();
        //        var data = _rolesRepository.GetAll($"USP_GetRolePermissionByRole", parameter, commandType: CommandType.StoredProcedure);
        //        resulGetTimeEntry.Success = true;
        //        resulGetTimeEntry.Data = data as List<RolesModel>;
        //    }
        //    catch (Exception ex)
        //    {
        //        resulGetTimeEntry.Success = false;
        //        resulGetTimeEntry.Message = ex.Message;
        //    }
        //    return resulGetTimeEntry;
        //}
        public async Task<GenericResult> GetAllMfuction()
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                var data = _mFunctionRepository.GetAll($"USP_S_Function", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<MFunctionModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> Create(RolesModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Name", model.Name);
                parameters.Add("NormalizedName", model.NormalizedName);
                parameters.Add("ConcurrencyStamp", model.ConcurrencyStamp);
                parameters.Add("Description", model.Description);
                parameters.Add("Status", model.Status);
                parameters.Add("@CreatedBy", model.Name);
                parameters.Add("CreatedOn", DateTime.Now);
                parameters.Add("@ModifiedBy", "");
                parameters.Add("@ModifiedOn", DateTime.Now);
                var affectedRows = _rolesRepository.Insert("USP_I_Roles", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> Update(RolesModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", model.Id);
                parameters.Add("Name", model.Name);
                parameters.Add("NormalizedName", model.NormalizedName);
                parameters.Add("ConcurrencyStamp", model.ConcurrencyStamp);
                parameters.Add("Description", model.Description);
                parameters.Add("Status", model.Status);
                parameters.Add("CreatedBy", model.Name);
                parameters.Add("CreatedOn", DateTime.Now);
                parameters.Add("@ModifiedBy", "");
                parameters.Add("@ModifiedOn", DateTime.Now);
                var affectedRows = _rolesRepository.Update("USP_U_Roles", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<GenericResult> Delete(string id)
        {

            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id);

                _rolesRepository.Execute("USP_D_Roles", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                return result;
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

using AutoMapper;
using Dapper;
using StackExchange.Redis;
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
    public class CommonService : ICommonService
    {
        private readonly IGenericRepository<MFunctionModel> _functionRespository;
        private readonly IGenericRepository<MPermission> _permissionRespository;
        private readonly IGenericRepository<RolePermission> _rolePermissionRespository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<UserModel> _userRespository;


        public CommonService(IGenericRepository<MFunctionModel> functionRespository, IGenericRepository<MPermission> permissionRespository,
            IGenericRepository<RolePermission> rolePermissionRespository, IMapper mapper, IGenericRepository<UserModel> userRespository)
        {
            _functionRespository = functionRespository;
            _permissionRespository = permissionRespository;
            _rolePermissionRespository = rolePermissionRespository;
            _mapper = mapper;
            _userRespository = userRespository;
        }
        public async Task<GenericResult> GetRolePermissionByRoleAndFunction(string Role, string Function)
        {
            GenericResult result = new GenericResult();
            try
            {


                //Lấy tất cả Function
                //var models = await _functionRespository.GetAllAsync($"USP_S_Function", null, commandType: CommandType.StoredProcedure);

                var parameters = new DynamicParameters();
                parameters.Add("Role", Role);
                //Lấy  PermissionRepository của Role 
                var permissionFunctionAll = await _rolePermissionRespository.GetAllAsync($"USP_GetRolePermissionByRole", parameters, commandType: CommandType.StoredProcedure);

                var listOfView = new List<MFunctionModel>();
                //foreach (var function in models)
                //{

                //}
                var permissionFunction = permissionFunctionAll.Where(x => x.FunctionId == Function).ToList();

                result.Success = true;
                result.Data = permissionFunction;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> GetFunctionByRole(string Role)
        {
            GenericResult result = new GenericResult();
            try
            {


                //Lấy tất cả Function
                var models = await _functionRespository.GetAllAsync($"USP_S_Function", null, commandType: CommandType.StoredProcedure);

                var parameters = new DynamicParameters();
                parameters.Add("Role", Role);
                //Lấy  PermissionRepository của Role 
                var permissionFunctionAll = await _rolePermissionRespository.GetAllAsync($"USP_GetRolePermissionByRole", parameters, commandType: CommandType.StoredProcedure);

                var listOfView = new List<MFunctionModel>();
                foreach (var function in models)
                {
                    var permissionFunction = permissionFunctionAll.Where(x => x.FunctionId == function.Id && x.Permission == "V").ToList();
                    if (permissionFunction != null && permissionFunction.Count > 0)
                    {
                        listOfView.Add(function);
                    }
                }
                var newList = AddChild(listOfView);


                result.Success = true;
                result.Data = newList;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public List<MFunctionModel> AddChild(List<MFunctionModel> functions)
        {
            List<MFunctionModel> result = new List<MFunctionModel>();
            //Lấy ra tất cả các Function có Father Id  = null
            var fatherList = functions.Where(x => string.IsNullOrEmpty(x.ParentId)).ToList();
            int fatherNum = 1;
            foreach (var father in fatherList)
            {

                //Lấy ra tất cả các Function có Father Id bằng function đang được lặp( father.id)
                var childList = functions.Where(x => x.ParentId == father.Id.ToString()).ToList();
                int childNum = 1;
                foreach (var child in childList)
                {
                    child.NumOfList = childNum;
                }
                father.Childrens = childList;
                father.NumOfList = fatherNum;
                fatherNum++;
            }
            result = fatherList;
            return result;
        }

        public async Task<List<UserModel>> GetUsersForMailLeaveAsync(string userName)
        {
            var users = new List<UserModel>();
            try
            {
                string query = $"SELECT U.UserName, U.Email, L.Email AS LeaderEmail\r\nFROM Users U\r\nLEFT JOIN TeamMember M ON U.UserName = M.UserName\r\nLEFT JOIN Team T ON M.TeamId = T.Id\r\nLEFT JOIN Users L ON T.Leader = L.UserName\r\nWHERE U.UserName = '{userName}'";

                var parameters = new DynamicParameters();
                users = await _userRespository.GetAllAsync(query, parameters, commandType: CommandType.Text);
            }
            catch { }

            return users;
        }
    }
}

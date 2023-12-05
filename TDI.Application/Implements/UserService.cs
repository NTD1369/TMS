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
using TDI.Utilities.Constants;
using TDI.Utilities.Dtos;
using TDI.Utilities.Helpers;

namespace TDI.Application.Implements
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<UserModel> _userRespository;
        private readonly IGenericRepository<ContractMemberModel> _memberRespository;

        private readonly IMapper _mapper;

        public UserService(IGenericRepository<UserModel> userRespository, IGenericRepository<ContractMemberModel> memberRespository, IMapper mapper)
        {
            _userRespository = userRespository;
            _memberRespository = memberRespository;
            _mapper = mapper;

        }
        
        public async Task<GenericResult> GetAllUser_List(string userName)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserName", userName);
                var data = _userRespository.GetAll($"USP_S_User_List", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<UserModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }

        public async Task<GenericResult> GetListUsersPM(string PM)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("PM", PM);
                var data = _userRespository.GetAll($"USP_S_ListUsersPM", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<UserModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> GetAllUser(string userName)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserName", userName);
                var data = _userRespository.GetAll($"USP_S_User", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<UserModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }

        public async Task<GenericResult> GetByMember(string ContractCode)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("ContractCode", ContractCode);
                var data = _memberRespository.GetAll($"USP_S_ContractMemberByEmployeeId", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<ContractMemberModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }

        public GenericResult InsertUser(string userCode, UserModel user)
        {
            GenericResult result = new GenericResult();
            try
            {


                var parameters = new DynamicParameters();
                parameters.Add("UserName", user.UserName);

                var dataCheck = _userRespository.GetAll("USP_S_User", parameters, commandType: CommandType.StoredProcedure);
                if (dataCheck != null && dataCheck.Count > 0)
                {
                    result.Success = false;
                    result.Message = "UserName already exists.";

                    return result;
                }

                //user.UserPassword = Encryptor.EncryptString(user.HashPassword, AppConstants.TEXT_PHRASE);
                

                parameters.Add("Email", user.Email);
                parameters.Add("PhoneNumber", user.PhoneNumber);
                parameters.Add("FullName", user.FullName);
                parameters.Add("BirthDay", user.BirthDay);
                parameters.Add("CreatedBy", userCode);
                parameters.Add("Status", user.Status);
                parameters.Add("UserPassword", user.UserPassword);
                parameters.Add("EmployeeId", user.EmployeeId);
                parameters.Add("CustomF1", user.CustomF1);
                parameters.Add("CustomF2", user.CustomF2);
                parameters.Add("CustomF3", user.CustomF3);
                parameters.Add("CustomF4", user.CustomF4);
                parameters.Add("UserType", user.UserType);
                parameters.Add("RoleId", user.RoleId);
                parameters.Add("CountryId", user.CountryId);
                parameters.Add("TeamId", user.TeamId);
                parameters.Add("LeaveCountry", user.LeaveCountry);

                _userRespository.Execute($"USP_I_User", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Insert User success.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Insert User failed: " + ex.Message;
            }
            return result;
        }

        public GenericResult UpdateUser(string userCode, UserModel userOld, UserModel userNew)
        {
            GenericResult result = new GenericResult();
            try
            {
                //userNew.UserPassword = Encryptor.EncryptString(userNew.HashPassword, AppConstants.TEXT_PHRASE);

                var parameters = new DynamicParameters();
                parameters.Add("UserId", userOld.Id);
                parameters.Add("UserName", userOld.UserName);
                parameters.Add("Email", userNew.Email);
                parameters.Add("PhoneNumber", userNew.PhoneNumber);
                parameters.Add("FullName", userNew.FullName);
                parameters.Add("BirthDay", userNew.BirthDay);
                parameters.Add("ModifiedBy", userCode);
                parameters.Add("Status", userNew.Status);
                parameters.Add("UserPassword", userNew.UserPassword);
                parameters.Add("EmployeeId", userNew.EmployeeId);
                parameters.Add("CustomF1", userNew.CustomF1);
                parameters.Add("CustomF2", userNew.CustomF2);
                parameters.Add("CustomF3", userNew.CustomF3);
                parameters.Add("CustomF4", userNew.CustomF4);
                parameters.Add("UserType", userNew.UserType);
                parameters.Add("RoleId", userNew.RoleId);
                parameters.Add("CountryId", userNew.CountryId);
                parameters.Add("TeamId", userNew.TeamId);
                parameters.Add("LeaveCountry", userNew.LeaveCountry);

                _userRespository.Execute($"USP_U_User", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Update User success.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Update User failed: " + ex.Message;
            }
            return result;
        }


        public async Task<GenericResult> ZZZ_GetAllUser_List(string userName)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserName", userName);
                var data = _userRespository.GetAll($"ZZZ_USP_S_User_List", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<UserModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }

    }
}

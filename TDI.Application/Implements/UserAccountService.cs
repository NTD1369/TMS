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
    public class UserAccountService : IUserAccountService
    {
        private readonly IGenericRepository<UserAccountModel> _userAccountService;
        private readonly IMapper _mapper;
      
        public UserAccountService(IGenericRepository<UserAccountModel> userAccountService, IMapper mapper)
        {
            _userAccountService = userAccountService;
            _mapper = mapper;

        }



        public async Task<GenericResult> GetUser(string userName, string passWord)
        {
            GenericResult resultGetUser = new GenericResult();
            try
            {
                 
                string hashPassword = Encryptor.EncryptString(passWord, AppConstants.TEXT_PHRASE);
                var parameter = new DynamicParameters();
                parameter.Add("UserName", userName);
                parameter.Add("passWord", hashPassword);
                var data = await _userAccountService.GetAsync($"USP_Login", parameter, commandType: CommandType.StoredProcedure);
                if(data == null)
                {
                    resultGetUser.Success = false;
                    resultGetUser.Message = "Invalid Username or Password !";
                    return resultGetUser;

                }
                else
                {
                    resultGetUser.Success = true;
                    resultGetUser.Data = data as UserAccountModel;
                    //GetAllAsync - List<UserAccountModel>
                }

            }
            catch (Exception ex)
            {
                resultGetUser.Success = false;
                resultGetUser = new GenericResult();
                resultGetUser.Message = ex.Message;
                resultGetUser.Error = ex.Message;
            }
            return resultGetUser;
        }
    }
}

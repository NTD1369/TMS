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
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly IGenericRepository<ChangePasswordModel> _changePasswordRepository;
        private readonly IMapper _mapper;

        public ChangePasswordService(IGenericRepository<ChangePasswordModel> changePasswordRepository, IMapper mapper)
        {
            _changePasswordRepository = changePasswordRepository;
            _mapper = mapper;
        }

        public async Task<GenericResult> Update(string userName, string password)
        {
            GenericResult result = new GenericResult();
            try
            {
                string newPass = TDI.Utilities.Helpers.Encryptor.EncryptString(password, TDI.Utilities.Constants.AppConstants.TEXT_PHRASE);

                var parameters = new DynamicParameters();
                parameters.Add("UserName", userName);
                parameters.Add("NewPassword", newPass);
                var affectedRows = _changePasswordRepository.Update("USP_U_ChangePassword", parameters, commandType: CommandType.StoredProcedure);
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

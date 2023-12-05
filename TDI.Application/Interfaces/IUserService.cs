using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IUserService
    {
        Task<GenericResult> GetAllUser(string userName);
        Task<GenericResult> GetByMember(string ContractCode);
        GenericResult InsertUser(string userCode, UserModel user);
        GenericResult UpdateUser(string userCode, UserModel userOld, UserModel userNew);


        Task<GenericResult> GetAllUser_List(string userName);
        Task<GenericResult> GetListUsersPM(string PM);
        Task<GenericResult> ZZZ_GetAllUser_List(string userName);

    }
}

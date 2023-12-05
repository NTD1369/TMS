using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface ICommonService
    {
        //Task FriSendMail();
        Task<GenericResult> GetFunctionByRole(string RoleId);
        //Task<GenericResult> LeaveSendEmail();

        Task<GenericResult> GetRolePermissionByRoleAndFunction(string Role, string Function);
        Task<List<UserModel>> GetUsersForMailLeaveAsync(string userName);
        //Task<GenericResult> testSendMailT6();
    }
}

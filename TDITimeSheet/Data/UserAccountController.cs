using Microsoft.AspNetCore.Mvc;
using TDI.Application.Interfaces;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class UserAccountController : Controller
    {
        IUserAccountService _userAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public async Task<GenericResult> GetUser(string userName, string passWord)
        {
            var resultGetUser = await _userAccountService.GetUser(userName, passWord);
            return resultGetUser;
        }
    }
}

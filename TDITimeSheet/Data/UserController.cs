using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class UserController
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<GenericResult> GetAllUser()
        {
            var result = await _userService.GetAllUser(string.Empty);
            return result;
        }
        public async Task<GenericResult> GetByMember(string ContractCode)
        {
            var result = await _userService.GetByMember(ContractCode);
            return result;
        }

        public async Task<GenericResult> GetAllUser_List(string UserName)
        {
            var result = await _userService.GetAllUser_List(UserName); // (string.Empty);
            return result;
        }
        public async Task<GenericResult> GetListUsersPM(string PM)
        {
            var result = await _userService.GetListUsersPM(PM); // (string.Empty);
            return result;
        }
    }
}

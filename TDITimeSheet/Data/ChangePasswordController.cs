using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Application.Interfaces.RPT;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class ChangePasswordController
    {
        IChangePasswordService _changePasswordService;


        public ChangePasswordController(IChangePasswordService changePasswordService)
        {
            _changePasswordService = changePasswordService;
        }
        public async Task<GenericResult> Update(string userName, string password)
        {
            var result = await _changePasswordService.Update(userName, password);
            return result;
        }
    }
}

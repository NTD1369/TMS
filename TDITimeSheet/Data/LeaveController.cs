using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class LeaveController
    {
        private ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            this._leaveService = leaveService;
        }

        public async Task<GenericResult> GetLeaveByUser(string userCode)
        {
            return await _leaveService.GetAllLeaveByUser(userCode);
        }

        public async Task<GenericResult> InsertLeaveAsync(LeaveModel leave)
        {
            return await _leaveService.InsertLeaveAsync(leave);
        }
       
    }
}

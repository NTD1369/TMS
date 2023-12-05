using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface ILeaveService
    {
        #region Leave Employee

        Task<GenericResult> GetAllLeaveByUser(string userCode);
        Task<GenericResult> InsertLeaveAsync(LeaveModel leave);
        Task<GenericResult> UpdateLeaveAsync(LeaveModel leaveOld, LeaveModel leaveNew);
        GenericResult DeleteLeaveAsync(LeaveModel leave);

        #endregion

        #region Leave Approval

        Task<GenericResult> GetAllLeaveForApproval(string userCode, DateTime fromDate, DateTime toDate);
        Task<GenericResult> UpdateLeaveApprovalAsync(LeaveModel leaveOld, LeaveModel leaveNew);
        Task<GenericResult> CreateFromLeave(LeaveModel leave);

        #endregion

        #region Leave Dashboard

        Task<GenericResult> GetLeaveDashboardByUser(string userName, int year);

        #endregion

        #region Reports
        Task<GenericResult> GetLeaveBalanceReport(string userCode, string userName, int year);

        Task<GenericResult> GetWhoLeaveOfDay();
        Task<GenericResult> GetWhoLeaveOfMonth();

        #endregion
    }
}

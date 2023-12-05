using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface ILeaveBalanceService
    {
        Task<GenericResult> GetAllLeaveBalance(int year);
        GenericResult InsertLeaveBalance(string userCode, LeaveBalanceModel leaveBalance); 
        GenericResult UpdateLeaveBalance(string userCode, LeaveBalanceModel leaveBalanceOld, LeaveBalanceModel leaveBalanceNew);
        GenericResult DeleteLeaveBalance(LeaveBalanceModel leaveBalance);
        GenericResult ImportLeaveBalance(string userCode, DataTable leaveBalanceData);
    }
}

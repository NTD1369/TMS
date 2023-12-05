using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<GenericResult> GetAllLeaveType(int id);
        GenericResult InsertLeaveType(string userCode, LeaveTypeModel leaveType);
        GenericResult UpdateLeaveType(string userCode, LeaveTypeModel leaveTypeOld, LeaveTypeModel leaveTypeNew);
        GenericResult DeleteLeaveType(LeaveTypeModel leaveType);
        Task<GenericResult> GetAllLeaveTypeByUser(string userCode);
        Task<GenericResult> GetContractData(string contractCode);
        Task<GenericResult> GetLeaveTypeList(string UserName);
    }
}

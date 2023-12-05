using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IHolidayService
    {
        Task<GenericResult> GetAll();
        Task<GenericResult> GetById(int Id);
        Task<GenericResult> Create(HolidayModel model);
        Task<GenericResult> Update(HolidayModel model);
        Task<GenericResult> Delete(int Holiday);
        Task<GenericResult> GetHolidayByUser(string userCode);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface ITestService
    {
        Task<GenericResult> GetTimeEntry(string UserCode, DateTime FromDate, DateTime ToDate);
        Task<GenericResult> GetTimeEntryWithoutId(string UserCode, DateTime Date, string PrjCode);
        Task<GenericResult> Create(TestModel model);
        Task<GenericResult> Update(TestModel model);
        Task<GenericResult> Delete(int TimeEntry);
       
    }
}

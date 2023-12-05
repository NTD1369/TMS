using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface ITimeEntryService
    {
        //Task<GenericResult> GetAllPrj();
        //Task<GenericResult> GetById(string id,DateTime ngay, string ngayTrongTuan);

        Task<GenericResult> GetTimeEntry(string UserCode, DateTime FromDate, DateTime ToDate, int Status);

        Task<GenericResult> GetTimeEntryWithoutId(string UserCode, DateTime Date, string PrjCode, string WBSName, string Comments, int Status);
        Task<GenericResult> Create(TimeEntryModel model);
        Task<GenericResult> Update(TimeEntryModel model);
        Task<GenericResult> Delete(int TimeEntry);


        
        Task<GenericResult> DeleteWeekLyTimeEntry(int Stt, string UserCode, string PrjCode, string ContractLineId, string Comments);
        Task<GenericResult> SaveDataWeekLyTimeEntry(int Stt, string UserCode, string PrjCode, string ContractLineId, DateTime Date, string CommentsDB,
             string NewComments, float Hour, string PrjName, string SAPB1DB)  ;


        Task<GenericResult> SubmitDataWeekLyTimeEntry(int Stt, string UserCode, string PrjCode, string ContractLineId, DateTime Date, string CommentsDB,
             string NewComments, float Hour, string PrjName, string SAPB1DB);
    }
}

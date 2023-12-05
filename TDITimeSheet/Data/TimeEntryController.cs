using System.Xml.Linq;
using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;
using TDITimeSheet.Pages;

namespace TDITimeSheet.Data
{
    public class TimeEntryController
    {
        ITimeEntryService _timeEntryService;

        public TimeEntryController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }
        //public async Task<GenericResult> GetAllPrj()
        //{
        //    var result = await _timeEntryService.GetAllPrj();
        //    return result;
        //}
        //public async Task<GenericResult> GetById(string Id,DateTime ngay, string ngayTrongTuan)
        //{
        //    var result = await _timeEntryService.GetById(Id, ngay, ngayTrongTuan);
        //    return result;
        //}

        public async Task<GenericResult> GetTimeEntry(string UserCode, DateTime FromDate, DateTime ToDate, int Status)
        {
            var result = await _timeEntryService.GetTimeEntry(UserCode,FromDate,ToDate, Status);
            return result;
        }

        public async Task<GenericResult> GetTimeEntryWithoutId(string UserCode, DateTime Date, string ProjectCode, string WBSName, string Comments, int Status)
        {
            var result = await _timeEntryService.GetTimeEntryWithoutId(UserCode, Date, ProjectCode, WBSName, Comments, Status);
            return result;
        }

        public async Task<GenericResult> Create(TimeEntryModel model)
        {

            var result = await _timeEntryService.Create(model);
            return result;
        }

        public async Task<GenericResult> Update(TimeEntryModel model)
        {
            var result = await _timeEntryService.Update(model);
            return result;
        }
        public async Task<GenericResult> Delete(int TimeEntryId)
        {
            var result = await _timeEntryService.Delete(TimeEntryId);
            return result;
        }

        public async Task<GenericResult> DeleteWeekLyTimeEntry(int Stt, string UserCode, string PrjCode, string ContractLineId, string Comments)
        {

            var result = await _timeEntryService.DeleteWeekLyTimeEntry( Stt, UserCode, PrjCode,  ContractLineId,  Comments);
            return result;
        }

        public async Task<GenericResult> SaveDataWeekLyTimeEntry(int Stt, string UserCode, string PrjCode, string ContractLineId, DateTime Date, string CommentsDB,
             string NewComments, float Hour, string PrjName, string SAPB1DB)  
        {

            var result = await _timeEntryService.SaveDataWeekLyTimeEntry(Stt, UserCode, PrjCode, ContractLineId, Date, CommentsDB, NewComments, Hour, PrjName, SAPB1DB);
            return result;
        }

        public async Task<GenericResult> SubmitDataWeekLyTimeEntry(int Stt, string UserCode, string PrjCode, string ContractLineId, DateTime Date, string CommentsDB,
             string NewComments, float Hour, string PrjName, string SAPB1DB)
        {

            var result = await _timeEntryService.SubmitDataWeekLyTimeEntry(Stt, UserCode, PrjCode, ContractLineId, Date, CommentsDB, NewComments, Hour, PrjName, SAPB1DB);
            return result;
        }

    }
}

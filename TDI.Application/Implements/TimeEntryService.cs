using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Data.Repositories;
using TDI.Utilities.Dtos;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace TDI.Application.Implements
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly IGenericRepository<TimeEntryModel> _timeEntryRepository;
        private readonly IMapper _mapper;


        public TimeEntryService(IGenericRepository<TimeEntryModel> timeEntryRepository, IMapper mapper)
        {
            _timeEntryRepository = timeEntryRepository;
            _mapper = mapper;
        }
        //public async Task<GenericResult> GetAllPrj()
        //{
        //    GenericResult resulTest = new GenericResult();
        //    try
        //    {
        //        var parameter = new DynamicParameters();
        //        var data = _timeEntryRepository.GetAll($"sp_projectlist", parameter, commandType: CommandType.StoredProcedure);
        //        resulTest.Success = true;
        //        resulTest.Data = data as List<TimeEntryModel>;
        //    }
        //    catch (Exception ex)
        //    {
        //        resulTest.Success = false;
        //        resulTest.Message = ex.Message;
        //    }
        //    return resulTest;
        //}
        //public async Task<GenericResult> GetById(string Id, DateTime ngay, string ngayTrongTuan)
        //{
        //    GenericResult result = new GenericResult();
        //    try
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("Id", Id);
        //        parameters.Add("ngay", ngay);
        //        parameters.Add("ngayTrongTuan", ngayTrongTuan);


        //        var data = await _timeEntryRepository.GetAsync($"USP_S_TimeEntry", parameters, commandType: CommandType.StoredProcedure);
        //        result.Success = true;
        //        result.Data = data as TimeEntryModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = ex.Message;
        //    }
        //    return result;
        //}

        public async Task<GenericResult> GetTimeEntry(string UserCode, DateTime FromDate, DateTime ToDate, int Status)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate);
                parameter.Add("Status", Status);

                var data = _timeEntryRepository.GetAll($"USP_S_TimeEntry", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<TimeEntryModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> GetTimeEntryById(string UserCode, string Id)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("Id", Id);


                var data = await _timeEntryRepository.GetAsync($"USP_S_TimeEntryById", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as TimeEntryModel;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> GetTimeEntryWithoutId(string UserCode, DateTime Date, string PrjCode, string ContractLineId, string Comments, int Status)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("Date", Date);
                parameter.Add("PrjCode", PrjCode);
                parameter.Add("Comments", Comments);
                parameter.Add("ContractLineId", ContractLineId);
                parameter.Add("Status", Status);

                TimeEntryModel data = _timeEntryRepository.Get($"USP_S_TimeEntryWithoutId", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }

        public async Task<GenericResult> Create(TimeEntryModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                //parameters.Add("Id", model.Id);
                parameters.Add("UserCode", model.UserCode);
                parameters.Add("Date", model.Date);
                parameters.Add("Hour", model.Hour);
                parameters.Add("PrjCode", model.PrjCode);
                parameters.Add("PrjName", model.PrjName);
                parameters.Add("Bilable", model.Bilable);
                parameters.Add("Description", model.Description);
                parameters.Add("Status", model.Status);
                parameters.Add("SAPB1DB", model.SAPB1DB);
                parameters.Add("Task", model.Task);
                parameters.Add("Comments", model.Comments);
                parameters.Add("Posted", model.Posted);
                parameters.Add("Phase", model.Phase);
                parameters.Add("Country", model.Country);
                parameters.Add("EBMName", model.EBMName);
                parameters.Add("ProspectName", model.ProspectName);
                parameters.Add("CreateBy", model.UserCode);
                parameters.Add("CreateOn", DateTime.Now);
                parameters.Add("@ModifiedBy", "");
                parameters.Add("@ModifiedOn", DateTime.Now);
                parameters.Add("@ContractLineId", model.ContractLineId);
                var affectedRows = _timeEntryRepository.Insert("USP_I_TimeEntry", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> Update(TimeEntryModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", model.Id);
                parameters.Add("UserCode", model.UserCode);
                parameters.Add("Date", model.Date);
                parameters.Add("Hour", model.Hour);
                parameters.Add("PrjCode", model.PrjCode);
                parameters.Add("PrjName", model.PrjName);
                parameters.Add("Bilable", model.Bilable);
                parameters.Add("Description", model.Description);
                parameters.Add("Status", model.Status);
                parameters.Add("SAPB1DB", model.SAPB1DB);
                parameters.Add("Task", model.Task);
                parameters.Add("Comments", model.Comments);
                parameters.Add("Posted", model.Posted);
                parameters.Add("Phase", model.Phase);
                parameters.Add("Country", model.Country);
                parameters.Add("EBMName", model.EBMName);
                parameters.Add("ProspectName", model.ProspectName);
                parameters.Add("CreateBy", model.UserCode);
                parameters.Add("CreateOn", DateTime.Now);
                parameters.Add("@ModifiedBy", "");
                parameters.Add("@ModifiedOn", DateTime.Now);
                parameters.Add("@ContractLineId", model.ContractLineId);
                var affectedRows = _timeEntryRepository.Update("USP_U_TimeEntry", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                //result.Message = key;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> Delete(int TimeEntryId)
        {

            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", TimeEntryId);

                _timeEntryRepository.Execute("USP_D_TimeEntry", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> DeleteWeekLyTimeEntry(int Stt, string UserCode, string PrjCode, string ContractLineId, string Comments)
        {

            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", UserCode);
                parameters.Add("PrjCode", PrjCode);
                parameters.Add("ContractLineId", ContractLineId);
                parameters.Add("Comments", Comments);

                _timeEntryRepository.Execute("USP_D_TimeEntry_Weekly", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> SaveDataWeekLyTimeEntry(int Stt, string UserCode, string PrjCode, string ContractLineId, DateTime Date, string CommentsDB,
            string NewComments, float Hour, string PrjName, string SAPB1DB)
        {

            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", UserCode);
                parameters.Add("PrjCode", PrjCode);
                parameters.Add("ContractLineId", ContractLineId);
                parameters.Add("Date", Date);
                parameters.Add("Comments", CommentsDB);  //comments old

                parameters.Add("NewComments", NewComments);
                parameters.Add("Hour", Hour);
                parameters.Add("PrjName", PrjName);
                parameters.Add("SAPB1DB", SAPB1DB);

                _timeEntryRepository.Execute("USP_I_TimeEntry_Weekly", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<GenericResult> SubmitDataWeekLyTimeEntry(int Stt, string UserCode, string PrjCode, string ContractLineId, DateTime Date, string CommentsDB,
            string NewComments, float Hour, string PrjName, string SAPB1DB)
        {

            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", UserCode);
                parameters.Add("PrjCode", PrjCode);
                parameters.Add("ContractLineId", ContractLineId);
                parameters.Add("Date", Date);
                parameters.Add("Comments", CommentsDB);  //comments old

                parameters.Add("NewComments", NewComments);
                parameters.Add("Hour", Hour);
                parameters.Add("PrjName", PrjName);
                parameters.Add("SAPB1DB", SAPB1DB);

                _timeEntryRepository.Execute("USP_I_TimeEntry_Weekly_Submit", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        //public async Task<GenericResult> ApprovalTimeEntryByID(int ID)
        //{

        //    GenericResult result = new GenericResult();
        //    try
        //    {
        //        var parameters = new DynamicParameters(); 
        //        parameters.Add("ID", ID); 

        //        _timeEntryRepository.Execute("USP_I_TimeEntry_Weekly_Submit", parameters, commandType: CommandType.StoredProcedure);
        //        result.Success = true;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = ex.Message;
        //    }
        //    return result;
        //}


    }
}

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

namespace TDI.Application.Implements
{
    public class TestService : ITestService
    {
        private readonly IGenericRepository<TestModel> _testRepository;
        private readonly IMapper _mapper;


        public TestService(IGenericRepository<TestModel> testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }
        //public async Task<GenericResult> TestGetTimeEntry()
        //{
        //    GenericResult resulTest= new GenericResult();
        //    try
        //    {
        //        var parameter = new DynamicParameters();
        //        var data = _timeEntryRepository.GetAll($"USP_S_TimeEntry", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<GenericResult> GetTimeEntry(string UserCode,DateTime FromDate,DateTime ToDate)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("FromDate", FromDate);
                parameter.Add("ToDate", ToDate); 

                var data = _testRepository.GetAll($"USP_S_TimeEntry", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success= true;
                resulGetTimeEntry.Data = data as List<TestModel>;
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
               

                var data = await _testRepository.GetAsync($"USP_S_TimeEntryById", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as TestModel;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> GetTimeEntryWithoutId(string UserCode, DateTime Date, string PrjCode)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);
                parameter.Add("Date", Date);
                parameter.Add("PrjCode", PrjCode);

                var data = _testRepository.Get($"USP_S_TimeEntryWithoutId", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as TestModel;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }

        public async Task<GenericResult> Create(TestModel model)
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
                parameters.Add("NamEBMNamee", model.EBMName);
                parameters.Add("ProspectName", model.ProspectName);
                //string qury = $"[USP_U_Country] '1','ABC','1','N'";

                var affectedRows = _testRepository.Insert("USP_I_TimeEntry", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<GenericResult> Update(TestModel model)
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
               
                var affectedRows = _testRepository.Update("USP_U_TimeEntry", parameters, commandType: CommandType.StoredProcedure);
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

                _testRepository.Execute("USP_D_TimeEntry", parameters, commandType: CommandType.StoredProcedure);
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

      
    }
}

using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class CountryService : ICountryService
    {
        private readonly IGenericRepository<CountryModel> _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(IGenericRepository<CountryModel> countryRepository, IMapper mapper/*, IHubContext<RequestHub> hubContext*/
        )//: base(hubContext)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;

        }
        public async Task<GenericResult> GetAll()
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                //parameters.Add("CompanyCode", CompanyCode ?? "");
                //parameters.Add("StoreId", StoreId ?? "");
                //parameters.Add("DailyId", DailyId ?? "");
                //parameters.Add("Id", "");
                var data = await _countryRepository.GetAllAsync($"USP_S_Country", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data = data as List<CountryModel>;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> GetById(int Id)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", Id);

                var data = await _countryRepository.GetAsync($"USP_S_CountryById", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data = data as CountryModel;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> Create(CountryModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("Name", model.Name);
                parameters.Add("Active", model.Active);
                parameters.Add("Sapb1db", model.Sapb1db);
                parameters.Add("HREmail", model.HREmail);
                //string qury = $"[USP_U_Country] '1','ABC','1','N'";

                var affectedRows = _countryRepository.Insert("USP_I_Country", parameters, commandType: CommandType.StoredProcedure);
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
        //public async Task<GenericResult> CreateList(List<CountryModel> models)
        //{
        //    GenericResult result = new GenericResult();
        //    try
        //    {
        //        foreach (var model in models)
        //        {
        //            var parameters = new DynamicParameters();

        //            parameters.Add("Name", model.Name);
        //            parameters.Add("Active", model.Active);
        //            parameters.Add("Sapb1db", model.Sapb1db);
        //            //string qury = $"[USP_U_Country] '1','ABC','1','N'";

        //            var affectedRows = _countryRepository.Insert("USP_I_Country", parameters, commandType: CommandType.StoredProcedure);
        //            result.Success = true;
        //            //result.Message = key;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = ex.Message;
        //    }
        //    return result;
        //}

        public async Task<GenericResult> Update(CountryModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", model.Id);
                parameters.Add("Name", model.Name);
                parameters.Add("Active", model.Active);
                parameters.Add("Sapb1db", model.Sapb1db);
                parameters.Add("HREmail", model.HREmail);
                var affectedRows = _countryRepository.Update("USP_U_Country", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<GenericResult> Delete(int CountryId)
        {

            GenericResult result = new GenericResult();

            //result.Success = false;
            //result.Message = "Tao k cho mày xóa vì chức  năng đang đực nâng cấp";
            //return result;
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", CountryId);

                _countryRepository.Execute("USP_D_Country", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<GenericResult> GetCountryActive(int Id)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                if (Id >= 0)
                {
                    parameters.Add("Id", Id);
                }
                else
                {
                    parameters.Add("Id", null);
                }

                var data = await _countryRepository.GetAllAsync($"USP_S_CountryActive", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data = data;
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

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
    public class HolidayService : IHolidayService  
    {
        private readonly IGenericRepository<HolidayModel> _Repository;
        private readonly IMapper _mapper;

        public HolidayService(IGenericRepository<HolidayModel> HolidayRepository, IMapper mapper/*, IHubContext<RequestHub> hubContext*/
        )//: base(hubContext)
        {
            _Repository = HolidayRepository;
            _mapper = mapper;

        }
        public async Task<GenericResult> GetAll()
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                var data = await _Repository.GetAllAsync($"USP_S_Holiday", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data = data as List<HolidayModel>;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        //public async Task<GenericResult> GetAll()
        //{
        //    GenericResult result = new GenericResult();
        //    try
        //    {
        //        var parameters = new DynamicParameters();
        //        //parameters.Add("CompanyCode", CompanyCode ?? "");
        //        //parameters.Add("StoreId", StoreId ?? "");
        //        //parameters.Add("DailyId", DailyId ?? "");jhjnjn
        //        //parameters.Add("Id", "");
        //        var data = await _Repository.GetAllAsync($"USP_S_Holiday", parameters, commandType: CommandType.StoredProcedure);
        //        result.Success = true;
        //        result.Data = data as List<HolidayModel>;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = ex.Message;
        //    }
        //    return result;
        //}
        public async Task<GenericResult> GetById(int Id)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", Id );

                var data = await _Repository.GetAsync($"USP_S_HolidayById", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data =  data as HolidayModel;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> Create(HolidayModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
              
                parameters.Add("Title", model.Title);
                parameters.Add("HolidayDateFr", model.HolidayDateFr);
                parameters.Add("HolidayDateTo", model.HolidayDateTo);
                parameters.Add("CountryId", model.CountryId);
                parameters.Add("ProjectCode", model.ProjectCode);
                parameters.Add("WBSCode", model.WBSCode);
                parameters.Add("ContractName", model.ContractName);

                //string qury = $"[USP_U_Country] '1','ABC','1','N'";

                var affectedRows = _Repository.Insert("USP_I_Holiday", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<GenericResult> Update(HolidayModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", model.Id);
                parameters.Add("Title", model.Title);
                parameters.Add("HolidayDateFr", model.HolidayDateFr);
                parameters.Add("HolidayDateTo", model.HolidayDateTo);
                parameters.Add("CountryId", model.CountryId);
                parameters.Add("ProjectCode", model.ProjectCode);
                parameters.Add("WBSCode", model.WBSCode);
                parameters.Add("ContractName", model.ContractName);

                var affectedRows = _Repository.Update("USP_U_Holiday", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<GenericResult> Delete(int HolidayId )
        {

            GenericResult result = new GenericResult();

            //result.Success = false;
            //result.Message = "Tao k cho mày xóa vì chức  năng đang đực nâng cấp";
            //return result;
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", HolidayId);
               
                _Repository.Execute("USP_D_Holiday", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<GenericResult> GetHolidayByUser(string userCode)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserCode", userCode);

                var data = await _Repository.GetAllAsync($"USP_S_HolidayByUser", parameters, commandType: CommandType.StoredProcedure);
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

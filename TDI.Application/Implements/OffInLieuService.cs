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
    public class OffInLieuService : IOffInLieuService
    {
        private readonly IGenericRepository<OffInLieuModel> _offInLieuRepository;

        public OffInLieuService(IGenericRepository<OffInLieuModel> offInLieuRepository)
        {
            this._offInLieuRepository = offInLieuRepository;
        }

        public async Task<GenericResult> GetAllOffInLieu(string userName)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserName", userName);

                var data = await _offInLieuRepository.GetAllAsync($"USP_S_OffInLieu", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                if (data.Any())
                {
                    result.Data = data.ToList();
                }
                else
                {
                    result.Message = "Data not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public GenericResult InsertOffInLieu(string userCode, OffInLieuModel offInLieu)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserName", offInLieu.UserName);
                parameters.Add("Days", offInLieu.Days);
                parameters.Add("Month", offInLieu.Month);
                parameters.Add("Year", offInLieu.Year);
                parameters.Add("Remark", offInLieu.Remark);
                parameters.Add("CreatedBy", userCode);

                _offInLieuRepository.Execute($"USP_I_OffInLieu", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Insert OffInLieu success.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Insert OffInLieu failed: " + ex.Message;
            }
            return result;
        }

        public GenericResult UpdateOffInLieu(string userCode, OffInLieuModel offInLieuOld, OffInLieuModel offInLieuNew)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID", offInLieuOld.ID);
                parameters.Add("UserName", offInLieuNew.UserName);
                parameters.Add("Days", offInLieuNew.Days);
                parameters.Add("Month", offInLieuNew.Month);
                parameters.Add("Year", offInLieuNew.Year);
                parameters.Add("Remark", offInLieuNew.Remark);
                parameters.Add("ModifiedBy", userCode);

                _offInLieuRepository.Execute($"USP_U_OffInLieu", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Update OffInLieu success.";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Update OffInLieu failed: " + ex.Message;
            }
            return result;
        }

        public GenericResult DeleteOffInLieu(OffInLieuModel offInLieu)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID", offInLieu.ID);

                _offInLieuRepository.Execute($"USP_D_OffInLieu", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Message = "Delete OffInLieu success.";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Delete OffInLieu failed: " + ex.Message;
            }
            return result;
        }
    }
}

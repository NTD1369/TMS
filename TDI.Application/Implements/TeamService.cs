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
    public class TeamService : ITeamService
    {
        private readonly IGenericRepository<TeamModel> _teamRespository;

        public TeamService(IGenericRepository<TeamModel> teamRespository)
        {
            _teamRespository = teamRespository;

        }
        public async Task<GenericResult> GetById(int Id)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", Id);

                var data = await _teamRespository.GetAsync($"USP_S_TeamById", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data = data as TeamModel;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> GetAllTeam(int id, string status)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id);
                parameters.Add("Status", status);

                var data = await _teamRespository.GetAllAsync($"USP_S_Team", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<GenericResult> GetAllTeams()
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                var data = await _teamRespository.GetAllAsync($"USP_SS_Team", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data = data as List<TeamModel>;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
       
        public async Task<GenericResult> Create(TeamModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("Name", model.Name);
                parameters.Add("Leader", model.Leader);
                parameters.Add("Status", model.Status);
        
                var affectedRows = _teamRespository.Insert("USP_I_Team", parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<GenericResult> Update(TeamModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", model.Id);
                parameters.Add("Name", model.Name);
                parameters.Add("Leader", model.Leader);
                parameters.Add("Status", model.Status);
                var affectedRows = _teamRespository.Update("USP_U_Team", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<GenericResult> Delete(int Id)
        {

            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", Id);

                _teamRespository.Execute("USP_D_Team", parameters, commandType: CommandType.StoredProcedure);
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

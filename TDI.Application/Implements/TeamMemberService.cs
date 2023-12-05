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
    public class TeamMemberService : ITeamMemberService
    {
        private readonly IGenericRepository<TeamMemberModel> _teamMemberRespository;

        public TeamMemberService(IGenericRepository<TeamMemberModel> teamMemberRespository)
        {
            _teamMemberRespository = teamMemberRespository;

        }
        public async Task<GenericResult> GetTeamMemberById(int TeamId)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("TeamId", TeamId);

                var data = await _teamMemberRespository.GetAllAsync($"USP_S_TeamMemberById", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                result.Data = data as List<TeamMemberModel>;
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

using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class TeamController
    {
        ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public async Task<GenericResult> GetById(int id)
        {
            var result = await _teamService.GetById(id);
            return result;
        }
        public async Task<GenericResult> GetAllTeam(int id, string status)
        {
            var result = await _teamService.GetAllTeam(id,status);
            return result;
        }
        public async Task<GenericResult> GetAllTeams()
        {
            var result = await _teamService.GetAllTeams();
            return result;
        }
        public async Task<GenericResult> Create(TeamModel model)
        {
            var result = await _teamService.Create(model);
            return result;
        }

        public async Task<GenericResult> Update(TeamModel model)
        {
            var result = await _teamService.Update(model);
            return result;
        }
        public async Task<GenericResult> Delete(int Id)
        {
            var result = await _teamService.Delete(Id);
            return result;
        }
    }
}

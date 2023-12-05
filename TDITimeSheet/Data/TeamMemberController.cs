using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class TeamMemberController
    {
        ITeamMemberService _teamMemberService;
        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }
        public async Task<GenericResult> GetTeamMemberById(int TeamId)
        {
            var result = await _teamMemberService.GetTeamMemberById(TeamId);
            return result;
        }
        
    }
}

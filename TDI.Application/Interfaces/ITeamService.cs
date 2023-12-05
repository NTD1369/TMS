using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface ITeamService
    {
        Task<GenericResult> GetById(int Id);

        Task<GenericResult> GetAllTeam(int id, string status);
        Task<GenericResult> GetAllTeams();
        Task<GenericResult> Create(TeamModel model);
        Task<GenericResult> Update(TeamModel model);
        Task<GenericResult> Delete(int Id);
    }
}

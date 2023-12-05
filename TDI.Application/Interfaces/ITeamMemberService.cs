using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface ITeamMemberService
    {
        Task<GenericResult> GetTeamMemberById(int TeamId);
    }
}

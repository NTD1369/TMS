using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IChangePasswordService
    {
        Task<GenericResult> Update(string userName, string password);
    }
}

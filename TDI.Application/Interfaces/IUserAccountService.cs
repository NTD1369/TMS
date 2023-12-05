using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Application.Interfaces;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IUserAccountService
    {
        Task<GenericResult> GetUser(string userName, string passWord);

     

    }
}



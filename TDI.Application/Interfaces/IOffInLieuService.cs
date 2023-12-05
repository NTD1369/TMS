using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface IOffInLieuService
    {
        Task<GenericResult> GetAllOffInLieu(string userName);
        GenericResult InsertOffInLieu(string userCode, OffInLieuModel offInLieu);
        GenericResult UpdateOffInLieu(string userCode, OffInLieuModel offInLieuOld, OffInLieuModel offInLieuNew);
        GenericResult DeleteOffInLieu(OffInLieuModel offInLieu);
    }
}

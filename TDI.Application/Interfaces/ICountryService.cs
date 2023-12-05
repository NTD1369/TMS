using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDI.Application.Interfaces
{
    public interface ICountryService
    {
        Task<GenericResult> GetAll();
        Task<GenericResult> GetById(int Id);
        Task<GenericResult> Create(CountryModel model);
        Task<GenericResult> Update(CountryModel model);
        Task<GenericResult> Delete(int Country);
        Task<GenericResult> GetCountryActive(int Id);
    }
}

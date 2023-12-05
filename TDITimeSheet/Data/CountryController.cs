using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class CountryController
    {
        ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        ///getall?id="abc"&name="cshd" <summary>
        /// getall?id="abc"
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResult> GetAll()
        {
            var result = await _countryService.GetAll();
            return result;
        }
        public async Task<GenericResult> GetById(int Id)
        {
            var result = await _countryService.GetById(Id);
            return result;
        }

        public async Task<GenericResult> Create(CountryModel model)
        {
            var result = await _countryService.Create(model);
            return result;
        }
        // public async Task<GenericResult> CreateList(List<CountryModel> models)
        //{
        //        //var result = await _countryService.Create(model);
                
           
         
        //    return null;
        //}

        public async Task<GenericResult> Update(CountryModel model)
        {
            var result = await _countryService.Update(model);
            return result;
        }
        public async Task<GenericResult> Delete(int CountryId)
        {
            var result = await _countryService.Delete(CountryId);
            return result;
        }


    }
}

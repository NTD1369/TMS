using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
   
        public class HolidayController
        {
            IHolidayService _holidayService;
            public HolidayController(IHolidayService holidayService)
            {
                _holidayService = holidayService;
            }
            ///getall?id="abc"&name="cshd" <summary>
            /// getall?id="abc"
            /// </summary>
            /// <returns></returns>
            public async Task<GenericResult> GetAll()
            {
                var result = await _holidayService.GetAll();
                return result;
            }
            public async Task<GenericResult> GetById(int Id)
            {
                var result = await _holidayService.GetById(Id);
                return result;
            }

            public async Task<GenericResult> Create(HolidayModel model)
            {
                var result = await _holidayService.Create(model);
                return result;
            }

            public async Task<GenericResult> Update(HolidayModel model)
            {
                var result = await _holidayService.Update(model);
                return result;
            }
            public async Task<GenericResult> Delete(int HolidayID)
            {
                var result = await _holidayService.Delete(HolidayID);
                return result;
            }


            #region "Code xu ly "
            //public Task<HolidayModel[]> GetFirstLoadAsync2(DateOnly startDate)
            //{
            //    var holidayAll = await _holidayService.GetAll();
            //    if (holidayAll.Success == true)
            //    {
            //        return holidayAll.Data.;
            //    }

            //    //return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //    //{
            //    //    Date = startDate.AddDays(index),
            //    //    TemperatureC = Random.Shared.Next(-20, 55),
            //    //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //    //}).ToArray());
            //}

            //public async Task<GenericResult> FirstLoad()
            //{


            //}


            #endregion

        
    }
}

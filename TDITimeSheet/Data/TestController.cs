using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;
using TDITimeSheet.Pages;

namespace TDITimeSheet.Data
{
    public class TestController
    {
        ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }
       
        public async Task<GenericResult> GetTimeEntry(string UserCode, DateTime FromDate, DateTime ToDate)
        {
            var result = await _testService.GetTimeEntry(UserCode,FromDate,ToDate);
            return result;
        }

        public async Task<GenericResult> GetTimeEntryWithoutId(string UserCode, DateTime Date, string ProjectCode)
        {
            var result = await _testService.GetTimeEntryWithoutId(UserCode, Date, ProjectCode);
            return result;
        }

        public async Task<GenericResult> Create(TestModel model)
        {
            var result = await _testService.Create(model);
            return result;
        }

        public async Task<GenericResult> Update(TestModel model)
        {
            var result = await _testService.Update(model);
            return result;
        }
        public async Task<GenericResult> Delete(int TimeEntryId)
        {
            var result = await _testService.Delete(TimeEntryId);
            return result;
        }
    }
}

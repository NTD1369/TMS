using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;

namespace TDITimeSheet.Data
{
    public class RolesController
    {
        IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        public async Task<GenericResult> GetByRoleId(string id)
        {
            var result = await _rolesService.GetByRoleId(id);
            return result;
        }

        public async Task<GenericResult> GetAll()
        {
            var result = await _rolesService.GetAll();
            return result;
        }  public async Task<GenericResult> GetAllMfuction()
        {
            var result = await _rolesService.GetAllMfuction();
            return result;
        }

        public async Task<GenericResult> Create(RolesModel model)
        {

            var result = await _rolesService.Create(model);
            return result;
        }

        public async Task<GenericResult> Update(RolesModel model)
        {
            var result = await _rolesService.Update(model);
            return result;
        }
        public async Task<GenericResult> Delete(string id)
        {
            var result = await _rolesService.Delete(id);
            return result;
        }
    }
}

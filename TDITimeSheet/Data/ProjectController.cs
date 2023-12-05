using TDI.Application.Implements;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Utilities.Dtos;
using TDITimeSheet.Pages;

namespace TDITimeSheet.Data
{
    public class ProjectController
    {
        IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<GenericResult> GetProject()
        {
            var result = await _projectService.GetProject();
            return result;
        }
        public async Task<GenericResult> GetProject_ContractHeader_ForWBS(string UserCode)
        {
            var result = await _projectService.GetProject_ContractHeader_ForWBS(UserCode);
            return result;
        }
        public async Task<GenericResult> sp_projectlist_byUser(string UserCode)
        {
            var result = await _projectService.sp_projectlist_byUser(UserCode);
            return result;
        }


        public async Task<GenericResult> Create(ProjectModel model)
        {
            var result = await _projectService.Create(model);
            return result;
        }

        public async Task<GenericResult> Update(ProjectModel model)
        {
            var result = await _projectService.Update(model);
            return result;
        }
        public async Task<GenericResult> Delete(int ProjectId)
        {
            var result = await _projectService.Delete(ProjectId);
            return result;
        }
    }
}

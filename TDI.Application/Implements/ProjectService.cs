using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Data.Repositories;
using TDI.Utilities.Dtos;

namespace TDI.Application.Implements
{
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<ProjectModel> _projectRepository;
        private readonly IMapper _mapper;
        public ProjectService(IGenericRepository<ProjectModel> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<GenericResult> GetProject_ContractHeader_ForWBS(string UserCode)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);

                var data = _projectRepository.GetAll($"sp_projectlist_ContractHeader_ForWBS", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<ProjectModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> sp_projectlist_byUser(string UserCode)
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("UserCode", UserCode);

                var data = _projectRepository.GetAll($"sp_projectlist_byUser", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success = true;
                resulGetTimeEntry.Data = data as List<ProjectModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> GetProject()
        {
            GenericResult resulGetTimeEntry = new GenericResult();
            try
            {
                var parameter = new DynamicParameters();
                //parameter.Add("UserCode", UserCode);

                var data = _projectRepository.GetAll($"sp_projectlist", parameter, commandType: CommandType.StoredProcedure);
                resulGetTimeEntry.Success= true;
                resulGetTimeEntry.Data = data as List<ProjectModel>;
            }
            catch (Exception ex)
            {
                resulGetTimeEntry.Success = false;
                resulGetTimeEntry.Message = ex.Message;
            }
            return resulGetTimeEntry;
        }
        public async Task<GenericResult> Create(ProjectModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("PrjName", model.PrjName);
                parameters.Add("Status", model.Status);

                var affectedRows = _projectRepository.Insert("USP_I_Project", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                //result.Message = key;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> Update(ProjectModel model)
        {
            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("PrjName", model.PrjName);
                parameters.Add("Status", model.Status);
                var affectedRows = _projectRepository.Update("USP_U_Project", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                //result.Message = key;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<GenericResult> Delete(int ProjectId)
        {

            GenericResult result = new GenericResult();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", ProjectId);

                _projectRepository.Execute("USP_D_Project", parameters, commandType: CommandType.StoredProcedure);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }


    }
}

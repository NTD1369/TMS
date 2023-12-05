using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Data.Models;

namespace TDI.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //tạo mapper từ table lên viewModel
            //CreateMap<TSalesHeader, SaleViewModel>();

            //CreateMap<LeaveModel, LeaveViewModel>();
        }
    }
}

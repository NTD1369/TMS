using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TDI.Application.Interfaces;
using TDI.Data.Entities;
using TDI.Data.Models;

namespace TDI.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //tạo mapper từ viewModel lên table
            //CreateMap<SaleViewModel, TSalesHeader>();

            //CreateMap<LeaveViewModel, LeaveModel>();
        }
    }
}

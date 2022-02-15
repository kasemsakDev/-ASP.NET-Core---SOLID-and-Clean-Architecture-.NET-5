using AutoMapper;
using HR.LeaveManagement1.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement1.Application.DTOs.LeaveRequest;
using HR.LeaveManagement1.Application.DTOs.LeaveType;
using HR.LeaveManagement1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement1.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region LeaveRequest Mappings
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestListDto>()
                .ForMember(dest => dest.DateRequested
                , opt => opt.MapFrom(src => src.DateCreated))
                .ReverseMap();
            //Custom constructor methods กำหนดให้ DateRequested มีค่าเท่ากับ DateCreated
            CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, UpdateLeaveRequestDto>().ReverseMap();
            #endregion LeaveRequest

            #region LeaveAllocation Mappings
            CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveAllocation, CreateLeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveAllocation, UpdateLeaveAllocationDto>().ReverseMap();
            #endregion LeaveAllocation

            #region LeaveType Mappings
            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType, UpdateLeaveTypeDto>().ReverseMap();
            #endregion  LeaveType Mappings
        }
    }
}

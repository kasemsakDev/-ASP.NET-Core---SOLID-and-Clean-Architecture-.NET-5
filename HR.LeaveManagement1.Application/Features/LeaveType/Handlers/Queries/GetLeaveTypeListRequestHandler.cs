using AutoMapper;
using HR.LeaveManagement1.Application.DTOs.LeaveType;
using HR.LeaveManagement1.Application.Features.LeaveType.Requests.Queries;
using HR.LeaveManagement1.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement1.Application.Features.LeaveType.Handlers.Queries
{
    public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository; //function get create update dalete at connet db
        private readonly IMapper _mapper; // maper dto with entity

        public GetLeaveTypeListRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {//dependency injection  เอา class มาประกาศใน constructor  เพื่อเรียกใช้งาน class ที่เรา เอาเข้ามา สามารถเข้าถึง functionการใช้งานของ class นั่นๆ
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }


        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetAll();
            return _mapper.Map<List<LeaveTypeDto>>(leaveType);
            //LeaveTypeDto = leaveType
            /*
               LeaveTypeDto.id = leaveType.id
               LeaveTypeDto.name = leaveType.name            
            */
        }
    }
}

﻿using AutoMapper;
using HR.LeaveManagement1.Application.DTOs.LeaveRequest;
using HR.LeaveManagement1.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement1.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement1.Application.Contracts.Identity;

namespace HR.LeaveManagement1.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
    {

        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetLeaveRequestDetailRequestHandler(
            ILeaveRequestRepository leaveRequestRepository
            ,IMapper mapper
            ,IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            this._userService = userService;
        }

        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<LeaveRequestDto>(await _leaveRequestRepository.GetLeaveRequestsWithDetails(request.Id));
            leaveRequest.Employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);
            return leaveRequest;
        }
    }
}

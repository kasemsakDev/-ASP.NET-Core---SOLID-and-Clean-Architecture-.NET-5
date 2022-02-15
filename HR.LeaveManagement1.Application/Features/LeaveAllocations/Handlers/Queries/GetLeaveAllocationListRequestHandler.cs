using AutoMapper;
using HR.LeaveManagement1.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement1.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement1.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HR.LeaveManagement1.Application.Contracts.Identity;
using HR.LeaveManagement1.Domain;
using HR.LeaveManagement1.Application.Constants;

namespace HR.LeaveManagement1.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
    {

       private readonly ILeaveAllocationRepository _leaveAllocationRepository;
       private readonly IMapper _mapper;
       private readonly IHttpContextAccessor _httpContextAccessor;
       private readonly IUserService _userService;

       public GetLeaveAllocationListRequestHandler(
           ILeaveAllocationRepository leaveAllocationRepository 
           ,IMapper mapper
           ,IHttpContextAccessor httpContextAccessor
           , IUserService userService)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
            this._userService = userService;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = new List<LeaveAllocation>();
            var allocations = new List<LeaveAllocationDto>();

            if(request.IsLoggedInUser)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst
                    (q => q.Type == CustomClaimTypes.Uid)?.Value;
                leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails(userId);
            }
            else
            {
                leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
                allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach(var req in allocations)
                {
                    req.Employee = await _userService.GetEmployee(req.EmployeeId);
                }
            }
            return allocations;
        }
    }
}

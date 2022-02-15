using AutoMapper;
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
using Microsoft.AspNetCore.Http;
using HR.LeaveManagement1.Application.Contracts.Identity;
using HR.LeaveManagement1.Domain;
using HR.LeaveManagement1.Application.Constants;

namespace HR.LeaveManagement1.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {

        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessssor;
        private readonly IUserService _userService;

        public GetLeaveRequestListRequestHandler(
            ILeaveRequestRepository leaveRequestRepository
            , IMapper mapper
            , IUserService userService
            , IHttpContextAccessor httpContextAccessssor
            )
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            this._userService = userService;
            this._httpContextAccessssor = httpContextAccessssor;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            if(request.IsLoggedInUser)
            {
                var userId = _httpContextAccessssor.HttpContext.User
                    .FindFirst(q => q.Type == CustomClaimTypes.Uid)?.Value;
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

                var employee = await _userService.GetEmployee(userId);
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                }
            }
            return requests;
        }
    }
}

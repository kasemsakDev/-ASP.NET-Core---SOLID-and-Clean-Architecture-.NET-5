using AutoMapper;
using HR.LeaveManagement1.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement1.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HR.LeaveManagement1.Domain;
using System.Threading.Tasks;
using HR.LeaveManagement1.Application.DTOs.LeaveAllocation.Validator;
using HR.LeaveManagement1.Application.Exceptions;
using HR.LeaveManagement1.Application.Contracts.Identity;
using HR.LeaveManagement1.Application.Responses;

namespace HR.LeaveManagement1.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, 
            IMapper mapper,
            IUserService userService
            )
        {
            _unitOfWork = unitOfWork;
            this._userService = userService;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var responese = new BaseCommandResponse();
            var validator = new CreateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocation);

            if(validationResult.IsValid == false)
            {
                responese.Success = false;
                responese.Message = "Allocations Failed";
                responese.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = await _unitOfWork.LeaveTypeRepository.Get(request.LeaveAllocation.LeaveTypeId);
                var employees = await _userService.GetEmployees();
                var period = DateTime.Now.Year;
                var allocations = new List<LeaveAllocation>();
                foreach(var emp in employees)
                {
                    if(await _unitOfWork.LeaveAllocationRepository.AllocationExists(emp.Id, leaveType.Id, period))
                        continue;
                    allocations.Add(new LeaveAllocation
                    {
                        EmployeeId = emp.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period
                    });
                }
                await _unitOfWork.LeaveAllocationRepository.AddAllocations(allocations);
                await _unitOfWork.Save();
                responese.Success = true;
                responese.Message = "Allocations.Successful";
            }              
            return responese;
        }
    }
}

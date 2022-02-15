using AutoMapper;
using HR.LeaveManagement1.Application.DTOs.LeaveAllocation.Validator;
using HR.LeaveManagement1.Application.Exceptions;
using HR.LeaveManagement1.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement1.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement1.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(
            IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

            if(validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.Get(request.Id);

            if(leaveAllocation is null)
            {
                throw new NotFoundException(nameof(leaveAllocation), request.LeaveAllocationDto.Id);
            }

            _mapper.Map(request.LeaveAllocationDto, leaveAllocation);

            await _unitOfWork.LeaveAllocationRepository.Update(leaveAllocation);
            await _unitOfWork.Save();
            return Unit.Value;

        }
    }
}

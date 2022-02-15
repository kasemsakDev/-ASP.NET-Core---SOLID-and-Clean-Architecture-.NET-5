using HR.LeaveManagement1.Application.DTOs.LeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement1.Application.Features.LeaveType.Requests.Commands
{
    public class UpdateLeaveTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UpdateLeaveTypeDto LeaveTypeDto { get; set; }
    }
}

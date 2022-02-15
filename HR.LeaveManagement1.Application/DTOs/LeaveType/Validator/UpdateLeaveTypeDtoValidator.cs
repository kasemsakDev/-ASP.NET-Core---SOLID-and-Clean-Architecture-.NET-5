using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement1.Application.DTOs.LeaveType.Validator
{
    public class UpdateLeaveTypeDtoValidator  : AbstractValidator<UpdateLeaveTypeDto>
    {
        public UpdateLeaveTypeDtoValidator()
        {
            Include(new ILeaveTypeDtoValidator());

            RuleFor(p => p.Id).NotEmpty().WithMessage("{PropertyName} must be present");
        }
    }
}

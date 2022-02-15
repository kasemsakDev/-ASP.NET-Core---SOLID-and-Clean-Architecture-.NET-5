using HR.LeaveManagement1.Application.DTOs.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement1.Application.DTOs.LeaveAllocation
{
    public  class UpdateLeaveAllocationDto : BaseDto,ILeaveAllocationDto
    {
        public int NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}

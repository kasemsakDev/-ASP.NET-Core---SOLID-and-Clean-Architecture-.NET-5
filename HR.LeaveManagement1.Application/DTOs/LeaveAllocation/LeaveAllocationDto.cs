using HR.LeaveManagement1.Application.DTOs.common;
using HR.LeaveManagement1.Application.DTOs.LeaveType;
using HR.LeaveManagement1.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement1.Application.DTOs.LeaveAllocation
{
    public class LeaveAllocationDto : BaseDto
    {
        public int NumberOfDays { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public Employee Employee { get; set; }
        public string EmployeeId { get; set;}
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}

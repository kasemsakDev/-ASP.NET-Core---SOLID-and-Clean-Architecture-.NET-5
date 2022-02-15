using HR.LeaveManagement1.Application.DTOs.LeaveRequest;
using HR.LeaveManagement1.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement1.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement1.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.LeaveManagement1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GETAll : api/<LeaveRequestsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDto>>> Get(bool isLoggedInUser = false)
        {
            var Request = new GetLeaveRequestListRequest() {  IsLoggedInUser = isLoggedInUser };
            var leaveRequest = await _mediator.Send(Request);
            return Ok(leaveRequest);
        }

        //GETDetail("{id}" : api/<LeaveRequestsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
            return Ok(leaveRequest);
        }

        //POST : api/<LeaveRequestsController>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveRequestDto leaveRequest)
        {
            var command = new CreateLeaveRequestCommand { LeaveRequestDto = leaveRequest };
            var reposonce = await _mediator.Send(command);
            return Ok(reposonce);
        }

        //PUT("{id}" : api/<LeaveRequestsController>/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto leaveRequest)
        {
            var command = new UpdateLeaveRequestCommand { Id = id, LeaveRequestDto = leaveRequest };
            await _mediator.Send(command);
            return NoContent();
        }

        //PUT("ChangeApproval/{id}") ChangeApproval api/<LeaveRequestsController>/ChangeApproval/5
        [HttpPut("ChangeApproval/{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] ChangeLeaveRequestApprovalDto changeLeaveRequestApproval)
        {
            var command = new UpdateLeaveRequestCommand { Id = id, ChangeLeaveRequestApprovalDto = changeLeaveRequestApproval };
            await _mediator.Send(command);
            return NoContent();
        }

        //DELETE("{id}") : api/<LeaveRequestsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

using HR.LeaveManagement1.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement1.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement1.Application.Features.LeaveAllocations.Requests.Queries;
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
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET api/<LeaveAllocationsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest() { IsLoggedInUser = isLoggedInUser});
            return Ok(leaveAllocations);
        }

        //GET api/<LeaveAllocationsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        {
            //_mediator.Send is  send to IRequestHandler
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationDetailRequest { Id = id });
            return Ok(leaveAllocations);
        }

        //POST : api/<LeaveAllocationsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationDto leaveAllocation)
        {
            var commad = new CreateLeaveAllocationCommand { LeaveAllocation = leaveAllocation };
            var reponse = await _mediator.Send(commad);
            return Ok(reponse);
        }

        //PUT : api/<LeaveAllocationsController>
        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveAllocationDto leaveAllocation)
        {
            var commad = new UpdateLeaveAllocationCommand { Id = id, LeaveAllocationDto = leaveAllocation };
            await _mediator.Send(commad);
            return NoContent();
        }

        //DELETE : api/<LeaveAllocationsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

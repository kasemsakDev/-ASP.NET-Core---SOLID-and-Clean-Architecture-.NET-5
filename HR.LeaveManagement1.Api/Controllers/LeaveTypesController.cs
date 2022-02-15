using HR.LeaveManagement1.Application.DTOs.LeaveType;
using HR.LeaveManagement1.Application.Features.LeaveType.Requests.Commands;
using HR.LeaveManagement1.Application.Features.LeaveType.Requests.Queries;
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
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET : Queries
        //POST : Command
        //ActionResult<LeaveTypeDto>  get return LeaveTypeDto
        //GET : api/<LeaveTypesController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
        }

        //GET api/<LeaveTypeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDto>> Get(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailRequest { Id = id });
            return Ok(leaveType);
        }

        // POST api/<LeaveTypesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveTypeDto leaveType)
        {
            //CreateLeaveTypeCommand is IRequest
            //_mediator send => CreateLeaveTypeCommandHanddler
            //_mediator.Send == Handler
            var command = new CreateLeaveTypeCommand { LeaveTypeDto = leaveType };
            var repsone = await _mediator.Send(command);
            return Ok(repsone);
        }


        //HttpPut is GET and POST
        // PUT api/<LeaveTypesController>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] UpdateLeaveTypeDto leaveType)
        {
            //IRequest add parameter
            var command = new UpdateLeaveTypeCommand { Id= id, LeaveTypeDto = leaveType };
            await _mediator.Send(command);
            return NoContent();
        }

        //DELETE api/<LeaveTypesController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveTypeCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

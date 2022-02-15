﻿using AutoMapper;
using HR.LeaveManagement1.Application.DTOs.LeaveType;
using HR.LeaveManagement1.Application.Features.LeaveType.Handlers.Commands;
using HR.LeaveManagement1.Application.Features.LeaveType.Requests.Commands;
using HR.LeaveManagement1.Application.Contracts.Persistence;
using HR.LeaveManagement1.Application.Profiles;
using HR.LeaveManagement1.Application.UnitTests.Mock;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using HR.LeaveManagement1.Application.Responses;
using HR.LeaveManagement1.Application.UnitTests.Mocks;

namespace HR.LeaveManagement1.Application.UnitTests.LeaveTypes.Commands
{
        public class CreateLeaveTypeCommandHandlerTests
        {
            private readonly IMapper _mapper;
            private readonly Mock<IUnitOfWork> _mockUow;
            private readonly CreateLeaveTypeDto _leaveTypeDto;
            private readonly CreateLeaveTypeCommandHandler _handler;

            public CreateLeaveTypeCommandHandlerTests()
            {
                _mockUow = MockUnitOfWork.GetUnitOfWork();

                var mapperConfig = new MapperConfiguration(c =>
                {
                    c.AddProfile<MappingProfile>();
                });

                _mapper = mapperConfig.CreateMapper();
                _handler = new CreateLeaveTypeCommandHandler(_mockUow.Object, _mapper);

                _leaveTypeDto = new CreateLeaveTypeDto
                {
                    DefaultDays = 15,
                    Name = "Test DTO"
                };
            }

            [Fact]
            public async Task Valid_LeaveType_Added()
            {
                var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

                var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();

                result.ShouldBeOfType<BaseCommandResponse>();

                leaveTypes.Count.ShouldBe(4);
            }

            [Fact]
            public async Task InValid_LeaveType_Added()
            {
                _leaveTypeDto.DefaultDays = -1;

                var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

                var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();

                leaveTypes.Count.ShouldBe(3);

                result.ShouldBeOfType<BaseCommandResponse>();

            }
        }
    }

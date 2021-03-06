﻿using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using AutoMapper;
using Domain.VMs;

namespace Presentation.API.V1
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper
            )
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("self")]
        public IActionResult Get()
        {
            var identityID = User.GetIdentityID();

            var user = _userService.GetByIdentityID(identityID);

            return Ok(_mapper.Map<UserVM>(user));
        }

        [HttpPut("self")]
        public async Task Update([FromForm] UserDTO userDTO)
        {
            var identityID = User.GetIdentityID();

            await _userService.Update(userDTO, identityID);
        }
    }
}

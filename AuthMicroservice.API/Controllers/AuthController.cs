﻿using Auth.Application.Commands.RegisterUser;
using Auth.Application.Queries.LoginUser;
using Auth.Application.Queries.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _mediator.Send(command);

            if (!response.Success) return BadRequest(new { errors = response.Errors });

            return Ok(new { message = response.Message });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _mediator.Send(query);

            return response.Success ? Ok(new { token = response.Token, expiration = response.Expiration }) : Unauthorized(new { errors = response.Errors });
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenQuery query)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _mediator.Send(query);

            if (!response.Success) return Unauthorized(new { errors = response.Errors });

            return Ok(new { token = response.Token, expiration = response.Expiration });
        }
    }
}

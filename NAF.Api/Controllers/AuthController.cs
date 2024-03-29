﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NAF.Application.Interfaces;
using NAF.Domain.Requests;

namespace NAF.Api.Controllers
{
    public class AuthController : NafControllerBase
    {
        private readonly IAuthAppService _authAppService;

        public AuthController(IAuthAppService authAppService, IUserAppService userAppService) : base(userAppService)
        {
            _authAppService = authAppService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _authAppService.Login(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

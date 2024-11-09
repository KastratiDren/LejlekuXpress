using LejlekuXpress.Data.DTO;
using LejlekuXpress.Data.ServiceInterfaces;
using LejlekuXpress.Models;
using LejlekuXpress.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LejlekuXpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly LogService _logService;

        public AuthController(IAuthService service, LogService logService)
        {
            _service = service;
            _logService = logService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDTO request)
        {
            try
            {
                var user = await _service.Register(request);

                await _logService.CreateLog(new Log
                {
                    Action = "RegisterUser",
                    Message = "User registered successfully"
                });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            try
            {
                var token = await _service.Login(request);
                if (token == null)
                {
                    return Unauthorized();
                }

                await _logService.CreateLog(new Log
                {
                    Action = "LoginUser",
                    Message = "User loged in successfully"
                });

                return Ok(new { token, isLoggedIn = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(int id, ChangePasswordDTO request)
        {
            try
            {
                var result = await _service.ChangePassword(id, request);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

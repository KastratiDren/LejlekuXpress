using LejlekuXpress.Data.DTO;
using LejlekuXpress.Data.ServiceInterfaces;
using LejlekuXpress.Models;
using LejlekuXpress.Services;
using Microsoft.AspNetCore.Mvc;

namespace LejlekuXpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly LogService _logService;
        public UserController(IUserService service, LogService logService)
        {
            _service = service;
            _logService = logService;
        }

        #region Get
        [HttpGet("get")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var result = await _service.GetUser(id);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = _service.DeleteUser(id);

                await _logService.CreateLog(new Log
                {
                    Action = "DeleteUser",
                    Message = "User deleted successfully"
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Update
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, UserDTO request)
        {
            try
            {
                var result = _service.UpdateUser(id, request);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetAll
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region MakeMod
        [HttpPut("makemod")]
        public async Task<IActionResult> MakeMod(int id)
        {
            try
            {
                var result = _service.MakeMod(id);
                if (result == null)
                    return NotFound();

                await _logService.CreateLog(new Log
                {
                    Action = "MakeModerator",
                    Message = "User became moderator successfully"
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}

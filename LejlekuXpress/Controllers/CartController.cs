using LejlekuXpress.Data.DTO;
using LejlekuXpress.Data.ServiceInterfaces;
using LejlekuXpress.Models;
using LejlekuXpress.Services;
using Microsoft.AspNetCore.Mvc;

namespace LejlekuXpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ICartService _service;
        private readonly LogService _logService;

        public CartController(ICartService service, LogService logService)
        {
            _service = service;
            _logService = logService;
        }

        #region Add
        [HttpPost("add")]
        public async Task<IActionResult> AddItem(CartDTO request)
        {
            try
            {
                var product = await _service.AddItem(request);

                await _logService.CreateLog(new Log
                {
                    Action = "AddProductToCart",
                    Message = "Product added to cart successfully"
                });

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetByUserId
        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetByUserId(int userID)
        {
            try
            {
                var result = await _service.GetByUserId(userID);
                if (result == null || result.Count == 0)
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
                var result = _service.DeleteItem(id);

                await _logService.CreateLog(new Log
                {
                    Action = "DeleteProductFromCart",
                    Message = "Product deleted form cart successfully"
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

